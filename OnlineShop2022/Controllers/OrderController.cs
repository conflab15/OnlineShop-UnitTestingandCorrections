using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShop2022.Models;
using Stripe;

namespace OnlineShop2022.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCartModel _shoppingCart;
        private readonly UserManager<CustomUserModel> _userManager;

        public OrderController(IOrderRepository orderRepository, ShoppingCartModel shoppingCart, UserManager<CustomUserModel> userManager)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }

        //Solution Additions: Amending the Checkout Page to include the Shopping Cart Contents, and to pass data from the current user to the form. 
        public IActionResult Checkout()
        {
            var vm = new CheckoutViewModel(); //Implement a new ViewModel
            vm.CartItems = _shoppingCart.GetShoppingCartItems(); //Assign the ShoppingCartItem List with the items from the cart
            vm.CartTotal = _shoppingCart.GetShoppingCartTotal();
            var currentUser = _userManager.FindByEmailAsync(User.Identity.Name); //Get the current user
            vm.Forename = currentUser.Result.Fname.ToString(); //Assigning the users details where applicable
            vm.Surname = currentUser.Result.Sname.ToString();
            vm.Email = User.Identity.Name;

            return View(vm);
        }
        public IActionResult Payment(OrderModel order)
        {
            return View(order);
        }

        [HttpPost]
        public ActionResult Pay(PaymentIntentCreateRequest request)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(request.Items),
                Currency = "gbp",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            return Json(new { clientSecret = paymentIntent.ClientSecret });
        }

        private int CalculateOrderAmount(Item[] items)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            return 1400;
        }

        public class Item
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }

        public class PaymentIntentCreateRequest
        {
            [JsonProperty("items")]
            public Item[] Items { get; set; }
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel checkout)
        {
            var items = _shoppingCart.GetShoppingCartItems(); //Gets the Shopping Cart Items
            _shoppingCart.ShoppingCartItems = items; //Not sure what this does?


            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, please add some products.");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(checkout.Order);
                _shoppingCart.ClearCart();
                return RedirectToAction("Payment", checkout.Order);
            }

            return View(checkout);
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}

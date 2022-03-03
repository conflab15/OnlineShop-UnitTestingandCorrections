using System.Collections.Generic;

namespace OnlineShop2022.Models
{
    public class CheckoutViewModel
    {
        //ViewModel to pass to the checkout page, which includes an OrderModel and a List of products in the shopping cart
        public List<ShoppingCartItemModel> CartItems { get; set; }
        public double CartTotal { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public OrderModel Order { get; set; }
    }
}

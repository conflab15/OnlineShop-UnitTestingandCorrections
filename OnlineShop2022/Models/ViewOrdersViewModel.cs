using System.Collections.Generic;

namespace OnlineShop2022.Models
{
    public class ViewOrdersViewModel
    {
        public string CustomerEmail { get; set; }
        public List<OrderModel> Order { get; set; }
    }
}

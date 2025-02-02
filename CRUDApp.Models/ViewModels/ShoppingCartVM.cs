using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDApp.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public Product Product { get; set; }
        [Range(1,1000)]
        public int Count { get; set; }
        public IEnumerable<ShoppingCart> ListCart { get; set; }
        public double TotalPrice { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}

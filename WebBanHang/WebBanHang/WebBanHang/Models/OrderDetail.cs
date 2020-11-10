using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }


        public Nullable<decimal> UnitPriceSale { get; set; }
        public Nullable<int> QuantitySale { get; set; }
        [ForeignKey("Order")]
        public int? IDOrder { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey("Product")]
        public int? IDProduct { get; set; }
        public virtual Product Product { get; set; }
    }
}
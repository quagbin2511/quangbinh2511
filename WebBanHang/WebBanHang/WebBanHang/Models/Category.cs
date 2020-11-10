using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class Category
    {

        [Key]
        public int IDCategory { get; set; }
        public string NameCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public List<Category> CateCollection { get; set; }
    }
}
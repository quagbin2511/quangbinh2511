using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            Images = "~/Content/Images/ttlq4.jpg";
        }
        [Key]
        public int IDProduct { get; set; }
        public string NameProduct { get; set; }

        public Nullable<int> UnitPrice { get; set; }
        public string Images { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        public Nullable<System.DateTime> ProductDate { get; set; }
        public string Available { get; set; }

        public string Descriptions { get; set; }
        public Nullable<int> Quantity { get; set; }

        [ForeignKey("Category")]
        public int? IDCategory { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
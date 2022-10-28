using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.ComponentModel;

namespace ModelsLibrary.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(1024)]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public double Rate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int Quantity { get; set; }

        [ForeignKey("Producer")]
        public int ProducerId { get; set; }
        public virtual Producer Producer { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public IEnumerable<ProductImage> ProductImage { get; set; }
    }
}

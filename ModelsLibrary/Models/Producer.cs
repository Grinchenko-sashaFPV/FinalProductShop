using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("Producers")]
    public class Producer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public double Rate { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    [Serializable]
    [Table("ProductsImages")]
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        public byte[] Image { get; set; }
        
        public string FileExtension { get; set; }
        
        public decimal Size { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}

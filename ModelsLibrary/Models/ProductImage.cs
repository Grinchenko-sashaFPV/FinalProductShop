﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models
{
    [Table("ProductsImages")]
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] Image { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}

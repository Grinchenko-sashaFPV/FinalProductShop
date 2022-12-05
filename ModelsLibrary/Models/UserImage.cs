using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Serializable]
    [Table("UsersImages")]
    public class UserImage
    {
        [Key]
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public string FileExtension { get; set; }

        public decimal Size { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

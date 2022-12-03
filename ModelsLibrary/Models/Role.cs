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
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100)]
        public string Name { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}

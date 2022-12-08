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
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50)] // MAX length of md5 generators string is 32
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }
        public IEnumerable<UserImage> UserImages { get; set; }
        [NotMapped]
        public byte[] ImageBytes { get; set; }
        [NotMapped]
        public double MoneyAmount { get; set; }
    }
}

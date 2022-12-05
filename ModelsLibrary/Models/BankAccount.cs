using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ModelsLibrary.Models
{
    [Serializable]
    [Table("BankAccounts")]
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        [DefaultValue(10000)]
        public double MoneyAmount { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

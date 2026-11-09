using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFCORE_04.Models
{
    public class Account
    {
        public enum AccountType
        {
            Saving = 1,
            Current,
            Business
        }
        public enum AccountStatus
        {
            Active =1, 
            Closed
        }

        [Key]
        [StringLength(50)]
        public string AccountNumber { get; set; }
        [Column(TypeName="(decimal(18,2)")]
        public decimal CurrentBalance { get; set; }
        public AccountType accountType { get; set; }
        public DateTime OpeningDate { get; set; }
        public AccountStatus accountStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string BranchCode { get; set; }// Foreing Key For Branch 
        public Branch branch { get; set; }
        public ICollection<customerAccount> CustomerAccounts { get; set; } = new List<customerAccount>();
        public ICollection<Transaction> transactions { get; set; } = new List<Transaction>();

    }
}

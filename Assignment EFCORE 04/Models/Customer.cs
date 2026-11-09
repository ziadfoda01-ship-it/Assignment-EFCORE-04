using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFCORE_04.Models
{
    public class Customer
    {
        public enum CustomerType
        {
            Individual =1,
            Busienss
        }
        [Key]
        
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        [StringLength(20)]
        public string NationalId { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public DateTime DateOfBirth  { get; set; }
        [StringLength(20)]
        public string PhoneNumber  { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        public CustomerType customerType { get; set; }
        public ICollection<customerAccount> customerAccounts { get; set; } = new List<customerAccount>();

        
    }
}

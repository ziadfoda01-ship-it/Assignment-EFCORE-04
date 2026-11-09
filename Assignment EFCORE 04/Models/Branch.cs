using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFCORE_04.Models
{
    public class Branch
    {
        [Key]
        [StringLength(50)]

        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(20)]
        public string PhoneNumber  { get; set; }
        public int ManagerId   { get; set; }
        public Manager manager { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}

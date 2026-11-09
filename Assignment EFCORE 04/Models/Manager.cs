using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFCORE_04.Models
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public string Address { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public Branch branch { get; set; }

    }
}

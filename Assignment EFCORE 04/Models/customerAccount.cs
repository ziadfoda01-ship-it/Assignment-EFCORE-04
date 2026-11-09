using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_EFCORE_04.Models
{
    public class customerAccount
    {
        public enum OwnerShipRole
        { Primary = 1, 
        CoHolder = 2}
        public int CustomerId { get; set; }
        public Customer customer { get; set; }
        public string AccountNumber { get; set; }
        public Account Account { get; set; }
        public OwnerShipRole ownerShip { get; set; }
        public DateTime dateTime { get; set; }
    }
}

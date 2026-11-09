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
    public class Transaction
    {
        public enum TransactionType
        {
            Deposit =1, 
            Withdrawal ,
            Transfer 

        }
        [Key]
        public  int  TransactionNumber  { get; set; }
        public  DateTime  TransactionDate { get; set; }
        [Column(TypeName= "(decimal (18,2)")]
        public  decimal  Amount  { get; set; }
        public  TransactionType  transactionType  { get; set; }
        [StringLength(250)]
        public  string  Note  { get; set; }
        [Required]
        [StringLength(50)]
        public  string  AccountNumber   { get; set; }
        public  Account  account  { get; set; }
    }
}

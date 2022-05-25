using System.ComponentModel.DataAnnotations;

namespace BankDataWithMinimalAPI.Models
{
    public class BankAccount
    {
        [Key]
        public int AccountNo { get; set; }

        public string AccountName { get; set;}

        public string AccountType { get; set;}

        public Double Balance { get; set; }


    }
}

using System.Collections.Generic;

namespace BankProject.Models
{
 public class BankAccount
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public int Balance { get; set; }
        public string Type { get; set; }
        public virtual  List<BaseCard> Cards { get; set; }
        public User User { get; set; }
    }
}

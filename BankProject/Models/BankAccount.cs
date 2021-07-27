using System;
using System.Collections.Generic;
using System.Text;

namespace BankProject.Models
{
  class BankAccount
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public int Balance { get; set; }
        public string Type { get; set; }
        public virtual  List<Card> Cards { get; set; }
        public User User { get; set; }
    }
}

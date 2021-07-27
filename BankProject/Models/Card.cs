using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BankProject.Models
{
    class Card
    {
        public int Id { get; set; }
        
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public int Balance { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankProject
{
  class CreditAccountcs:BaseAccount
    {
        public CreditAccountcs():base()
        {
            Type = "Credit";
        }
    }
}

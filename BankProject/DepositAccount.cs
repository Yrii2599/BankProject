using System;
using System.Collections.Generic;
using System.Text;
using BankProject.Helpers;
using BankProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BankProject
{
    class DepositAccount:BaseAccount
    {
        public DepositAccount():base()
        {
            Type = "Deposit";
        }

    }
}

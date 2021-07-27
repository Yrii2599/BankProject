using System;
using System.Collections.Generic;
using System.Text;

namespace BankProject.Models
{
    class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string  Password { get; set; }
        public virtual  List<BaseAccount> BankAccounts { get; set; }
        public virtual  PersonalInfo PersonalInfo { get; set; }

        public User()
        {
            PersonalInfo = new PersonalInfo();
        }
    }
}

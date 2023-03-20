using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Core.Entities
{
    public class User
    {
        public User() 
        {
        }
        public User(string name, string email, string address, string phone, string userType, decimal money)
        {
            this.Name = name;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.UserType = userType;
            this.Money = money;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}

using Sat.Recruitment.Api.Core.Entities;
using Sat.Recruitment.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Infrastructure.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private string _path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Users.txt");

        public void ApplyPromotions(User user)
        {
            if (user.UserType == "Normal")
            {
                if (user.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    // If new user is normal and has more than USD100
                    var gift = user.Money * percentage;
                    user.Money += gift;
                }
                if (user.Money < 100 && user.Money > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gift = user.Money * percentage;
                    user.Money += gift;
                }
            }
            else if (user.UserType == "SuperUser")
            {
                if (user.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gift = user.Money * percentage;
                    user.Money += gift;
                }
            }
            else if (user.UserType == "Premium")
            {
                if (user.Money > 100)
                {
                    var gift = user.Money * 2;
                    user.Money += gift;
                }
            }
        }

        public List<User> getAllUsersFromFile()
        {
            List<User> getAllUsers = new List<User>();
            using (var fileStream = new FileStream(_path, FileMode.Open))
            using (var reader = new StreamReader(fileStream))
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;

                    var user = new User
                    (
                        line.Split(',')[0].ToString(),
                        line.Split(',')[1].ToString(),
                        line.Split(',')[3].ToString(),
                        line.Split(',')[2].ToString(),
                        line.Split(',')[4].ToString(),
                        decimal.Parse(line.Split(',')[5].ToString())
                    );
                    getAllUsers.Add(user);
                }
            }
            return getAllUsers;
        }
    }
}

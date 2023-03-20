using Sat.Recruitment.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Core.Interfaces
{
    public interface IUserRepository
    {
        List<User> getAllUsersFromFile();
        void ApplyPromotions(User user);
    }
}

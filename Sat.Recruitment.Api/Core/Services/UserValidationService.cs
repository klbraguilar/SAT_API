using Sat.Recruitment.Api.Core.Entities;
using Sat.Recruitment.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Core
{
    public class UserValidationService : IUserValidationService
    {
        public bool IsDuplicateUser(User userTovalidate, List<User> listOfUsersToValidate)
        {
            foreach (var user in listOfUsersToValidate)
            {
                if (user.Email == userTovalidate.Email || user.Phone == userTovalidate.Phone)
                {
                    return true;
                }
                else if (user.Name == userTovalidate.Name && user.Address == userTovalidate.Address)
                {
                    return true;
                }
            }

            return false;
        }

        public string ValidateErrors(User user, ref string errors)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                errors = "The name is required";
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                errors += " The email is required";
            }

            if (string.IsNullOrEmpty(user.Address))
            {
                errors += " The address is required";
            }

            if (string.IsNullOrEmpty(user.Phone))
            {
                errors += " The phone is required";
            }

            return errors;
        }
    }
}

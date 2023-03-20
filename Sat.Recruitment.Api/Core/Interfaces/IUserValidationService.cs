﻿using Sat.Recruitment.Api.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Core.Interfaces
{
    public interface IUserValidationService
    {
        String ValidateErrors(User user, ref string errors);
        bool IsDuplicateUser(User userTovalidate, List<User> listOfUsersToValidate);
    }
}

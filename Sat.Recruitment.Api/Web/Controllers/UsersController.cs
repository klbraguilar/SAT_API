using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Core.Entities;
using Sat.Recruitment.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> listOfUsers = new List<User>();
        private readonly IUserValidationService _userValidationService;
        private readonly INormalizeEmailService _normalizeEmailService;
        private readonly IUserRepository _userRepository;
        private readonly Result _result;
        public UsersController(IUserValidationService userValidationService, INormalizeEmailService normalizeEmailService, IUserRepository userRepository)
        {
            _userValidationService = userValidationService;
            _normalizeEmailService = normalizeEmailService;
            _userRepository = userRepository;
            listOfUsers = getAllUsersFromFile();
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(User userToCreate)
        {
            string errors = "";

            ValidateErrors(userToCreate, ref errors);

            if (errors.Length > 0)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = errors
                };
            }
            ApplyPromotions(userToCreate);

            NormalizeEmail(userToCreate.Email);

            try
            {
                if (IsDuplicateUser(userToCreate, listOfUsers))
                {
                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }

                Debug.WriteLine("User Created");

                return new Result()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating user: {ex.Message}");

                return new Result()
                {
                    IsSuccess = false,
                    Errors = "Error creating user"
                };
            }
        }

        private List<User> getAllUsersFromFile()
        {
            return _userRepository.getAllUsersFromFile();
        }

        //Validate errors
        private void ValidateErrors(User request, ref string errors)
        {

            _userValidationService.ValidateErrors(request, ref errors);
        }


        private void ApplyPromotions(User user)
        {
            _userRepository.ApplyPromotions(user);
        }

        private string NormalizeEmail(string email)
        {
            return _normalizeEmailService.NormalizeEmail(email);
        }

        private bool IsDuplicateUser(User newUser, List<User> listOfUserToValidate)
        {
            return _userValidationService.IsDuplicateUser(newUser, listOfUserToValidate);
        }
    }
}

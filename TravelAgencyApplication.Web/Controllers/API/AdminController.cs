﻿using Microsoft.AspNetCore.Mvc;
using TravelAgencyApplication.Domain.DTO;
using Microsoft.AspNetCore.Identity;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Model;
namespace TravelAgencyApplication.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<TAUser> _userManager;

        public AdminController(UserManager<TAUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("[action]")]
    public bool ImportAllUsers(List<UserRegistrationDTO> model)
    {
        bool status = true;

        foreach (var userDTO in model)
        {
            var userCheck = _userManager.FindByEmailAsync(userDTO.Email).Result;

            if (userCheck == null)
            {
                    var user = new TAUser
                    {
                        FirstName = userDTO.FirstName,
                        LastName = userDTO.LastName,
                        PhoneNumber = userDTO.PhoneNumber,
                        UserName = userDTO.Email,
                        NormalizedUserName = userDTO.Email,
                        Email = userDTO.Email,
                        EmailConfirmed = true,
                        UserRole = (Domain.Enum.UserRole)userDTO.UserRole,
                        Reservations = new List<Reservation>()
                    };

                var result = _userManager.CreateAsync(user, userDTO.Password).Result;
                status = status && result.Succeeded;
            }
            else
            {
                continue;
            }
        }
        return status;
    }

}
}
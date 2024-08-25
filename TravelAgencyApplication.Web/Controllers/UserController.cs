using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyApplication.Service.Interface;
using Microsoft.AspNet.Identity;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.ViewModel;
using Newtonsoft.Json;
using System.Text;
using ExcelDataReader;
using TravelAgencyApplication.Domain.DTO;
using TravelAgencyApplication.Domain.Model;
using System.Configuration;

namespace TravelAgencyApplication.Web.Controllers
{
    [Route("Admin/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            // Fetch the current user's user details
            if(userId == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var currentUser = _userService.GetDetailsForTAUser(userId);
            if(currentUser.UserRole != UserRole.ADMIN)
            {
                return Redirect("/Identity/Account/Login");

            }
            List<TAUser> users = _userService.GetAllTAUsers().ToList(); 

            var viewModel = new TAUserIndexViewModel
            {
                Users = users
            };

            return View(viewModel);
        }

        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(string? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetDetailsForTAUser(id);
            if (user == null)
            {
                return NotFound();
            }


            var userId = User.Identity.GetUserId();

            var currentUser = _userService.GetDetailsForTAUser(userId);

            if (userId != id && currentUser.UserRole != UserRole.ADMIN)
            {
                return Forbid();
            }
            return View(user);
        }

        [Route("Edit/{id?}")]
        public IActionResult Edit(string? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetDetailsForTAUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public IActionResult Edit(string id, [Bind("Id,FirstName,LastName,PhoneNumber")] TAUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.UpdateExistingTAUser(user);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(user);
        }


        // GET: TAUsers/Delete/5
        [Route("Delete/{id?}")]
        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetDetailsForTAUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id?}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var user = _userService.GetDetailsForTAUser(id);
            if (user == null)
            {
                return NotFound();
            }
            _userService.DeleteTAUser(id);
            return RedirectToAction(nameof(Index));
        }

        [Route("ImportUsers")]
        public IActionResult ImportUsers(IFormFile file)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\{file.FileName}";
            Console.WriteLine(pathToUpload);
            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            List<UserRegistrationDTO> users = getAllUsersFromFile(file.FileName);
            HttpClient client = new HttpClient();
            string URL = "https://localhost:7262/api/Admin/ImportAllUsers";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result = response.Content.ReadAsAsync<bool>().Result;

            return RedirectToAction("Index");

        }

        private List<UserRegistrationDTO> getAllUsersFromFile(string fileName)
        {
            List<UserRegistrationDTO> users = new List<UserRegistrationDTO>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        users.Add(new UserRegistrationDTO
                        {
                            FirstName = reader.GetValue(0)?.ToString(),
                            LastName = reader.GetValue(1)?.ToString(),
                            PhoneNumber = reader.GetValue(2)?.ToString(),
                            Email = reader.GetValue(3)?.ToString(),
                            Password = reader.GetValue(4)?.ToString(),
                            ConfirmPassword = reader.GetValue(5)?.ToString(),
                            UserRole = Convert.ToInt32(reader.GetValue(6)) 
                        });
                    }
                }
            }
            return users;
        }

        [Route("MakeTouristGuide")]
        public IActionResult MakeTouristGuide(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetDetailsForTAUser(id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserRole = UserRole.TRAVEL_GUIDE;

            _userService.UpdateExistingTAUser(user);

            return RedirectToAction(nameof(Index));
        }

    }
}

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

using TravelAgencyApplication.Service.Implementation;

namespace TravelAgencyApplication.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly AuthorizationService _authorizationService;

        public UserController(IUserService userService, AuthorizationService authorizationService)
        {
            _userService = userService;
            _authorizationService = authorizationService;
        }
        [Route("Admin/[controller]/Index")]
        [Route("Admin/[controller]")]
        public IActionResult Index()
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
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
        [Route("Admin/[controller]/Details/{id?}")]
        public async Task<IActionResult> Details(string? id)
        {
            
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            if (id == null)
            {
                return NotFound();
            }
            var user = _userService.GetDetailsForTAUser(id);

            return View(user);
        }
        public async Task<IActionResult> MyProfile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

          
            var userId = User.Identity.GetUserId();

            var currentUser = _userService.GetDetailsForTAUser(userId);
            return View(currentUser);
        }

        [Route("Admin/[controller]/Edit/{id?}")]
        public IActionResult Edit(string? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
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
        [Route("Admin/[controller]/Edit/{id}")]
        public IActionResult Edit(string id, [Bind("Id,FirstName,LastName,PhoneNumber")] TAUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
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
        [Route("Admin/[controller]/Delete/{id?}")]

        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
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
        [Route("Admin/[controller]/Delete/{id?}")]
        public IActionResult DeleteConfirmed(string id)
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }

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
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\{file.FileName}";

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

            return RedirectToAction(nameof(MyProfile));
        }

    }
}

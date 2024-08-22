using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyApplication.Service.Interface;
using Microsoft.AspNet.Identity;
using TravelAgencyApplication.Domain.Identity;
using TravelAgencyApplication.Domain.Enum;
using TravelAgencyApplication.Domain.ViewModel;

namespace TravelAgencyApplication.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            // Fetch the current user's user details
            var currentUser = _userService.GetDetailsForTAUser(userId);

            List<TAUser> users = _userService.GetAllTAUsers().ToList(); 

            var viewModel = new TAUserIndexViewModel
            {
                Users = users,
                CurrentUserRole = currentUser?.UserRole ?? UserRole.CUSTOMER
            };

            return View(viewModel);
        }
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
   
    }
}

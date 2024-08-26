using Microsoft.AspNetCore.Mvc;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Implementation;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    [Route("Admin/[controller]")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly AuthorizationService _authorizationService;

        public TagController(ITagService tagService, AuthorizationService authorizationService)
        {
            _tagService = tagService;
            _authorizationService = authorizationService;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            var tags = _tagService.GetAllTags();
            return View(tags);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Tag tag)
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            if (ModelState.IsValid)
            {
                _tagService.CreateNewTag(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        [Route("Edit/{id?}")]
        public IActionResult Edit(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            var tag = _tagService.GetDetailsForTag(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name")] Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            if (ModelState.IsValid)
            {
                _tagService.UpdateExistingTag(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        [Route("Delete/{id?}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            var tag = _tagService.GetDetailsForTag(id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (!_authorizationService.IsUserAuthorized(out var currentUser))
            {
                return Redirect("/Identity/Account/Login");
            }
            _tagService.DeleteTag(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

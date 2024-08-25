using Microsoft.AspNetCore.Mvc;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    [Route("Admin/[controller]")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var tags = _tagService.GetAllTags();
            return View(tags);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Tag tag)
        {
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
            _tagService.DeleteTag(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

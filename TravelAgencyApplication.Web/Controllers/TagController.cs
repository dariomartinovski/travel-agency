using Microsoft.AspNetCore.Mvc;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            var tags = _tagService.GetAllTags();
            return View(tags);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _tagService.DeleteTag(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

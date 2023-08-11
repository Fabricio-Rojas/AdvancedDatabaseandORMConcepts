using ControllersAndViews.Data;
using ControllersAndViews.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ControllersAndViews.Controllers
{
    public class StudentsController : Controller
    {
        private ControllersAndViewsContext _db;
        public StudentsController(ControllersAndViewsContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (_db.Students == null)
                return NotFound();
            List<Student> Students = _db.Students.ToList();
            return View(Students);
        }
        public IActionResult Details(Guid? id)
        {
            if (_db.Students == null || id == null)
                return NotFound();

            Student? student = _db.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Id,FullName,CourseId")] Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            _db.Students.Add(student);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(Guid? id)
        {
            if (_db.Students == null || id == null)
                return NotFound();

            Student? student = _db.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Guid id, [Bind("Id,FullName,CourseId")] Student student)
        {
            if (id != student.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(student);

            try
            {
                _db.Update(student);
                _db.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                if (!_db.Students.Any(s => s.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(Guid? id)
        {
            if (_db.Students == null || id == null)
                return NotFound();

            Student? student = _db.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (_db.Students == null)
                return NotFound();

            Student? student = _db.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            _db.Students.Remove(student);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

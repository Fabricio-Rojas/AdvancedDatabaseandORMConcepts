using DataAnnotation.Data;
using DataAnnotation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DataAnnotation.Controllers
{
    public class ClientsController : Controller
    {
        private DataAnnotationContext _db;
        public ClientsController(DataAnnotationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (_db.Clients == null)
                return NotFound();
            List<Client> Clients = _db.Clients.ToList();
            return View(Clients);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Id,FirstName,LastName,PhoneNumber")] Client client)
        {
            if (!ModelState.IsValid)
                return View(client);

            _db.Clients.Add(client);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (_db.Clients == null || id == null)
                return NotFound();

            Client? client = _db.Clients.FirstOrDefault(c => c.Id == id);

            if (client == null)
                return NotFound();

            return View(client);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber")] Client client)
        {
            if (id != client.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(client);

            try
            {
                _db.Update(client);
                _db.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                if (!_db.Clients.Any(c => c.Id == id))
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
    }
}

using DataAnnotation.Data;
using DataAnnotation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DataAnnotation.Controllers
{
    public class RoomsController : Controller
    {
        private DataAnnotationContext _db;
        public RoomsController(DataAnnotationContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (_db.Rooms == null)
                return NotFound();
            List<Room> Rooms = _db.Rooms.ToList();
            return View(Rooms);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("RoomNumber,Capacity,Section")] Room room)
        {
            if (!ModelState.IsValid)
                return View(room);

            _db.Rooms.Add(room);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? roomNumber)
        {
            if (_db.Rooms == null || roomNumber == null)
                return NotFound();

            Room? room = _db.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

            if (room == null)
                return NotFound();

            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(int roomNumber, [Bind("RoomNumber,Capacity,Section")] Room room)
        {
            if (roomNumber != room.RoomNumber)
                return NotFound();

            if (!ModelState.IsValid)
                return View(room);

            try
            {
                _db.Update(room);
                _db.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                if (!_db.Rooms.Any(r => r.RoomNumber == roomNumber))
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

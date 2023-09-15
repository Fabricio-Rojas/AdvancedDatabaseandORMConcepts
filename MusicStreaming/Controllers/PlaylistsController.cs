using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStreaming.Data;
using MusicStreaming.Models;

namespace MusicStreaming.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly MusicStreamingContext _db;

        public PlaylistsController(MusicStreamingContext context)
        {
            _db = context;
        }

        // GET: Playlists
        public async Task<IActionResult> Index()
        {
              return _db.Playlists != null ? 
                          View(await _db.Playlists.ToListAsync()) :
                          Problem("Entity set 'MusicStreamingContext.Playlists'  is null.");
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _db.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // GET: Playlists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                _db.Add(playlist);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = playlist.Id });
            }
            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _db.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Playlist playlist)
        {
            if (id != playlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(playlist);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(playlist.Id))
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
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _db.Playlists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Playlists == null)
            {
                return Problem("Entity set 'MusicStreamingContext.Playlists'  is null.");
            }
            var playlist = await _db.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _db.Playlists.Remove(playlist);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveSong(int? playlistId, int? songId)
        {
            if (playlistId == null || songId == null || _db.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _db.Playlists
                .Include(p => p.Songs)
                .FirstOrDefaultAsync(m => m.Id == playlistId);

            if (playlist == null)
            {
                return NotFound();
            }

            var songToRemove = playlist.Songs.FirstOrDefault(s => s.Id == songId);

            if (songToRemove == null)
            {
                return NotFound();
            }

            playlist.Songs.Remove(songToRemove);

            await _db.SaveChangesAsync();

            // Redirect back to the playlist details page
            return RedirectToAction(nameof(Details), new { id = playlistId });
        }

        private bool PlaylistExists(int id)
        {
          return (_db.Playlists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

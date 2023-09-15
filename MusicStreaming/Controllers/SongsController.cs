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
    public class SongsController : Controller
    {
        private readonly MusicStreamingContext _context;

        public SongsController(MusicStreamingContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var musicStreamingContext = _context.Songs.Include(s => s.SongArtists);
            return View(await musicStreamingContext.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Playlist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Title");
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Title");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,LengthInSeconds,AlbumId,PlaylistId")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Title", song.AlbumId);
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Title", song.PlaylistId);
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Title", song.AlbumId);
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Title", song.PlaylistId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,LengthInSeconds,AlbumId,PlaylistId")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Title", song.AlbumId);
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Title", song.PlaylistId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Playlist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Songs == null)
            {
                return Problem("Entity set 'MusicStreamingContext.Songs'  is null.");
            }
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ActionName("Add-To-Playlist")]
        public async Task<IActionResult> AddToPlaylist(int? songId)
        {
            if (songId == null || _context.Songs == null)
            {
                return NotFound();
            }

            Song? song = await _context.Songs.FindAsync(songId);
            if (song == null)
            {
                return NotFound();
            }

            var playlists = await _context.Playlists.ToListAsync();
            ViewData["PlaylistId"] = new SelectList(playlists, "Id", "Title");

            return View("AddToPlaylist", song);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToPlaylist(int songId, [Bind("Id,PlaylistId")] Song song)
        {
            if (songId != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the selected playlist
                    var selectedPlaylist = await _context.Playlists.FindAsync(song.PlaylistId);

                    // Attach the song to the selected playlist
                    selectedPlaylist.Songs.Add(song);

                    _context.Update(selectedPlaylist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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

            var playlists = await _context.Playlists.ToListAsync();
            ViewData["PlaylistId"] = new SelectList(playlists, "Id", "Title", song.PlaylistId);

            return View(song);
        }
        private bool SongExists(int id)
        {
          return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

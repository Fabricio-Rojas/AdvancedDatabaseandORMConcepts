using Microsoft.EntityFrameworkCore;
using MusicStreaming.Models;

namespace MusicStreaming.Data
{
    public class MusicStreamingContext : DbContext
    {
        public MusicStreamingContext(DbContextOptions<MusicStreamingContext> options) : base(options) { }

        public DbSet<Song> Songs { get; set; } = default!;
        public DbSet<Album> Albums { get; set; } = default!;
        public DbSet<Artist> Artists { get; set; } = default!;
        public DbSet<Playlist> Playlists { get; set; } = default!;
        public DbSet<SongArtists> SongsArtists { get; set;} = default!;
    }
}

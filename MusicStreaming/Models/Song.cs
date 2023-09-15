using System.ComponentModel.DataAnnotations;

namespace MusicStreaming.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Required]
        [Display( Name = "Length in Seconds")]
        public int LengthInSeconds { get; set; }

        [Required]
        public int AlbumId { get; set; }
        public Album Album { get; set;}

        [Required]
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

        public HashSet<SongArtists> SongArtists { get; set;}
    }
}

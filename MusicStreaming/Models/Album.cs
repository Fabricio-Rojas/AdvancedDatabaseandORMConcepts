using System.ComponentModel.DataAnnotations;

namespace MusicStreaming.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Year { get; set; }/* = new DateTime(2001, 1, 1);*/

        public HashSet<Song> Songs { get; set; }
    }
}

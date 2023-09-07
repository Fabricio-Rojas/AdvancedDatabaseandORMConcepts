using System.ComponentModel.DataAnnotations;

namespace DataAnnotation.Models
{
    public class Room
    {
        [Key]
        [Display(Name = "Room Number")]
        public int RoomNumber { get; set; }

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, 6, ErrorMessage = "Capacity must be between 1 and 6.")]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Section is required.")]
        [Display(Name = "Section")]
        public Section Section { get; set; }
    }

    public enum Section
    {
        [Display(Name = "First")]
        First,

        [Display(Name = "Second")]
        Second,

        [Display(Name = "Third")]
        Third
    }
}

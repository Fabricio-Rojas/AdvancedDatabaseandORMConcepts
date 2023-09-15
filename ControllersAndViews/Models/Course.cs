using Microsoft.Build.Framework;

namespace ControllersAndViews.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public HashSet<Student> Students { get; set; } = new HashSet<Student>();
    }
}

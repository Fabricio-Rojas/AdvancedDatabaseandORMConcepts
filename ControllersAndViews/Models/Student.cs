using System;
using System.ComponentModel.DataAnnotations;

namespace ControllersAndViews.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        [Required]
        public string FullName { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}

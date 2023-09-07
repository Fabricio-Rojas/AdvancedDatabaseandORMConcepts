using System.ComponentModel.DataAnnotations;

namespace DataAnnotation.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First Name must be between 3 and 25 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 and 25 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format. Use 10 digits.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}

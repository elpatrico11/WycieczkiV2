using System.ComponentModel.DataAnnotations;

namespace WycieczkiV2.ViewModel
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }


        public string PhoneNumber { get; set; }


        public string Email { get; set; }

        [MaxLength(50)]
        public string Citizenship { get; set; }
    }
}

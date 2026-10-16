using System.ComponentModel.DataAnnotations;

namespace Electronic_Clearance_Platform.Models
{
    public class ClearanceRequest
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Student / Employee Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Identification Number is required")]
        [Display(Name = "Identification Number")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; } = DateTime.Now;


        [Display(Name = "Request Status")]
        public string Status { get; set; } = "Pending";
        public bool IsLibraryCleared { get; set; } = false;
        public bool IsFinanceCleared { get; set; } = false;
        public bool IsStudentAffairsCleared { get; set; } = false;
        public bool IsDeptCleared { get; set; } = false;
    }
    }
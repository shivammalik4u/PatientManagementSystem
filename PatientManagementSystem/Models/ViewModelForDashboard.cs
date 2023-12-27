using System.ComponentModel.DataAnnotations;

namespace PatientManagementSystem.Models
{
    public class ViewModelForDashboard
    {
        [Display(Name = "Patient Id")]
        public int Id { get; set; }
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }
        [Display(Name = "Staff Name")]
        public string StaffName { get; set; }
        [Display(Name = "Lab Bill")]
        public decimal LabBill { get; set; }
        [Display(Name = "Pharmacy Bill")]
        public decimal PharmacyBill { get; set; }
    }
}

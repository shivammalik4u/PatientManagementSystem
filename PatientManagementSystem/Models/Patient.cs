using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagementSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        // One-to-Many relationship with LabBilling
        [InverseProperty("Patient")]
        public List<LabBilling> LabBillings { get; set; }

        // One-to-Many relationship with PharmacyBilling
        [InverseProperty("Patient")]
        public List<PharmacyBilling> PharmacyBillings { get; set; }

        [InverseProperty("Patient")]
        public List<Staff> StaffMembers { get; set; }
    }
}

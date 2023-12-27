using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagementSystem.Models
{
    public class LabBilling
    {
        public int Id { get; set; }
        public DateTime BillingDate { get; set; }
        public decimal Amount { get; set; }
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}

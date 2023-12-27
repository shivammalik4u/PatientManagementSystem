using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagementSystem.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}

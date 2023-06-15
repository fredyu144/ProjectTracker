using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeProjectTrackerAPI.Data
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        [Column("Project")]
        public string ProjectName { get; set; }
        public string Role { get; set; }
    }
}

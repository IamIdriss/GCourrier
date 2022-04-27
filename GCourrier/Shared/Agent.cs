using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GCourrier.Shared
{
    public class Agent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Le prenom nom d'agent")]
        public string? FirstName { get; set; }
        [Required]
        [Display(Name = "Le nom d'agent")]
        public string? LastName { get; set; }
        public string? Email { get; set; }
       
        public Power Power { get; set; }

        [Required]
        [ForeignKey("Department")]
        [Display(Name = "Id de son Departement")]
        public int DepartmentId { get; set; }
        public string? PhotoPath { get; set; }
        public Department? Department { get; set; }
    }
}

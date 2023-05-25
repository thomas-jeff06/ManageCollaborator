using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageCollaborator.Models
{
    public class Collaborator : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaboratorId { get; set; }
        public int CompanyId { get; set; }
        public int PositionId { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Matricula { get; set; } 
        public DateTime AdmissionDate { get; set; }
        public DateTime? LayoffDate { get; set; }
        
    }
}

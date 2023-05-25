using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageCollaborator.Models
{
    public class CollaboratorHistoric : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaboratorHistoricId { get; set; }
        public int collaboratorId { get; set; }
        public string OldPosition { get; set; }
        public string NewPosition { get; set; }
    }
}

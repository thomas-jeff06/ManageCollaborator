using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageCollaborator.Models
{
    public class Position : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PositionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }   
        public string CBO { get; set; }
    }
}

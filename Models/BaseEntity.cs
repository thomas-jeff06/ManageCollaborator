using Microsoft.VisualBasic;

namespace ManageCollaborator.Models
{
    public class BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DeleteDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManageCollaborator.Models
{
    public class Company : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public string FantasyName { get; set; }
        public string CNPJ { get; set; } 
        public string CorporateName { get; set;}
    }
}

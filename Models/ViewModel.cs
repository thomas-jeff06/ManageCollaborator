namespace ManageCollaborator.Models
{
    public class ViewModel
    {
        public List<Collaborator> Collaborators { get; set; }
        public Company Company { get; set; }
        public List<Position> Positions { get; set; }
        public Position ChoicePosition { get; set; }
        public Collaborator NewCollaborator { get; set; }
        public int CompanyId { get; set; }
        public List<Position> PositionsCollaborators { get; set; }

    }
}

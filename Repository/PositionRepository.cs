using ManageCollaborator.Context;
using ManageCollaborator.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.Design;
using System.Collections.Generic;

namespace ManageCollaborator.Repository
{
    public class PositionRepository
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlite("Data Source=bdsqliteproject.db")
        .Options;

        public List<Position> GetPositions()
        {
            using (var dbContext = new ApplicationDbContext(options))
            {
                return dbContext.Position.ToList();
            }
        }

        public Position GetPositionById(int positionId)
        {
            using (var dbContext = new ApplicationDbContext(options))
            {
                return dbContext.Position.Find(positionId);
            }
        }

        public List<Position> GetPositionsByCollaborators(List<Collaborator> collaborators)
        {
            List<Position> PositionsCollaborators = new List<Position>();

            foreach (var collaborator in collaborators)
            {
                using (var dbContext = new ApplicationDbContext(options))
                {
                    PositionsCollaborators.Add(GetPositionById(collaborator.PositionId));
                }
            }
            return PositionsCollaborators;
        }
    }
}

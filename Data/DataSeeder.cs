using System.Drawing.Drawing2D;
using System.Drawing;
using FollowingErrors.Entities;

namespace FollowingErrors.Data
{
    public class DataSeeder
    {
        private readonly BugsManager _dbContext;
        const int limit = 5;

        public DataSeeder(BugsManager dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedData()
        {

            if (!_dbContext.Project.Any())
            {
                
                Project[] projects = new Project[limit];
                for (int i = 0; i < limit; i++)
                {
                    projects[i] = new Project
                    {
                        Name = $"Project {i+1}"
                    };
                }
                _dbContext.Project.AddRange(projects);
                _dbContext.SaveChanges();
            }
            if (!_dbContext.User.Any())
            {
                User[] users = new User[limit];
                for (int i = 0; i < limit; i++)
                {
                    users[i] = new User
                    {
                        Name = $"User {i + 1}",
                        Surname = $"Surname {i+1}"
                    };
                }
                _dbContext.User.AddRange(users);
                _dbContext.SaveChanges();
            }
        }
    }
}

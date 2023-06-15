namespace EmployeeProjectTrackerAPI.Repository
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly ApplicationDbContext _db;
        public ProjectRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<Project> GetProjects() => _db.Projects;
        public IQueryable<Project> GetProjects(Func<Project, bool> query) => _db.Projects.Where(query).AsQueryable();
    }
}

namespace EmployeeProjectTrackerAPI.Repository
{
    public interface IProjectRepository
    {
        IQueryable<Project> GetProjects();
        IQueryable<Project> GetProjects(Func<Project, bool> query);
    }
}

namespace EmployeeProjectTrackerAPI.Manager
{
    public interface IProjectService
    {
        Task<SearchProjectResponse> SearchProject(string query, int page, int pageSize);
    }
}

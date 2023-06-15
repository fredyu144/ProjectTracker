namespace EmployeeProjectTrackerAPI.Models
{
    public class SearchProjectResponse
    {
        public List<Project> Projects { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
    }
}

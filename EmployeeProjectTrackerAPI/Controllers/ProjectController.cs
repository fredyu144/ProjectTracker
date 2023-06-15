using Microsoft.AspNetCore.Mvc;

namespace EmployeeProjectTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string query, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("please provide something to search");
            }
            return Ok(await _projectService.SearchProject(query, page, pageSize));
        }
    }
}

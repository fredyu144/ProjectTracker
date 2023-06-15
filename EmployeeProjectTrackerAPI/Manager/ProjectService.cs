using System.Text.RegularExpressions;

namespace EmployeeProjectTrackerAPI.Manager
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<SearchProjectResponse> SearchProject(string query, int page, int pageSize)
        {
            string[] searchTerms = Regex.Split(query, @"(?i) AND |, ").Select(term => term.Trim().ToLower()).ToArray();
            Func<Project, bool> projectQuery = project => true;
            Func<Project, bool> projectOrQuery = null;
            foreach (string term in searchTerms)
            {
                bool isNegated = term.StartsWith('-') || term.StartsWith("not");
                bool isExactSearch = term.StartsWith('"') && term.EndsWith('"');

                string keyword = term.Replace("not ", "").Trim('-', '"','(', ')').ToLower();

                // Perform the search based on the search term
                if (isNegated)
                {
                    if (isExactSearch)
                    {
                        projectQuery = projectQuery.ComposeAnd(project => !IsAnyPropertyEqualTo(project, keyword));
                    }
                    else
                    {
                        projectQuery = projectQuery.ComposeAnd(project => !IsAnyPropertyContains(project, keyword));
                    }
                }
                else
                {
                    if (isExactSearch)
                    {
                        projectQuery = projectQuery.ComposeAnd(project => IsAnyPropertyEqualTo(project, keyword));
                    }
                    else if (Regex.IsMatch(keyword, @"(?i) OR |\| "))
                    {
                        string[] orTerms = Regex.Split(term, @"(?i) OR |\| ").Select(term => term.Trim('-', '"').Trim('(', ')')).ToArray();
                        foreach(string orTerm in orTerms)
                        {
                            bool isOrKeywordNegated = orTerm.StartsWith('-');
                            bool isOrKeywordExactSearch = orTerm.StartsWith('"') && term.EndsWith('"');

                            string orKeyword = orTerm.Trim('-', '"').ToLower();
                            if (isOrKeywordNegated)
                            {
                                if (isOrKeywordExactSearch)
                                {
                                    projectOrQuery = projectOrQuery.ComposeOr(project => !IsAnyPropertyEqualTo(project, orKeyword));
                                }
                                else
                                {
                                    var asd = _projectRepository
                                       .GetProjects()
                                       .AsEnumerable()
                                       .Where(project => !IsAnyPropertyContains(project, orKeyword))
                                       .ToList();
                                    projectOrQuery = projectOrQuery.ComposeOr(project => !IsAnyPropertyContains(project, orKeyword));
                                }
                            }
                            else
                            {
                                if (isOrKeywordExactSearch)
                                {
                                    projectOrQuery = projectOrQuery.ComposeOr(project => IsAnyPropertyEqualTo(project, orKeyword));
                                }
                                else
                                {
                                    projectOrQuery = projectOrQuery.ComposeOr(project => IsAnyPropertyContains(project, orKeyword));
                                }
                            }
                        }
                    }
                    else
                    {
                        projectQuery = projectQuery.ComposeAnd(project => IsAnyPropertyContains(project, keyword));
                    }
                }
            }
            IEnumerable<Project>? result = _projectRepository
                .GetProjects()
                .AsEnumerable()
                .Where(x => projectQuery.Invoke(x)
                    && (projectOrQuery is null ? true : projectOrQuery.Invoke(x)));
            List<Project>? pagedResult = result
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new SearchProjectResponse()
            {
                Projects = pagedResult,
                CurrentPage = page, 
                Pages = (result.Count()/pageSize)+1, 
                PageSize = pageSize, 
                Total = result.Count()
            };
        }

        private bool IsAnyPropertyEqualTo(Project project, string keyword)
        {
            return project.Name.Equals(keyword, StringComparison.InvariantCultureIgnoreCase)
                || project.Company.Equals(keyword, StringComparison.InvariantCultureIgnoreCase)
                || project.ProjectName.Equals(keyword, StringComparison.InvariantCultureIgnoreCase)
                || project.Role.Equals(keyword, StringComparison.InvariantCultureIgnoreCase);
        }

        private  bool IsAnyPropertyContains(Project project, string keyword)
        {
            return project.Name.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                || project.Company.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                || project.ProjectName.Contains(keyword, StringComparison.InvariantCultureIgnoreCase)
                || project.Role.Contains(keyword, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}

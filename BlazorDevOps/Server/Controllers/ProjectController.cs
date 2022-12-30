using BlazorDevOps.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.TeamFoundation.Core.WebApi;

namespace BlazorDevOps.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly VssConnectionAdapter _connectionAdapter;

        public ProjectController(
            ILogger<ProjectController> logger,
            VssConnectionAdapter adapter)
        {
            _logger = logger;
            _connectionAdapter = adapter;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectItem>> Get()
        {
            try
            {
                var connection = await _connectionAdapter.GetConnection();
                var client = connection.GetClient<ProjectHttpClient>();

                var results = await client.GetProjects();
                return results.Select(p => new ProjectItem { Name = p.Name, Url = p.Url })
                    .ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch data from ADO.");
                return Array.Empty<ProjectItem>();
            }

        }
        
    }
}

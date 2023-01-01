using BlazorDevOps.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

namespace BlazorDevOps.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestPlanController : ControllerBase
{
    private readonly ILogger<TestPlanController> _logger;
    private readonly VssConnectionAdapter _connectionAdapter;

    public TestPlanController(
        ILogger<TestPlanController> logger,
        VssConnectionAdapter adapter)
    {
        _logger = logger;
        _connectionAdapter = adapter;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string projectId)
    {
        try
        {
            var projectGuid = Guid.Parse(projectId);
            var connection = await _connectionAdapter.GetConnection();
            var testClient = connection.GetClient<TestPlanHttpClient>();

            var results = await testClient.GetTestPlansAsync(projectGuid);
            return new OkObjectResult(results.Select(p => new TestPlanItem { Name = p.Name })
                .ToArray());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch data from ADO.");
            return new BadRequestResult();
        }

    }
}

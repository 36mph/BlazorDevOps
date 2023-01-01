namespace BlazorDevOps.Shared;

public class ProjectItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
}

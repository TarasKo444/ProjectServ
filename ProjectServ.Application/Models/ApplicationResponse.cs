namespace ProjectServ.Application.Models;

public class ApplicationResponse
{
    public Guid Id { get; set; }
    public string CarNumber { get; set; } = null!;
    public string CarBrand { get; set; } = null!;
    public DateTime TimeOfArrival { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? TimeOfAcceptance  { get; set; }
    public DateTime? ClosedAt  { get; set; }
    public string UserName { get; set; } = null!;
    public string? MasterName { get; set; } = null!;
    public Guid UserId { get; set; }
    public Guid? MasterId { get; set; }
    public string ProblemDescription { get; set; } = null!;
    public string CurrentStatus { get; set; } = null!;
}
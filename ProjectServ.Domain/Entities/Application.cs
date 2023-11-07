
using ProjectServ.Domain.Enums;

namespace ProjectServ.Domain.Entities;

public class Application
{
    public Guid Id { get; set; }
    
    public string CarNumber { get; set; } = null!;
    public string CarBrand { get; set; } = null!;
    public DateTime TimeOfArrival { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? TimeOfAcceptance  { get; set; }
    public DateTime? ClosedAt  { get; set; }
    public string ProblemDescription { get; set; } = null!;
    public string CurrentStatus { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public Guid? MasterId { get; set; }
    public AppUser User { get; set; } = null!;
    public AppUser? Master { get; set; }
}
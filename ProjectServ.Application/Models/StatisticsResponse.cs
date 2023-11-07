namespace ProjectServ.Application.Models;

public class StatisticsResponse
{
    public int UsersCount { get; set; }
    public int MastersCount { get; set; }
    public int ApplicationsCount { get; set; }
    public int WaitingApplicationsCount { get; set; }
    public int InWorkApplicationsCount { get; set; }
    public int ClosedApplicationsCount { get; set; }
}

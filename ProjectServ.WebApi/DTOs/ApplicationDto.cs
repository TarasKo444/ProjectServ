namespace LadaServ.WebApi.DTOs;

public class ApplicationDto
{
    public string? CarNumber { get; set; }
    public string? CarBrand { get; set; }
    public DateTime? TimeOfArrival { get; set; }
    public string? ProblemDescription { get; set; }
}

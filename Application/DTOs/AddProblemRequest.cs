namespace Application.DTOs;

public class AddProblemRequest
{
    public string ProblemName { get; set; }
    public Location Location { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    
    public string Image { get; set; }

}

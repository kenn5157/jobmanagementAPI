
public class Problem
{
    public int ProblemId { get; set; }
    public string ProblemName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }

    public string Image { get; set; }

}
public class ProblemResponse
{
    public List<Problem> Problems { get; set;}
}

public class Problem
{
    public int ProblemId { get; set; }
    public string ProblemName { get; set; }
    public Location Location { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }

    public string Image { get; set; }

    public static ProblemDB ConvertToProblemDB(Problem problem) {
        return new ProblemDB {
                ProblemId = problem.ProblemId,
                ProblemName = problem.ProblemName,
                Latitude = problem.Location.Latitude,
                Longitude = problem.Location.Longitude,
                Status = problem.Status,
                Description = problem.Description,
                Image = problem.Image
        };
    }
}
public class ProblemResponse
{
    public List<Problem> Problems { get; set;}
}
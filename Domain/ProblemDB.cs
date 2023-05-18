public class ProblemDB
{
    public int ProblemId { get; set; }
    public string ProblemName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }

    public string Image { get; set; }

    public static List<Problem> ConvertToProblems(List<ProblemDB> dbEntities){
        List<Problem> problems = new List<Problem>();

        foreach (var dbEntity in dbEntities) {
            Problem problem = new Problem {
                ProblemId = dbEntity.ProblemId,
                ProblemName = dbEntity.ProblemName,
                Location = new Location{
                    Latitude = dbEntity.Latitude,
                    Longitude = dbEntity.Longitude,
                },
                Status = dbEntity.Status,
                Description = dbEntity.Description,
                Image = dbEntity.Image
            };
            problems.Add(problem);
        }
        return problems;
    }

    public static Problem ConvertToProblem(ProblemDB problemDB) {
        return new Problem{
            ProblemId = problemDB.ProblemId,
                ProblemName = problemDB.ProblemName,
                Location = new Location{
                    Latitude = problemDB.Latitude,
                    Longitude = problemDB.Longitude,
                },
                Status = problemDB.Status,
                Description = problemDB.Description,
                Image = problemDB.Image

        };
    }
}
public class ProblemDBResponse
{
    public List<ProblemDB> ProblemDBs { get; set;}
}
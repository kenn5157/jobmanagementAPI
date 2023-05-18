using Application.Interfaces;

namespace Infrastructure;

public class ProblemRepository: IProblemRepository
{
    private readonly DatabaseContext _dbContext;

    public ProblemRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext ?? throw new NullReferenceException("DatabaseContext can not be null.");
    }
        
    public List<ProblemDB> GetAllProblems()
    {
        return _dbContext.ProblemTable.ToList();
    }

    public ProblemDB GetById(int ProblemId)
    {
        return _dbContext.ProblemTable.Find(ProblemId);
    }

    public ProblemDB AddProblem(ProblemDB problem)
    {
        _dbContext.ProblemTable.Add(problem);
        _dbContext.SaveChanges();
        return _dbContext.ProblemTable.Find(problem.ProblemId);
    }

    public ProblemDB? EditProblem(ProblemDB problem)
    {
        _dbContext.ProblemTable.Update(problem);
        _dbContext.SaveChanges();
        return _dbContext.ProblemTable.Find(problem.ProblemId);
    }

    public int DeleteProblem(int id)
    {
        ProblemDB problem = _dbContext.ProblemTable.Find(id);
        _dbContext.ProblemTable.Remove(problem);
        return _dbContext.SaveChanges();
    }
}
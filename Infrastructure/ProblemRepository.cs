using Application.DTOs;
using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class ProblemRepository: IProblemRepository
{
    private readonly DatabaseContext _dbContext;

    public ProblemRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext ?? throw new NullReferenceException("DatabaseContext can not be null.");
    }
        
    public List<Problem> GetAllProblems()
    {
        return _dbContext.ProblemTable.ToList();
    }

    public Problem GetById(int ProblemId)
    {
        return _dbContext.ProblemTable.Find(ProblemId);
    }

    public Problem AddProblem(Problem problem)
    {
        _dbContext.ProblemTable.Add(problem);
        _dbContext.SaveChanges();
        return _dbContext.ProblemTable.Find(problem.ProblemId);
    }

    public Problem? EditProblem(Problem problem)
    {
        _dbContext.ProblemTable.Update(problem);
        _dbContext.SaveChanges();
        return _dbContext.ProblemTable.Find(problem.ProblemId);
    }

    public int DeleteProblem(int id)
    {
        Problem problem = _dbContext.ProblemTable.Find(id);
        _dbContext.ProblemTable.Remove(problem);
        return _dbContext.SaveChanges();
    }
}
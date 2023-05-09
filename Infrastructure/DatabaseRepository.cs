using Infrastructure.Interface;

namespace Infrastructure;

public class DatabaseRepository : IDatabaseRepository
{
    private readonly DatabaseContext _dbContext;
    
    public DatabaseRepository(DatabaseContext dbContext) 
    {
        _dbContext = dbContext ?? 
                     throw new NullReferenceException("DatabaseContext can not be null.");
    }

    public void buildDB()
    {
        _dbContext.Database.EnsureCreated();
    }
}
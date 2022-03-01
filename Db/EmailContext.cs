using BadgeMeUp.Models;

namespace BadgeMeUp.Db;

public class EmailQueueDb
{
    private readonly BadgeContext _db;

    public EmailQueueDb(BadgeContext dbContext)
    {
        _db = dbContext;
    }

    public async Task QueueEmailToSend(EmailQueue email)
    {
        await _db.EmailQueue.AddAsync(email);
        await _db.SaveChangesAsync();
    }
}
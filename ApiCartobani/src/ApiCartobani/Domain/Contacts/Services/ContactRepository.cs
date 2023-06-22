namespace ApiCartobani.Domain.Contacts.Services;

using ApiCartobani.Domain.Contacts;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IContactRepository : IGenericRepository<Contact>
{
}

public sealed class ContactRepository : GenericRepository<Contact>, IContactRepository
{
    private readonly CartobaniDbContext _dbContext;

    public ContactRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}

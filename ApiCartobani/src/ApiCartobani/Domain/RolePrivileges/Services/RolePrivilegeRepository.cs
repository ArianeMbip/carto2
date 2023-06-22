namespace ApiCartobani.Domain.RolePrivileges.Services;

using ApiCartobani.Domain.RolePrivileges;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IRolePrivilegeRepository : IGenericRepository<RolePrivilege>
{
}

public sealed class RolePrivilegeRepository : GenericRepository<RolePrivilege>, IRolePrivilegeRepository
{
    private readonly CartobaniDbContext _dbContext;

    public RolePrivilegeRepository(CartobaniDbContext dbContext) : base(dbContext, "RolePrivileges")
    {
        _dbContext = dbContext;
    }
}

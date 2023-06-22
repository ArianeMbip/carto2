namespace ApiCartobani.Domain.InterfacesUtilisateur.Services;

using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Databases;
using ApiCartobani.Services;

public interface IInterfaceUtilisateurRepository : IGenericRepository<InterfaceUtilisateur>
{
}

public sealed class InterfaceUtilisateurRepository : GenericRepository<InterfaceUtilisateur>, IInterfaceUtilisateurRepository
{
    private readonly CartobaniDbContext _dbContext;

    public InterfaceUtilisateurRepository(CartobaniDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}

namespace ApiCartobani.Databases;

using ApiCartobani.Domain;
using ApiCartobani.Databases.EntityConfigurations;
using ApiCartobani.Services;
using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.DAs;
using ApiCartobani.Domain.Icones;
using ApiCartobani.Domain.Universs;
using ApiCartobani.Domain.Flux;
using ApiCartobani.Domain.Composants;
using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.Environnements;
using ApiCartobani.Domain.GestionnaireActifs;
using ApiCartobani.Domain.RolePrivileges;
using MediatR;

using ApiCartobani.Domain.TypeElements;
using ApiCartobani.Domain.Attributs;
using ApiCartobani.Domain.ValeurAttributs;
using ApiCartobani.Domain.Actifs;
using ApiCartobani.Domain.DAs;
using ApiCartobani.Domain.Icones;
using ApiCartobani.Domain.Universs;
using ApiCartobani.Domain.Flux;
using ApiCartobani.Domain.Composants;
using ApiCartobani.Domain.InterfacesUtilisateur;
using ApiCartobani.Domain.Fonctionnalites;
using ApiCartobani.Domain.Contacts;
using ApiCartobani.Domain.PiecesJointes;
using ApiCartobani.Domain.Historiques;
using ApiCartobani.Domain.Environnements;
using ApiCartobani.Domain.GestionnaireActifs;
using ApiCartobani.Domain.RolePrivileges;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.Query;
//using MongoFramework;
//using Microsoft.EntityFrameworkCore.Storage;
//using Microsoft.Extensions.Options;
using ParkBee.MongoDb;
using MongoDB.Driver;

//public sealed class CartobaniDbContext : DbContext

public interface ICartobaniDbContext
{
}

public sealed class CartobaniDbContext : ICartobaniDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;
    private readonly IDateTimeProvider _dateTimeProvider;
    public readonly IMongoDatabase _database;

    //public CartobaniDbContext(
    //    DbContextOptions<CartobaniDbContext> options, ICurrentUserService currentUserService, IMediator mediator, IDateTimeProvider dateTimeProvider) 
    //        : base(options)

    //public CartobaniDbContext(IMongoContextOptionsBuilder optionsBuilder, ICurrentUserService currentUserService, IMediator mediator, IDateTimeProvider dateTimeProvider)
    public CartobaniDbContext(ICurrentUserService currentUserService, IMediator mediator, IDateTimeProvider dateTimeProvider)

    {
        _currentUserService = currentUserService;
        _mediator = mediator;
        _dateTimeProvider = dateTimeProvider;

        //var mongoClient = new MongoClient("mongodb://localhost:27017");
        var mongoClient = new MongoClient("mongodb://172.21.6.30:27017/perco_om_test?maxPoolSize=300");
        _database = mongoClient.GetDatabase("cartowraptdb");

        //_database = new MongoClient("mongodb://localhost:27017").GetDatabase("CartoDb");

    }

    //public IMongoCollection<User> Users { get; set; }
    //public DbSet<User> UsersSet { get; set; }

    // protected override async Task OnConfiguring()
    // {
    //     await OptionsBuilder.Entity<User>(async entity =>
    //     {
    //         var usersCollection = entity.ToCollection("ApplicationUsers");
    //         var searchByEmail = new CreateIndexModel<Permit>(Builders<User>.IndexKeys
    //             .Ascending(u => u.Email));

    //         await permitsCollection.Indexes.CreateOneAsync(searchByEmail);

    //         entity.HasKey(p => p.UserId);
    //     });
    // }

    public int SaveChanges()
    {
        //UpdateAuditFields();
        //var result = base.SaveChanges();
        //_dispatchDomainEvents().GetAwaiter().GetResult();
        return 1; // result;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        //UpdateAuditFields();
        //var result = await base.SaveChangesAsync(cancellationToken);
        //await _dispatchDomainEvents();
        return 1; // result;
    }
}



     //#region DbSet Region - Do Not Delete
     //public DbSet<TypeElement> TypeElements { get; set; }
     //public DbSet<Attribut> Attributs { get; set; }
     //public DbSet<ValeurAttribut> ValeurAttributs { get; set; }
     //public DbSet<Actif> Actifs { get; set; }
     //public DbSet<DA> DAs { get; set; }
     //public DbSet<Icone> Icones { get; set; }
     //public DbSet<Univers> Univers { get; set; }
     //public DbSet<Flux> Flux { get; set; }
     //public DbSet<Composant> Composants { get; set; }
     //public DbSet<InterfaceUtilisateur> InterfacesUtilisateur { get; set; }
     //public DbSet<Fonctionnalite> Fonctionnalites { get; set; }
     //public DbSet<Contact> Contacts { get; set; }
     //public DbSet<PieceJointe> PiecesJointes { get; set; }
     //public DbSet<Historique> Historiques { get; set; }
     //public DbSet<Environnement> Environnements { get; set; }
     //public DbSet<GestionnaireActif> GestionnaireActif { get; set; }
     //public DbSet<RolePrivilege> RolePrivileges { get; set; }
     //public DbSet<UserRole> UserRoles { get; set; }
     //public DbSet<User> Users { get; set; }
     //public DbSet<RolePermission> RolePermissions { get; set; }
     //#endregion DbSet Region - Do Not Delete

     //protected override void OnModelCreating(ModelBuilder modelBuilder)
     //{
     //    base.OnModelCreating(modelBuilder);
     //    modelBuilder.FilterSoftDeletedRecords();
     //    /* any query filters added after this will override soft delete 
     //            https://docs.microsoft.com/en-us/ef/core/querying/filters
     //            https://github.com/dotnet/efcore/issues/10275
     //    */
    /*
        #region Entity Database Config Region - Only delete if you don't want to automatically add configurations
        modelBuilder.ApplyConfiguration(new TypeElementConfiguration());
        modelBuilder.ApplyConfiguration(new AttributConfiguration());
        modelBuilder.ApplyConfiguration(new ValeurAttributConfiguration());
        modelBuilder.ApplyConfiguration(new ActifConfiguration());
        modelBuilder.ApplyConfiguration(new DAConfiguration());
        modelBuilder.ApplyConfiguration(new IconeConfiguration());
        modelBuilder.ApplyConfiguration(new UniversConfiguration());
        modelBuilder.ApplyConfiguration(new FluxConfiguration());
        modelBuilder.ApplyConfiguration(new ComposantConfiguration());
        modelBuilder.ApplyConfiguration(new InterfaceUtilisateurConfiguration());
        modelBuilder.ApplyConfiguration(new FonctionnaliteConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new PieceJointeConfiguration());
        modelBuilder.ApplyConfiguration(new HistoriqueConfiguration());
        modelBuilder.ApplyConfiguration(new EnvironnementConfiguration());
        modelBuilder.ApplyConfiguration(new GestionnaireActifConfiguration());
        modelBuilder.ApplyConfiguration(new RolePrivilegeConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
        #endregion Entity Database Config Region - Only delete if you don't want to automatically add configurations
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        var result = base.SaveChanges();
        _dispatchDomainEvents().GetAwaiter().GetResult();
        return result;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateAuditFields();
        var result = await base.SaveChangesAsync(cancellationToken);
        await _dispatchDomainEvents();
        return result;
    }
    
    private async Task _dispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<BaseEntity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.DomainEvents.Clear();
            foreach (var entityDomainEvent in events)
                await _mediator.Publish(entityDomainEvent);
        }
    }
        
    private void UpdateAuditFields()
    {
        var now = _dateTimeProvider.DateTimeUtcNow;
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.UpdateCreationProperties(now, _currentUserService?.UserId);
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService?.UserId);
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService?.UserId);
                    break;
                
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService?.UserId);
                    entry.Entity.UpdateIsDeleted(true);
                    break;
            }
        }
    }


/*
public static class Extensions
{
    public static void FilterSoftDeletedRecords(this ModelBuilder modelBuilder)
    {
        Expression<Func<BaseEntity, bool>> filterExpr = e => !e.IsDeleted;
        foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes()
            .Where(m => m.ClrType.IsAssignableTo(typeof(BaseEntity))))
        {
            // modify expression to handle correct child type
            var parameter = Expression.Parameter(mutableEntityType.ClrType);
            var body = ReplacingExpressionVisitor
                .Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
            var lambdaExpression = Expression.Lambda(body, parameter);

            // set filter
            mutableEntityType.SetQueryFilter(lambdaExpression);
        }
    }
}*/

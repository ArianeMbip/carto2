namespace ApiCartobani.IntegrationTests.FeatureTests.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.Domain.Fonctionnalites.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteFonctionnaliteCommandTests : TestBase
{
    [Test]
    public async Task can_delete_fonctionnalite_from_db()
    {
        // Arrange
        var fakeFonctionnaliteOne = FakeFonctionnalite.Generate(new FakeFonctionnaliteForCreationDto().Generate());
        await InsertAsync(fakeFonctionnaliteOne);
        var fonctionnalite = await ExecuteDbContextAsync(db => db.Fonctionnalites
            .FirstOrDefaultAsync(f => f.Id == fakeFonctionnaliteOne.Id));

        // Act
        var command = new DeleteFonctionnalite.Command(fonctionnalite.Id);
        await SendAsync(command);
        var fonctionnaliteResponse = await ExecuteDbContextAsync(db => db.Fonctionnalites.CountAsync(f => f.Id == fonctionnalite.Id));

        // Assert
        fonctionnaliteResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_fonctionnalite_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteFonctionnalite.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_fonctionnalite_from_db()
    {
        // Arrange
        var fakeFonctionnaliteOne = FakeFonctionnalite.Generate(new FakeFonctionnaliteForCreationDto().Generate());
        await InsertAsync(fakeFonctionnaliteOne);
        var fonctionnalite = await ExecuteDbContextAsync(db => db.Fonctionnalites
            .FirstOrDefaultAsync(f => f.Id == fakeFonctionnaliteOne.Id));

        // Act
        var command = new DeleteFonctionnalite.Command(fonctionnalite.Id);
        await SendAsync(command);
        var deletedFonctionnalite = await ExecuteDbContextAsync(db => db.Fonctionnalites
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == fonctionnalite.Id));

        // Assert
        deletedFonctionnalite?.IsDeleted.Should().BeTrue();
    }
}
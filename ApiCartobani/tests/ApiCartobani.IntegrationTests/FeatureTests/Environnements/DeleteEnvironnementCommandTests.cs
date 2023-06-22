namespace ApiCartobani.IntegrationTests.FeatureTests.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.Domain.Environnements.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteEnvironnementCommandTests : TestBase
{
    [Test]
    public async Task can_delete_environnement_from_db()
    {
        // Arrange
        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnementOne);
        var environnement = await ExecuteDbContextAsync(db => db.Environnements
            .FirstOrDefaultAsync(e => e.Id == fakeEnvironnementOne.Id));

        // Act
        var command = new DeleteEnvironnement.Command(environnement.Id);
        await SendAsync(command);
        var environnementResponse = await ExecuteDbContextAsync(db => db.Environnements.CountAsync(e => e.Id == environnement.Id));

        // Assert
        environnementResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_environnement_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteEnvironnement.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_environnement_from_db()
    {
        // Arrange
        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnementOne);
        var environnement = await ExecuteDbContextAsync(db => db.Environnements
            .FirstOrDefaultAsync(e => e.Id == fakeEnvironnementOne.Id));

        // Act
        var command = new DeleteEnvironnement.Command(environnement.Id);
        await SendAsync(command);
        var deletedEnvironnement = await ExecuteDbContextAsync(db => db.Environnements
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == environnement.Id));

        // Assert
        deletedEnvironnement?.IsDeleted.Should().BeTrue();
    }
}
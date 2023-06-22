namespace ApiCartobani.IntegrationTests.FeatureTests.DAs;

using ApiCartobani.SharedTestHelpers.Fakes.DA;
using ApiCartobani.Domain.DAs.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;

public class DeleteDACommandTests : TestBase
{
    [Test]
    public async Task can_delete_da_from_db()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeDAOne = FakeDA.Generate(new FakeDAForCreationDto()
            .RuleFor(d => d.IdActif, _ => fakeActifOne.Id).Generate());
        await InsertAsync(fakeDAOne);
        var dA = await ExecuteDbContextAsync(db => db.DAs
            .FirstOrDefaultAsync(d => d.Id == fakeDAOne.Id));

        // Act
        var command = new DeleteDA.Command(dA.Id);
        await SendAsync(command);
        var dAResponse = await ExecuteDbContextAsync(db => db.DAs.CountAsync(d => d.Id == dA.Id));

        // Assert
        dAResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_da_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteDA.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_da_from_db()
    {
        // Arrange
        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifOne);

        var fakeDAOne = FakeDA.Generate(new FakeDAForCreationDto()
            .RuleFor(d => d.IdActif, _ => fakeActifOne.Id).Generate());
        await InsertAsync(fakeDAOne);
        var dA = await ExecuteDbContextAsync(db => db.DAs
            .FirstOrDefaultAsync(d => d.Id == fakeDAOne.Id));

        // Act
        var command = new DeleteDA.Command(dA.Id);
        await SendAsync(command);
        var deletedDA = await ExecuteDbContextAsync(db => db.DAs
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == dA.Id));

        // Assert
        deletedDA?.IsDeleted.Should().BeTrue();
    }
}
namespace ApiCartobani.IntegrationTests.FeatureTests.Actifs;

using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.Domain.Actifs.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;
using ApiCartobani.SharedTestHelpers.Fakes.Actif;

public class DeleteActifCommandTests : TestBase
{
    [Test]
    public async Task can_delete_actif_from_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.TypeActif, _ => fakeTypeElementOne.Id)
            .RuleFor(a => a.PreVersion, _ => fakeActifParentOne.Id)
            .RuleFor(a => a.Hierarchie, _ => fakeActifParentOne.Id).Generate());
        await InsertAsync(fakeActifOne);
        var actif = await ExecuteDbContextAsync(db => db.Actifs
            .FirstOrDefaultAsync(a => a.Id == fakeActifOne.Id));

        // Act
        var command = new DeleteActif.Command(actif.Id);
        await SendAsync(command);
        var actifResponse = await ExecuteDbContextAsync(db => db.Actifs.CountAsync(a => a.Id == actif.Id));

        // Assert
        actifResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_actif_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteActif.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_actif_from_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        fakeActifParentOne = FakeActif.Generate(new FakeActifForCreationDto().Generate());
        await InsertAsync(fakeActifParentOne);

        var fakeActifOne = FakeActif.Generate(new FakeActifForCreationDto()
            .RuleFor(a => a.TypeActif, _ => fakeTypeElementOne.Id)
            .RuleFor(a => a.PreVersion, _ => fakeActifParentOne.Id)
            .RuleFor(a => a.Hierarchie, _ => fakeActifParentOne.Id).Generate());
        await InsertAsync(fakeActifOne);
        var actif = await ExecuteDbContextAsync(db => db.Actifs
            .FirstOrDefaultAsync(a => a.Id == fakeActifOne.Id));

        // Act
        var command = new DeleteActif.Command(actif.Id);
        await SendAsync(command);
        var deletedActif = await ExecuteDbContextAsync(db => db.Actifs
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == actif.Id));

        // Assert
        deletedActif?.IsDeleted.Should().BeTrue();
    }
}
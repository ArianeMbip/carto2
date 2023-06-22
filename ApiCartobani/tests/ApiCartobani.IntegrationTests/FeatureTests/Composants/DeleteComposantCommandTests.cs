namespace ApiCartobani.IntegrationTests.FeatureTests.Composants;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using ApiCartobani.Domain.Composants.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

public class DeleteComposantCommandTests : TestBase
{
    [Test]
    public async Task can_delete_composant_from_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeComposantOne = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementOne.Id).Generate());
        await InsertAsync(fakeComposantOne);
        var composant = await ExecuteDbContextAsync(db => db.Composants
            .FirstOrDefaultAsync(c => c.Id == fakeComposantOne.Id));

        // Act
        var command = new DeleteComposant.Command(composant.Id);
        await SendAsync(command);
        var composantResponse = await ExecuteDbContextAsync(db => db.Composants.CountAsync(c => c.Id == composant.Id));

        // Assert
        composantResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_composant_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteComposant.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_composant_from_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeComposantOne = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementOne.Id).Generate());
        await InsertAsync(fakeComposantOne);
        var composant = await ExecuteDbContextAsync(db => db.Composants
            .FirstOrDefaultAsync(c => c.Id == fakeComposantOne.Id));

        // Act
        var command = new DeleteComposant.Command(composant.Id);
        await SendAsync(command);
        var deletedComposant = await ExecuteDbContextAsync(db => db.Composants
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == composant.Id));

        // Assert
        deletedComposant?.IsDeleted.Should().BeTrue();
    }
}
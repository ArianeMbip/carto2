namespace ApiCartobani.IntegrationTests.FeatureTests.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.Domain.Attributs.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteAttributCommandTests : TestBase
{
    [Test]
    public async Task can_delete_attribut_from_db()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttributOne);
        var attribut = await ExecuteDbContextAsync(db => db.Attributs
            .FirstOrDefaultAsync(a => a.Id == fakeAttributOne.Id));

        // Act
        var command = new DeleteAttribut.Command(attribut.Id);
        await SendAsync(command);
        var attributResponse = await ExecuteDbContextAsync(db => db.Attributs.CountAsync(a => a.Id == attribut.Id));

        // Assert
        attributResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_attribut_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteAttribut.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_attribut_from_db()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttributOne);
        var attribut = await ExecuteDbContextAsync(db => db.Attributs
            .FirstOrDefaultAsync(a => a.Id == fakeAttributOne.Id));

        // Act
        var command = new DeleteAttribut.Command(attribut.Id);
        await SendAsync(command);
        var deletedAttribut = await ExecuteDbContextAsync(db => db.Attributs
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == attribut.Id));

        // Assert
        deletedAttribut?.IsDeleted.Should().BeTrue();
    }
}
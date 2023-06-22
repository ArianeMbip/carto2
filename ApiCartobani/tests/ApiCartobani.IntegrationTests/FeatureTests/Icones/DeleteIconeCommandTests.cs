namespace ApiCartobani.IntegrationTests.FeatureTests.Icones;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using ApiCartobani.Domain.Icones.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteIconeCommandTests : TestBase
{
    [Test]
    public async Task can_delete_icone_from_db()
    {
        // Arrange
        var fakeIconeOne = FakeIcone.Generate(new FakeIconeForCreationDto().Generate());
        await InsertAsync(fakeIconeOne);
        var icone = await ExecuteDbContextAsync(db => db.Icones
            .FirstOrDefaultAsync(i => i.Id == fakeIconeOne.Id));

        // Act
        var command = new DeleteIcone.Command(icone.Id);
        await SendAsync(command);
        var iconeResponse = await ExecuteDbContextAsync(db => db.Icones.CountAsync(i => i.Id == icone.Id));

        // Assert
        iconeResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_icone_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteIcone.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_icone_from_db()
    {
        // Arrange
        var fakeIconeOne = FakeIcone.Generate(new FakeIconeForCreationDto().Generate());
        await InsertAsync(fakeIconeOne);
        var icone = await ExecuteDbContextAsync(db => db.Icones
            .FirstOrDefaultAsync(i => i.Id == fakeIconeOne.Id));

        // Act
        var command = new DeleteIcone.Command(icone.Id);
        await SendAsync(command);
        var deletedIcone = await ExecuteDbContextAsync(db => db.Icones
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == icone.Id));

        // Assert
        deletedIcone?.IsDeleted.Should().BeTrue();
    }
}
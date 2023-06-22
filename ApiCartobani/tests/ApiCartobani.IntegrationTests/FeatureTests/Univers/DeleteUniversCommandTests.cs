namespace ApiCartobani.IntegrationTests.FeatureTests.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.Domain.Univers.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteUniversCommandTests : TestBase
{
    [Test]
    public async Task can_delete_univers_from_db()
    {
        // Arrange
        var fakeUniversOne = FakeUnivers.Generate(new FakeUniversForCreationDto().Generate());
        await InsertAsync(fakeUniversOne);
        var univers = await ExecuteDbContextAsync(db => db.Univers
            .FirstOrDefaultAsync(u => u.Id == fakeUniversOne.Id));

        // Act
        var command = new DeleteUnivers.Command(univers.Id);
        await SendAsync(command);
        var universResponse = await ExecuteDbContextAsync(db => db.Univers.CountAsync(u => u.Id == univers.Id));

        // Assert
        universResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_univers_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteUnivers.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_univers_from_db()
    {
        // Arrange
        var fakeUniversOne = FakeUnivers.Generate(new FakeUniversForCreationDto().Generate());
        await InsertAsync(fakeUniversOne);
        var univers = await ExecuteDbContextAsync(db => db.Univers
            .FirstOrDefaultAsync(u => u.Id == fakeUniversOne.Id));

        // Act
        var command = new DeleteUnivers.Command(univers.Id);
        await SendAsync(command);
        var deletedUnivers = await ExecuteDbContextAsync(db => db.Univers
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == univers.Id));

        // Assert
        deletedUnivers?.IsDeleted.Should().BeTrue();
    }
}
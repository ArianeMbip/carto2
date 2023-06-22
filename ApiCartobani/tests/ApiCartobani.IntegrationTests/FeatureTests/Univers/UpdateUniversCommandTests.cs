namespace ApiCartobani.IntegrationTests.FeatureTests.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using ApiCartobani.Domain.Univers.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Univers.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateUniversCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_univers_in_db()
    {
        // Arrange
        var fakeUniversOne = FakeUnivers.Generate(new FakeUniversForCreationDto().Generate());
        var updatedUniversDto = new FakeUniversForUpdateDto().Generate();
        await InsertAsync(fakeUniversOne);

        var univers = await ExecuteDbContextAsync(db => db.Univers
            .FirstOrDefaultAsync(u => u.Id == fakeUniversOne.Id));
        var id = univers.Id;

        // Act
        var command = new UpdateUnivers.Command(id, updatedUniversDto);
        await SendAsync(command);
        var updatedUnivers = await ExecuteDbContextAsync(db => db.Univers.FirstOrDefaultAsync(u => u.Id == id));

        // Assert
        updatedUnivers.Nom.Should().Be(updatedUniversDto.Nom);
    }
}
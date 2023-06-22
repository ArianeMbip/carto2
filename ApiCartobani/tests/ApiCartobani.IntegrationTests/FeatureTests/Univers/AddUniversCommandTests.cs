namespace ApiCartobani.IntegrationTests.FeatureTests.Univers;

using ApiCartobani.SharedTestHelpers.Fakes.Univers;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.Univers.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddUniversCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_univers_to_db()
    {
        // Arrange
        var fakeUniversOne = new FakeUniversForCreationDto().Generate();

        // Act
        var command = new AddUnivers.Command(fakeUniversOne);
        var universReturned = await SendAsync(command);
        var universCreated = await ExecuteDbContextAsync(db => db.Univers
            .FirstOrDefaultAsync(u => u.Id == universReturned.Id));

        // Assert
        universReturned.Nom.Should().Be(fakeUniversOne.Nom);

        universCreated.Nom.Should().Be(fakeUniversOne.Nom);
    }
}
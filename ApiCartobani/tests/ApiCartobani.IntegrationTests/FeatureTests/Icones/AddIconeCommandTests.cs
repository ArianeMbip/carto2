namespace ApiCartobani.IntegrationTests.FeatureTests.Icones;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.Icones.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddIconeCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_icone_to_db()
    {
        // Arrange
        var fakeIconeOne = new FakeIconeForCreationDto().Generate();

        // Act
        var command = new AddIcone.Command(fakeIconeOne);
        var iconeReturned = await SendAsync(command);
        var iconeCreated = await ExecuteDbContextAsync(db => db.Icones
            .FirstOrDefaultAsync(i => i.Id == iconeReturned.Id));

        // Assert
        iconeReturned.Url.Should().Be(fakeIconeOne.Url);

        iconeCreated.Url.Should().Be(fakeIconeOne.Url);
    }
}
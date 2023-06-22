namespace ApiCartobani.IntegrationTests.FeatureTests.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.Environnements.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddEnvironnementCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_environnement_to_db()
    {
        // Arrange
        var fakeEnvironnementOne = new FakeEnvironnementForCreationDto().Generate();

        // Act
        var command = new AddEnvironnement.Command(fakeEnvironnementOne);
        var environnementReturned = await SendAsync(command);
        var environnementCreated = await ExecuteDbContextAsync(db => db.Environnements
            .FirstOrDefaultAsync(e => e.Id == environnementReturned.Id));

        // Assert
        environnementReturned.Nom.Should().Be(fakeEnvironnementOne.Nom);

        environnementCreated.Nom.Should().Be(fakeEnvironnementOne.Nom);
    }
}
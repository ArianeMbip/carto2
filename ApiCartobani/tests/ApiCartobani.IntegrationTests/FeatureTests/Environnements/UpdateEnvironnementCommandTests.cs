namespace ApiCartobani.IntegrationTests.FeatureTests.Environnements;

using ApiCartobani.SharedTestHelpers.Fakes.Environnement;
using ApiCartobani.Domain.Environnements.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Environnements.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateEnvironnementCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_environnement_in_db()
    {
        // Arrange
        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        var updatedEnvironnementDto = new FakeEnvironnementForUpdateDto().Generate();
        await InsertAsync(fakeEnvironnementOne);

        var environnement = await ExecuteDbContextAsync(db => db.Environnements
            .FirstOrDefaultAsync(e => e.Id == fakeEnvironnementOne.Id));
        var id = environnement.Id;

        // Act
        var command = new UpdateEnvironnement.Command(id, updatedEnvironnementDto);
        await SendAsync(command);
        var updatedEnvironnement = await ExecuteDbContextAsync(db => db.Environnements.FirstOrDefaultAsync(e => e.Id == id));

        // Assert
        updatedEnvironnement.Nom.Should().Be(updatedEnvironnementDto.Nom);
    }
}
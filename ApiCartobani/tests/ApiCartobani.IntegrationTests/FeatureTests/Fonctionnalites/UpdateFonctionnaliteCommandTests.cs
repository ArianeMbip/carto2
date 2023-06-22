namespace ApiCartobani.IntegrationTests.FeatureTests.Fonctionnalites;

using ApiCartobani.SharedTestHelpers.Fakes.Fonctionnalite;
using ApiCartobani.Domain.Fonctionnalites.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Fonctionnalites.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateFonctionnaliteCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_fonctionnalite_in_db()
    {
        // Arrange
        var fakeFonctionnaliteOne = FakeFonctionnalite.Generate(new FakeFonctionnaliteForCreationDto().Generate());
        var updatedFonctionnaliteDto = new FakeFonctionnaliteForUpdateDto().Generate();
        await InsertAsync(fakeFonctionnaliteOne);

        var fonctionnalite = await ExecuteDbContextAsync(db => db.Fonctionnalites
            .FirstOrDefaultAsync(f => f.Id == fakeFonctionnaliteOne.Id));
        var id = fonctionnalite.Id;

        // Act
        var command = new UpdateFonctionnalite.Command(id, updatedFonctionnaliteDto);
        await SendAsync(command);
        var updatedFonctionnalite = await ExecuteDbContextAsync(db => db.Fonctionnalites.FirstOrDefaultAsync(f => f.Id == id));

        // Assert
        updatedFonctionnalite.Nom.Should().Be(updatedFonctionnaliteDto.Nom);
        updatedFonctionnalite.Type.Should().Be(updatedFonctionnaliteDto.Type);
    }
}
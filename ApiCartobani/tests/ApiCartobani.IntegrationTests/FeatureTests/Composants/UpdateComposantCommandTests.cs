namespace ApiCartobani.IntegrationTests.FeatureTests.Composants;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using ApiCartobani.Domain.Composants.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Composants.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

public class UpdateComposantCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_composant_in_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeComposantOne = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementOne.Id).Generate());
        var updatedComposantDto = new FakeComposantForUpdateDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementOne.Id).Generate();
        await InsertAsync(fakeComposantOne);

        var composant = await ExecuteDbContextAsync(db => db.Composants
            .FirstOrDefaultAsync(c => c.Id == fakeComposantOne.Id));
        var id = composant.Id;

        // Act
        var command = new UpdateComposant.Command(id, updatedComposantDto);
        await SendAsync(command);
        var updatedComposant = await ExecuteDbContextAsync(db => db.Composants.FirstOrDefaultAsync(c => c.Id == id));

        // Assert
        updatedComposant.Nom.Should().Be(updatedComposantDto.Nom);
        updatedComposant.TypeComposant.Should().Be(updatedComposantDto.TypeComposant);
    }
}
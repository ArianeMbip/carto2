namespace ApiCartobani.IntegrationTests.FeatureTests.ValeurAttributs;

using ApiCartobani.SharedTestHelpers.Fakes.ValeurAttribut;
using ApiCartobani.Domain.ValeurAttributs.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.ValeurAttributs.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.SharedTestHelpers.Fakes.Environnement;

public class UpdateValeurAttributCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_valeurattribut_in_db()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttributOne);

        var fakeEnvironnementOne = FakeEnvironnement.Generate(new FakeEnvironnementForCreationDto().Generate());
        await InsertAsync(fakeEnvironnementOne);

        var fakeValeurAttributOne = FakeValeurAttribut.Generate(new FakeValeurAttributForCreationDto()
            .RuleFor(v => v.Attribut, _ => fakeAttributOne.Id)
            .RuleFor(v => v.Environnement, _ => fakeEnvironnementOne.Id).Generate());
        var updatedValeurAttributDto = new FakeValeurAttributForUpdateDto()
            .RuleFor(v => v.Attribut, _ => fakeAttributOne.Id)
            .RuleFor(v => v.Environnement, _ => fakeEnvironnementOne.Id).Generate();
        await InsertAsync(fakeValeurAttributOne);

        var valeurAttribut = await ExecuteDbContextAsync(db => db.ValeurAttributs
            .FirstOrDefaultAsync(v => v.Id == fakeValeurAttributOne.Id));
        var id = valeurAttribut.Id;

        // Act
        var command = new UpdateValeurAttribut.Command(id, updatedValeurAttributDto);
        await SendAsync(command);
        var updatedValeurAttribut = await ExecuteDbContextAsync(db => db.ValeurAttributs.FirstOrDefaultAsync(v => v.Id == id));

        // Assert
        updatedValeurAttribut.Valeur.Should().Be(updatedValeurAttributDto.Valeur);
        updatedValeurAttribut.Attribut.Should().Be(updatedValeurAttributDto.Attribut);
        updatedValeurAttribut.Environnement.Should().Be(updatedValeurAttributDto.Environnement);
    }
}
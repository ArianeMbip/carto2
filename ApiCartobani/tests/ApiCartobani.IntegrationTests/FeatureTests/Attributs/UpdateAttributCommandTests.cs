namespace ApiCartobani.IntegrationTests.FeatureTests.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.Domain.Attributs.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Attributs.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateAttributCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_attribut_in_db()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        var updatedAttributDto = new FakeAttributForUpdateDto().Generate();
        await InsertAsync(fakeAttributOne);

        var attribut = await ExecuteDbContextAsync(db => db.Attributs
            .FirstOrDefaultAsync(a => a.Id == fakeAttributOne.Id));
        var id = attribut.Id;

        // Act
        var command = new UpdateAttribut.Command(id, updatedAttributDto);
        await SendAsync(command);
        var updatedAttribut = await ExecuteDbContextAsync(db => db.Attributs.FirstOrDefaultAsync(a => a.Id == id));

        // Assert
        updatedAttribut.Nom.Should().Be(updatedAttributDto.Nom);
        updatedAttribut.Requis.Should().Be(updatedAttributDto.Requis);
        updatedAttribut.TypeDonnee.Should().Be(updatedAttributDto.TypeDonnee);
    }
}
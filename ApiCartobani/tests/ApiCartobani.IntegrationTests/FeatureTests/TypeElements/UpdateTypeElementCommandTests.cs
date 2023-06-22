namespace ApiCartobani.IntegrationTests.FeatureTests.TypeElements;

using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;
using ApiCartobani.Domain.TypeElements.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.TypeElements.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateTypeElementCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_typeelement_in_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        var updatedTypeElementDto = new FakeTypeElementForUpdateDto().Generate();
        await InsertAsync(fakeTypeElementOne);

        var typeElement = await ExecuteDbContextAsync(db => db.TypeElements
            .FirstOrDefaultAsync(t => t.Id == fakeTypeElementOne.Id));
        var id = typeElement.Id;

        // Act
        var command = new UpdateTypeElement.Command(id, updatedTypeElementDto);
        await SendAsync(command);
        var updatedTypeElement = await ExecuteDbContextAsync(db => db.TypeElements.FirstOrDefaultAsync(t => t.Id == id));

        // Assert
        updatedTypeElement.Nom.Should().Be(updatedTypeElementDto.Nom);
        updatedTypeElement.Type.Should().Be(updatedTypeElementDto.Type);
        updatedTypeElement.Icone.Should().Be(updatedTypeElementDto.Icone);
    }
}
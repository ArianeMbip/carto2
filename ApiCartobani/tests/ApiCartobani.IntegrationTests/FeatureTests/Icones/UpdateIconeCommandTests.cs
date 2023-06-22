namespace ApiCartobani.IntegrationTests.FeatureTests.Icones;

using ApiCartobani.SharedTestHelpers.Fakes.Icone;
using ApiCartobani.Domain.Icones.Dtos;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Icones.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateIconeCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_icone_in_db()
    {
        // Arrange
        var fakeIconeOne = FakeIcone.Generate(new FakeIconeForCreationDto().Generate());
        var updatedIconeDto = new FakeIconeForUpdateDto().Generate();
        await InsertAsync(fakeIconeOne);

        var icone = await ExecuteDbContextAsync(db => db.Icones
            .FirstOrDefaultAsync(i => i.Id == fakeIconeOne.Id));
        var id = icone.Id;

        // Act
        var command = new UpdateIcone.Command(id, updatedIconeDto);
        await SendAsync(command);
        var updatedIcone = await ExecuteDbContextAsync(db => db.Icones.FirstOrDefaultAsync(i => i.Id == id));

        // Assert
        updatedIcone.Url.Should().Be(updatedIconeDto.Url);
    }
}
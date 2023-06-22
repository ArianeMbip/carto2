namespace ApiCartobani.IntegrationTests.FeatureTests.Composants;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiCartobani.Domain.Composants.Features;
using static TestFixture;
using SharedKernel.Exceptions;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

public class AddComposantCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_composant_to_db()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeComposantOne = new FakeComposantForCreationDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementOne.Id).Generate();

        // Act
        var command = new AddComposant.Command(fakeComposantOne);
        var composantReturned = await SendAsync(command);
        var composantCreated = await ExecuteDbContextAsync(db => db.Composants
            .FirstOrDefaultAsync(c => c.Id == composantReturned.Id));

        // Assert
        composantReturned.Nom.Should().Be(fakeComposantOne.Nom);
        composantReturned.TypeComposant.Should().Be(fakeComposantOne.TypeComposant);

        composantCreated.Nom.Should().Be(fakeComposantOne.Nom);
        composantCreated.TypeComposant.Should().Be(fakeComposantOne.TypeComposant);
    }
}
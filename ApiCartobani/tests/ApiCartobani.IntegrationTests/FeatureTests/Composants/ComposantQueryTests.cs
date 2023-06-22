namespace ApiCartobani.IntegrationTests.FeatureTests.Composants;

using ApiCartobani.SharedTestHelpers.Fakes.Composant;
using ApiCartobani.Domain.Composants.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;
using ApiCartobani.SharedTestHelpers.Fakes.TypeElement;

public class ComposantQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_composant_with_accurate_props()
    {
        // Arrange
        var fakeTypeElementOne = FakeTypeElement.Generate(new FakeTypeElementForCreationDto().Generate());
        await InsertAsync(fakeTypeElementOne);

        var fakeComposantOne = FakeComposant.Generate(new FakeComposantForCreationDto()
            .RuleFor(c => c.TypeComposant, _ => fakeTypeElementOne.Id).Generate());
        await InsertAsync(fakeComposantOne);

        // Act
        var query = new GetComposant.Query(fakeComposantOne.Id);
        var composant = await SendAsync(query);

        // Assert
        composant.Nom.Should().Be(fakeComposantOne.Nom);
        composant.TypeComposant.Should().Be(fakeComposantOne.TypeComposant);
    }

    [Test]
    public async Task get_composant_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetComposant.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
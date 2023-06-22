namespace ApiCartobani.IntegrationTests.FeatureTests.Attributs;

using ApiCartobani.SharedTestHelpers.Fakes.Attribut;
using ApiCartobani.Domain.Attributs.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class AttributQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_attribut_with_accurate_props()
    {
        // Arrange
        var fakeAttributOne = FakeAttribut.Generate(new FakeAttributForCreationDto().Generate());
        await InsertAsync(fakeAttributOne);

        // Act
        var query = new GetAttribut.Query(fakeAttributOne.Id);
        var attribut = await SendAsync(query);

        // Assert
        attribut.Nom.Should().Be(fakeAttributOne.Nom);
        attribut.Requis.Should().Be(fakeAttributOne.Requis);
        attribut.TypeDonnee.Should().Be(fakeAttributOne.TypeDonnee);
    }

    [Test]
    public async Task get_attribut_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetAttribut.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
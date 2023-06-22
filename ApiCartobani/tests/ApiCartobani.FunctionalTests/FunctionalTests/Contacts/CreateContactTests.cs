namespace ApiCartobani.FunctionalTests.FunctionalTests.Contacts;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateContactTests : TestBase
{
    [Test]
    public async Task create_contact_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeContact = new FakeContactForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Contacts.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeContact);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
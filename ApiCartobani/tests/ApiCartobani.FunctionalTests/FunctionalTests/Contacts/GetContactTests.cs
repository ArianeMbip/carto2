namespace ApiCartobani.FunctionalTests.FunctionalTests.Contacts;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetContactTests : TestBase
{
    [Test]
    public async Task get_contact_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeContact = FakeContact.Generate(new FakeContactForCreationDto().Generate());
        await InsertAsync(fakeContact);

        // Act
        var route = ApiRoutes.Contacts.GetRecord.Replace(ApiRoutes.Contacts.Id, fakeContact.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
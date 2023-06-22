namespace ApiCartobani.FunctionalTests.FunctionalTests.Contacts;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateContactRecordTests : TestBase
{
    [Test]
    public async Task put_contact_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeContact = FakeContact.Generate(new FakeContactForCreationDto().Generate());
        var updatedContactDto = new FakeContactForUpdateDto().Generate();
        await InsertAsync(fakeContact);

        // Act
        var route = ApiRoutes.Contacts.Put.Replace(ApiRoutes.Contacts.Id, fakeContact.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedContactDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
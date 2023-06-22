namespace ApiCartobani.FunctionalTests.FunctionalTests.Contacts;

using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using ApiCartobani.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetContactListTests : TestBase
{
    [Test]
    public async Task get_contact_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Contacts.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
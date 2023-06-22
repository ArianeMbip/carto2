namespace ApiCartobani.IntegrationTests.FeatureTests.Contacts;

using ApiCartobani.Domain.Contacts.Dtos;
using ApiCartobani.SharedTestHelpers.Fakes.Contact;
using SharedKernel.Exceptions;
using ApiCartobani.Domain.Contacts.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class ContactListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_contact_list()
    {
        // Arrange
        var fakeContactOne = FakeContact.Generate(new FakeContactForCreationDto().Generate());
        var fakeContactTwo = FakeContact.Generate(new FakeContactForCreationDto().Generate());
        var queryParameters = new ContactParametersDto();

        await InsertAsync(fakeContactOne, fakeContactTwo);

        // Act
        var query = new GetContactList.Query(queryParameters);
        var contacts = await SendAsync(query);

        // Assert
        contacts.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}
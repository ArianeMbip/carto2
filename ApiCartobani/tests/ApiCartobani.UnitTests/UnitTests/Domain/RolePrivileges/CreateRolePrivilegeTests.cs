namespace ApiCartobani.UnitTests.UnitTests.Domain.RolePrivileges;

using ApiCartobani.SharedTestHelpers.Fakes.RolePrivilege;
using ApiCartobani.Domain.RolePrivileges;
using ApiCartobani.Domain.RolePrivileges.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateRolePrivilegeTests
{
    private readonly Faker _faker;

    public CreateRolePrivilegeTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_rolePrivilege()
    {
        // Arrange
        var rolePrivilegeToCreate = new FakeRolePrivilegeForCreationDto().Generate();
        
        // Act
        var fakeRolePrivilege = RolePrivilege.Create(rolePrivilegeToCreate);

        // Assert
        fakeRolePrivilege.Nom.Should().Be(rolePrivilegeToCreate.Nom);
        fakeRolePrivilege.Lire.Should().Be(rolePrivilegeToCreate.Lire);
        fakeRolePrivilege.Ecrire.Should().Be(rolePrivilegeToCreate.Ecrire);
        fakeRolePrivilege.Modifier.Should().Be(rolePrivilegeToCreate.Modifier);
        fakeRolePrivilege.Supprimer.Should().Be(rolePrivilegeToCreate.Supprimer);
        fakeRolePrivilege.Valider.Should().Be(rolePrivilegeToCreate.Valider);
        fakeRolePrivilege.Archiver.Should().Be(rolePrivilegeToCreate.Archiver);
        fakeRolePrivilege.Generer.Should().Be(rolePrivilegeToCreate.Generer);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeRolePrivilege = FakeRolePrivilege.Generate();

        // Assert
        fakeRolePrivilege.DomainEvents.Count.Should().Be(1);
        fakeRolePrivilege.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(RolePrivilegeCreated));
    }
}
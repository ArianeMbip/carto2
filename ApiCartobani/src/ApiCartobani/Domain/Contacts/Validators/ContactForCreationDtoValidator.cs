namespace ApiCartobani.Domain.Contacts.Validators;

using ApiCartobani.Domain.Contacts.Dtos;
using FluentValidation;

public sealed class ContactForCreationDtoValidator: ContactForManipulationDtoValidator<ContactForCreationDto>
{
    public ContactForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
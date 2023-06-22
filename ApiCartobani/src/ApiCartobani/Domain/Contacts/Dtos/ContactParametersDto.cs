namespace ApiCartobani.Domain.Contacts.Dtos;

using SharedKernel.Dtos;

public sealed class ContactParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

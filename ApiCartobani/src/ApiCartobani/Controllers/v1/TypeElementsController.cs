namespace ApiCartobani.Controllers.v1;

using ApiCartobani.Domain.TypeElements.Features;
using ApiCartobani.Domain.TypeElements.Dtos;
using ApiCartobani.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using ApiCartobani.Domain.TypeElements.Services;

[ApiController]
[Route("api/typeelements")]
[ApiVersion("1.0")]
public sealed class TypeElementsController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITypeElementRepository _typeElementRepository;

    public TypeElementsController(IMediator mediator, ITypeElementRepository typeElementRepository)
    {
        _mediator = mediator;
        _typeElementRepository = typeElementRepository;
    }
    

    /// <summary>
    /// Creates a new TypeElement record.
    /// </summary>
    /// <response code="201">TypeElement created.</response>
    /// <response code="400">TypeElement has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the TypeElement.</response>
    [ProducesResponseType(typeof(TypeElementDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddTypeElement")]
    public async Task<ActionResult<TypeElementDto>> AddTypeElement([FromBody]TypeElementForCreationDto typeElementForCreation)
    {
        var command = new AddTypeElement.Command(typeElementForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetTypeElement",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single TypeElement by ID.
    /// </summary>
    /// <response code="200">TypeElement record returned successfully.</response>
    /// <response code="400">TypeElement has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the TypeElement.</response>
    [ProducesResponseType(typeof(TypeElementDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetTypeElement")]
    public async Task<ActionResult<TypeElementDto>> GetTypeElement(Guid id)
    {
        var query = new GetTypeElement.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all TypeElements.
    /// </summary>
    /// <response code="200">TypeElement list returned successfully.</response>
    /// <response code="400">TypeElement has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the TypeElement.</response>
    /// <remarks>
    /// Requests can be narrowed down with a variety of query string values:
    /// ## Query String Parameters
    /// - **PageNumber**: An integer value that designates the page of records that should be returned.
    /// - **PageSize**: An integer value that designates the number of records returned on the given page that you would like to return. This value is capped by the internal MaxPageSize.
    /// - **SortOrder**: A comma delimited ordered list of property names to sort by. Adding a `-` before the name switches to sorting descendingly.
    /// - **Filters**: A comma delimited list of fields to filter by formatted as `{Name}{Operator}{Value}` where
    ///     - {Name} is the name of a filterable property. You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if LikeCount or CommentCount is >10
    ///     - {Operator} is one of the Operators below
    ///     - {Value} is the value to use for filtering. You can also have multiple values (for OR logic) by using a pipe delimiter, eg.`Title@= new|hot` will return posts with titles that contain the text "new" or "hot"
    ///
    ///    | Operator | Meaning                       | Operator  | Meaning                                      |
    ///    | -------- | ----------------------------- | --------- | -------------------------------------------- |
    ///    | `==`     | Equals                        |  `!@=`    | Does not Contains                            |
    ///    | `!=`     | Not equals                    |  `!_=`    | Does not Starts with                         |
    ///    | `>`      | Greater than                  |  `@=*`    | Case-insensitive string Contains             |
    ///    | `&lt;`   | Less than                     |  `_=*`    | Case-insensitive string Starts with          |
    ///    | `>=`     | Greater than or equal to      |  `==*`    | Case-insensitive string Equals               |
    ///    | `&lt;=`  | Less than or equal to         |  `!=*`    | Case-insensitive string Not equals           |
    ///    | `@=`     | Contains                      |  `!@=*`   | Case-insensitive string does not Contains    |
    ///    | `_=`     | Starts with                   |  `!_=*`   | Case-insensitive string does not Starts with |
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<TypeElementDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetTypeElements")]
    public async Task<IActionResult> GetTypeElements([FromQuery] TypeElementParametersDto typeElementParametersDto)
    {
        var query = new GetTypeElementList.Query(typeElementParametersDto);
        var queryResponse = await _mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }

    //[HttpGet("search")]
    //public async Task<IActionResult> SearchByProperty(string propertyName, string searchValue)
    //{
    //    var results = await _typeElementRepository.SearchByProperty(propertyName, searchValue);
    //    return Ok(results);
    //}

    /// <summary>
    /// Deletes an existing TypeElement record.
    /// </summary>
    /// <response code="204">TypeElement deleted.</response>
    /// <response code="400">TypeElement has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the TypeElement.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteTypeElement")]
    public async Task<ActionResult> DeleteTypeElement(Guid id)
    {
        var command = new DeleteTypeElement.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Updates an entire existing TypeElement.
    /// </summary>
    /// <response code="204">TypeElement updated.</response>
    /// <response code="400">TypeElement has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the TypeElement.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateTypeElement")]
    public async Task<IActionResult> UpdateTypeElement(Guid id, TypeElementForUpdateDto typeElement)
    {
        var command = new UpdateTypeElement.Command(id, typeElement);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}

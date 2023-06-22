namespace ApiCartobani.Controllers.v1;

using ApiCartobani.Domain.Icones.Features;
using ApiCartobani.Domain.Icones.Dtos;
using ApiCartobani.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/icones")]
[ApiVersion("1.0")]
public sealed class IconesController: ControllerBase
{
    private readonly IMediator _mediator;

    public IconesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Icone record.
    /// </summary>
    /// <response code="201">Icone created.</response>
    /// <response code="400">Icone has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Icone.</response>
    [ProducesResponseType(typeof(IconeDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddIcone")]
    public async Task<ActionResult<IconeDto>> AddIcone([FromBody]IconeForCreationDto iconeForCreation)
    {
        var command = new AddIcone.Command(iconeForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetIcone",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Icone by ID.
    /// </summary>
    /// <response code="200">Icone record returned successfully.</response>
    /// <response code="400">Icone has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Icone.</response>
    [ProducesResponseType(typeof(IconeDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetIcone")]
    public async Task<ActionResult<IconeDto>> GetIcone(Guid id)
    {
        var query = new GetIcone.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Icones.
    /// </summary>
    /// <response code="200">Icone list returned successfully.</response>
    /// <response code="400">Icone has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Icone.</response>
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
    [ProducesResponseType(typeof(IEnumerable<IconeDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetIcones")]
    public async Task<IActionResult> GetIcones([FromQuery] IconeParametersDto iconeParametersDto)
    {
        var query = new GetIconeList.Query(iconeParametersDto);
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


    /// <summary>
    /// Deletes an existing Icone record.
    /// </summary>
    /// <response code="204">Icone deleted.</response>
    /// <response code="400">Icone has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Icone.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteIcone")]
    public async Task<ActionResult> DeleteIcone(Guid id)
    {
        var command = new DeleteIcone.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Updates an entire existing Icone.
    /// </summary>
    /// <response code="204">Icone updated.</response>
    /// <response code="400">Icone has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Icone.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateIcone")]
    public async Task<IActionResult> UpdateIcone(Guid id, IconeForUpdateDto icone)
    {
        var command = new UpdateIcone.Command(id, icone);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}

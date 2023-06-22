namespace ApiCartobani.Controllers.v1;

using ApiCartobani.Domain.ValeurAttributs.Features;
using ApiCartobani.Domain.ValeurAttributs.Dtos;
using ApiCartobani.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/valeurattributs")]
[ApiVersion("1.0")]
public sealed class ValeurAttributsController: ControllerBase
{
    private readonly IMediator _mediator;

    public ValeurAttributsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new ValeurAttribut record.
    /// </summary>
    /// <response code="201">ValeurAttribut created.</response>
    /// <response code="400">ValeurAttribut has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the ValeurAttribut.</response>
    [ProducesResponseType(typeof(ValeurAttributDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddValeurAttribut")]
    public async Task<ActionResult<ValeurAttributDto>> AddValeurAttribut([FromBody]ValeurAttributForCreationDto valeurAttributForCreation)
    {
        var command = new AddValeurAttribut.Command(valeurAttributForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetValeurAttribut",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single ValeurAttribut by ID.
    /// </summary>
    /// <response code="200">ValeurAttribut record returned successfully.</response>
    /// <response code="400">ValeurAttribut has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the ValeurAttribut.</response>
    [ProducesResponseType(typeof(ValeurAttributDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetValeurAttribut")]
    public async Task<ActionResult<ValeurAttributDto>> GetValeurAttribut(Guid id)
    {
        var query = new GetValeurAttribut.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all ValeurAttributs.
    /// </summary>
    /// <response code="200">ValeurAttribut list returned successfully.</response>
    /// <response code="400">ValeurAttribut has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the ValeurAttribut.</response>
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
    [ProducesResponseType(typeof(IEnumerable<ValeurAttributDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetValeurAttributs")]
    public async Task<IActionResult> GetValeurAttributs([FromQuery] ValeurAttributParametersDto valeurAttributParametersDto)
    {
        var query = new GetValeurAttributList.Query(valeurAttributParametersDto);
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
    /// Deletes an existing ValeurAttribut record.
    /// </summary>
    /// <response code="204">ValeurAttribut deleted.</response>
    /// <response code="400">ValeurAttribut has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the ValeurAttribut.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteValeurAttribut")]
    public async Task<ActionResult> DeleteValeurAttribut(Guid id)
    {
        var command = new DeleteValeurAttribut.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Updates an entire existing ValeurAttribut.
    /// </summary>
    /// <response code="204">ValeurAttribut updated.</response>
    /// <response code="400">ValeurAttribut has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the ValeurAttribut.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateValeurAttribut")]
    public async Task<IActionResult> UpdateValeurAttribut(Guid id, ValeurAttributForUpdateDto valeurAttribut)
    {
        var command = new UpdateValeurAttribut.Command(id, valeurAttribut);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}

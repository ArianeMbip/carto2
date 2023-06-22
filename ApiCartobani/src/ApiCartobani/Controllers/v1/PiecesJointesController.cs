namespace ApiCartobani.Controllers.v1;

using ApiCartobani.Domain.PiecesJointes.Features;
using ApiCartobani.Domain.PiecesJointes.Dtos;
using ApiCartobani.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/piecesjointes")]
[ApiVersion("1.0")]
public sealed class PiecesJointesController: ControllerBase
{
    private readonly IMediator _mediator;

    public PiecesJointesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new PieceJointe record.
    /// </summary>
    /// <response code="201">PieceJointe created.</response>
    /// <response code="400">PieceJointe has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the PieceJointe.</response>
    [ProducesResponseType(typeof(PieceJointeDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddPieceJointe")]
    public async Task<ActionResult<PieceJointeDto>> AddPieceJointe([FromBody]PieceJointeForCreationDto pieceJointeForCreation)
    {
        var command = new AddPieceJointe.Command(pieceJointeForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetPieceJointe",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single PieceJointe by ID.
    /// </summary>
    /// <response code="200">PieceJointe record returned successfully.</response>
    /// <response code="400">PieceJointe has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the PieceJointe.</response>
    [ProducesResponseType(typeof(PieceJointeDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetPieceJointe")]
    public async Task<ActionResult<PieceJointeDto>> GetPieceJointe(Guid id)
    {
        var query = new GetPieceJointe.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all PiecesJointes.
    /// </summary>
    /// <response code="200">PieceJointe list returned successfully.</response>
    /// <response code="400">PieceJointe has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the PieceJointe.</response>
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
    [ProducesResponseType(typeof(IEnumerable<PieceJointeDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetPiecesJointes")]
    public async Task<IActionResult> GetPiecesJointes([FromQuery] PieceJointeParametersDto pieceJointeParametersDto)
    {
        var query = new GetPieceJointeList.Query(pieceJointeParametersDto);
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
    /// Deletes an existing PieceJointe record.
    /// </summary>
    /// <response code="204">PieceJointe deleted.</response>
    /// <response code="400">PieceJointe has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the PieceJointe.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeletePieceJointe")]
    public async Task<ActionResult> DeletePieceJointe(Guid id)
    {
        var command = new DeletePieceJointe.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Updates an entire existing PieceJointe.
    /// </summary>
    /// <response code="204">PieceJointe updated.</response>
    /// <response code="400">PieceJointe has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the PieceJointe.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdatePieceJointe")]
    public async Task<IActionResult> UpdatePieceJointe(Guid id, PieceJointeForUpdateDto pieceJointe)
    {
        var command = new UpdatePieceJointe.Command(id, pieceJointe);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}

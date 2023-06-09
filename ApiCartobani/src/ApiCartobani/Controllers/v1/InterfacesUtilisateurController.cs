namespace ApiCartobani.Controllers.v1;

using ApiCartobani.Domain.InterfacesUtilisateur.Features;
using ApiCartobani.Domain.InterfacesUtilisateur.Dtos;
using ApiCartobani.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/interfacesutilisateur")]
[ApiVersion("1.0")]
public sealed class InterfacesUtilisateurController: ControllerBase
{
    private readonly IMediator _mediator;

    public InterfacesUtilisateurController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new InterfaceUtilisateur record.
    /// </summary>
    /// <response code="201">InterfaceUtilisateur created.</response>
    /// <response code="400">InterfaceUtilisateur has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the InterfaceUtilisateur.</response>
    [ProducesResponseType(typeof(InterfaceUtilisateurDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddInterfaceUtilisateur")]
    public async Task<ActionResult<InterfaceUtilisateurDto>> AddInterfaceUtilisateur([FromBody]InterfaceUtilisateurForCreationDto interfaceUtilisateurForCreation)
    {
        var command = new AddInterfaceUtilisateur.Command(interfaceUtilisateurForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetInterfaceUtilisateur",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single InterfaceUtilisateur by ID.
    /// </summary>
    /// <response code="200">InterfaceUtilisateur record returned successfully.</response>
    /// <response code="400">InterfaceUtilisateur has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the InterfaceUtilisateur.</response>
    [ProducesResponseType(typeof(InterfaceUtilisateurDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetInterfaceUtilisateur")]
    public async Task<ActionResult<InterfaceUtilisateurDto>> GetInterfaceUtilisateur(Guid id)
    {
        var query = new GetInterfaceUtilisateur.Query(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all InterfacesUtilisateur.
    /// </summary>
    /// <response code="200">InterfaceUtilisateur list returned successfully.</response>
    /// <response code="400">InterfaceUtilisateur has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the InterfaceUtilisateur.</response>
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
    [ProducesResponseType(typeof(IEnumerable<InterfaceUtilisateurDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetInterfacesUtilisateur")]
    public async Task<IActionResult> GetInterfacesUtilisateur([FromQuery] InterfaceUtilisateurParametersDto interfaceUtilisateurParametersDto)
    {
        var query = new GetInterfaceUtilisateurList.Query(interfaceUtilisateurParametersDto);
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
    /// Deletes an existing InterfaceUtilisateur record.
    /// </summary>
    /// <response code="204">InterfaceUtilisateur deleted.</response>
    /// <response code="400">InterfaceUtilisateur has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the InterfaceUtilisateur.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteInterfaceUtilisateur")]
    public async Task<ActionResult> DeleteInterfaceUtilisateur(Guid id)
    {
        var command = new DeleteInterfaceUtilisateur.Command(id);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Updates an entire existing InterfaceUtilisateur.
    /// </summary>
    /// <response code="204">InterfaceUtilisateur updated.</response>
    /// <response code="400">InterfaceUtilisateur has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the InterfaceUtilisateur.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateInterfaceUtilisateur")]
    public async Task<IActionResult> UpdateInterfaceUtilisateur(Guid id, InterfaceUtilisateurForUpdateDto interfaceUtilisateur)
    {
        var command = new UpdateInterfaceUtilisateur.Command(id, interfaceUtilisateur);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}

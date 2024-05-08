using BackendApi.DataTransferObjects;
using BusinessLogicLayer;
using DataTransferObjects;
using DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  [Produces("application/json")]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public class DocumentsController(DocumentManager documentManager) : ControllerBase
  {

    [HttpGet("{id:int}", Name = "GetById")]
    [ProducesResponseType(typeof(DocumentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDocumentById([FromRoute] int id)
    {
      var device = await documentManager.GetDocumentById(id);
      if (device == null)
      {
        return NotFound();
      }
      return Ok(device);
    }

    [Authorize("read:documents")]
    [HttpGet(Name = "GetAll")]
    [ProducesResponseType(typeof(IEnumerable<DocumentListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDocuments()
    {
      var devices = await documentManager.GetDocumentsAsync();
      return Ok(devices);
    }

    // api/documents/name?name=abc&sort=true
    [HttpGet("name", Name = "Search")]
    [ProducesResponseType(typeof(IEnumerable<DocumentListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDocumentsByName([FromQuery] string name, [FromQuery] bool sort)
    {
      var devices = await documentManager.GetDocumentsByNameAsync(name, sort);
      return Ok(devices);
    }

    [HttpPost("query", Name = "Query")]
    [ProducesResponseType(typeof(IEnumerable<DocumentListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> QueryDocuments([FromBody] QueryObject query)
    {
      var devices = await documentManager.QueryDocuments(query);
      return Ok(devices);
    }

    [HttpPost(Name = "Add")]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddDocument([FromBody] CreateDocumentDto deviceDto)
    {
      //if (!ModelState.IsValid)
      //{
        
      //}

      var result = await documentManager.AddDocumentAsync(deviceDto);
      if (!result.Success)
      {
        return result.Exception switch
        {
          DbUpdateConcurrencyException => BadRequest("Concurrency Error"),
          DbUpdateException => BadRequest("Validation Error"),
          _ => BadRequest() // result.Exception
        };
      }
      var id = result.Data.Id; // TODO: Get the id of the newly created device
      return Created($"api/devices/{id}", id);
    }

    [HttpPut(Name = "Update")]
    [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDocument([FromBody] UpdateDocumentDto deviceDto)
    {
      var result = await documentManager.UpdateDocumentAsync(deviceDto);
      if (!result.Success)
      {
        return result.Exception switch
        {
          DbUpdateConcurrencyException => BadRequest("Concurrency Error"),
          DbUpdateException => BadRequest("Validation Error"),
          _ => BadRequest() // result.Exception
        };
      }
      return Accepted();
    }
  }
}

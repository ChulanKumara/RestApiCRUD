using Microsoft.AspNetCore.Mvc;
using Atmoz.EmissionBreakdownApi.Models;
using EmissionBreakdownApi.DTOs;
using EmissionBreakdownApi.Interfaces;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Atmoz.EmissionBreakdownApi.Controllers;

[ApiController]
[Route("/api/v1")]
public class EmissionBreakdownController : ControllerBase
{
    private readonly ILogger<EmissionBreakdownController> _logger;
    private readonly IEmissionBreakdownRepository _repository;
    private readonly IMapper _mapper;

    public EmissionBreakdownController(ILogger<EmissionBreakdownController> logger, IEmissionBreakdownRepository repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("EmissionBreakdown")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Query([FromQuery] EmissionBreakdownQueryParametersDTO parameters)
    {
        var data = _mapper.Map<EmissionBreakdownQueryParameters>(parameters);

        var results = await _repository.QueryAsync(data);
        return Ok(results);
    }

    [HttpGet("EmissionBreakdown/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EmissionBreakdownRowDTO>> Get(long id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null) return NotFound();
        return Ok(task);
	}

    [HttpPost("EmissionBreakdown")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreatedEmissionBreakdownRowDTO>> Create(EmissionBreakdownRowDTO row)
    {
        _repository.CreateAsync(row);

        var result = await _repository.SaveAllAsync();
        if (!result) return BadRequest("Could not save changes to the DB");

        CreatedEmissionBreakdownRowDTO createdEmissionBreakdownRowDTO = new CreatedEmissionBreakdownRowDTO();
        createdEmissionBreakdownRowDTO.RowId = row.Id;
        createdEmissionBreakdownRowDTO.Data = row;

        return Created("/EmissionBreakdown", createdEmissionBreakdownRowDTO);
    }

    [HttpDelete("EmissionBreakdown/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result == null) return NotFound();

        _repository.DeleteAsync(result);

        if (await _repository.SaveAllAsync()) return Ok();

        return BadRequest("Could not update DB");
    }

    [HttpPatch("EmissionBreakdown/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EmissionBreakdownRowDTO>> Update(long id, EmissionBreakdownRowDTO row)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result == null) return NotFound();

        _mapper.Map(row, result);

        if (await _repository.SaveAllAsync()) return Ok(row);

        return BadRequest("Problem saving changes");
    }
}

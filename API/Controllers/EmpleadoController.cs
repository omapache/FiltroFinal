using API.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class EmpleadoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public EmpleadoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
    {
        var Empleado = await unitofwork.Empleados.GetAllAsync();
        return mapper.Map<List<EmpleadoDto>>(Empleado);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EmpleadoDto>> Get(int id)
    {
        var Empleado = await unitofwork.Empleados.GetByIdAsync(id);
        if (Empleado == null){
            return NotFound();
        }
        return this.mapper.Map<EmpleadoDto>(Empleado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Empleado>> Post(EmpleadoDto EmpleadoDto)
    {
        var Empleado = this.mapper.Map<Empleado>(EmpleadoDto);
        this.unitofwork.Empleados.Add(Empleado);
        await unitofwork.SaveAsync();
        if(Empleado == null)
        {
            return BadRequest();
        }
        EmpleadoDto.Id = Empleado.Id;
        return CreatedAtAction(nameof(Post), new {id = EmpleadoDto.Id}, EmpleadoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody]EmpleadoDto EmpleadoDto){
        if(EmpleadoDto == null)
        {
            return NotFound();
        }
        var Empleado = this.mapper.Map<Empleado>(EmpleadoDto);
        unitofwork.Empleados.Update(Empleado);
        await unitofwork.SaveAsync();
        return EmpleadoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id){
        var Empleado = await unitofwork.Empleados.GetByIdAsync(id);
        if(Empleado == null)
        {
            return NotFound();
        }
        unitofwork.Empleados.Remove(Empleado);
        await unitofwork.SaveAsync();
        return NoContent();
    }
    [HttpGet("consulta6")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> EmpleadosSinRepresentanteVentas()
    {
        var Cliente = await unitofwork.Empleados.EmpleadosSinRepresentanteVentas();
        return mapper.Map<List<object>>(Cliente);
    }
}
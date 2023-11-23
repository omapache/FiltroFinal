using API.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public ProductoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
    {
        var Producto = await unitofwork.Productos.GetAllAsync();
        return mapper.Map<List<ProductoDto>>(Producto);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProductoDto>> Get(string id)
    {
        var Producto = await unitofwork.Productos.GetByIdAsync(id);
        if (Producto == null){
            return NotFound();
        }
        return this.mapper.Map<ProductoDto>(Producto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Producto>> Post(ProductoDto ProductoDto)
    {
        var Producto = this.mapper.Map<Producto>(ProductoDto);
        this.unitofwork.Productos.Add(Producto);
        await unitofwork.SaveAsync();
        if(Producto == null)
        {
            return BadRequest();
        }
        ProductoDto.Id = Producto.Id;
        return CreatedAtAction(nameof(Post), new {id = ProductoDto.Id}, ProductoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody]ProductoDto ProductoDto){
        if(ProductoDto == null)
        {
            return NotFound();
        }
        var Producto = this.mapper.Map<Producto>(ProductoDto);
        unitofwork.Productos.Update(Producto);
        await unitofwork.SaveAsync();
        return ProductoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(string id){
        var Producto = await unitofwork.Productos.GetByIdAsync(id);
        if(Producto == null)
        {
            return NotFound();
        }
        unitofwork.Productos.Remove(Producto);
        await unitofwork.SaveAsync();
        return NoContent();
    }
    [HttpGet("consulta5")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> VentasProductosMas3000Euros()
    {
        var Cliente = await unitofwork.Productos.VentasProductosMas3000Euros();
        return mapper.Map<List<object>>(Cliente);
    }
    [HttpGet("consulta7")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<object>>> ProductoMasVendido()
    {
        var Cliente = await unitofwork.Productos.ProductoMasVendido();
        return mapper.Map<List<object>>(Cliente);
    }
}
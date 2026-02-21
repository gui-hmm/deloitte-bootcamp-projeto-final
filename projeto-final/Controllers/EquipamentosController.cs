using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.DTOs;
using ProjetoFinal.Services;

namespace ProjetoFinal.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EquipamentosController : ControllerBase
{
    private readonly IEquipamentoService _service;

    public EquipamentosController(IEquipamentoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(EquipamentoCreateDto dto)
    {
        var result = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        string? tipo,
        string? status,
        string? codigo,
        int page = 1,
        int pageSize = 10)
    {
        var result = await _service.ListarAsync(tipo, status, codigo, page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.ObterPorIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EquipamentoUpdateDto dto)
    {
        var updated = await _service.AtualizarAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.RemoverAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}

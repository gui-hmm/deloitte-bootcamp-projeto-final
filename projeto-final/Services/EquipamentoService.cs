using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Domain.Models;
using ProjetoFinal.Domain.Enums;
using ProjetoFinal.DTOs;
using ProjetoFinal.DTOs.Common;

namespace ProjetoFinal.Services;

public class EquipamentoService : IEquipamentoService
{
    private readonly AppDbContext _context;

    public EquipamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EquipamentoResponseDto> CriarAsync(EquipamentoCreateDto dto)
    {
        if (!Enum.TryParse<TipoEquipamento>(dto.Tipo, true, out var tipo))
            throw new ArgumentException("Tipo inválido");

        if (!Enum.TryParse<StatusOperacional>(dto.StatusOperacional, true, out var status))
            throw new ArgumentException("StatusOperacional inválido");

        if (await _context.Equipamentos.AnyAsync(e => e.Codigo == dto.Codigo))
            throw new ArgumentException("Já existe um equipamento com esse código.");

        if (dto.Horimetro < 0)
            throw new ArgumentException("Horímetro não pode ser negativo.");

        var equipamento = new Equipamento(
            dto.Codigo,
            tipo,
            dto.Modelo,
            dto.Horimetro,
            status,
            dto.DataAquisicao,
            dto.LocalizacaoAtual
        );

        _context.Equipamentos.Add(equipamento);
        await _context.SaveChangesAsync();

        return MapToResponse(equipamento);
    }

    public async Task<PagedResponse<EquipamentoResponseDto>> ListarAsync(
        string? tipo,
        string? status,
        string? codigo,
        int page,
        int pageSize)
    {
        var query = _context.Equipamentos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(tipo))
            query = query.Where(e => e.Tipo.ToString() == tipo);

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(e => e.StatusOperacional.ToString() == status);

        if (!string.IsNullOrWhiteSpace(codigo))
            query = query.Where(e => e.Codigo.Contains(codigo));

        var totalRecords = await query.CountAsync();

        var equipamentos = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var response = equipamentos.Select(MapToResponse);

        return new PagedResponse<EquipamentoResponseDto>(
            response,
            page,
            pageSize,
            totalRecords
        );
    }
    public async Task<EquipamentoResponseDto?> ObterPorIdAsync(int id)
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);
        return equipamento == null ? null : MapToResponse(equipamento);
    }

    public async Task<bool> AtualizarAsync(int id, EquipamentoUpdateDto dto)
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);
        if (equipamento == null) return false;

        if (!Enum.TryParse<StatusOperacional>(dto.StatusOperacional, true, out var status))
            throw new ArgumentException("StatusOperacional inválido");

        equipamento.Atualizar(dto.Horimetro, status, dto.LocalizacaoAtual);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);
        if (equipamento == null) return false;

        _context.Equipamentos.Remove(equipamento);
        await _context.SaveChangesAsync();
        return true;
    }

    private static EquipamentoResponseDto MapToResponse(Equipamento e)
    {
        return new EquipamentoResponseDto
        {
            Id = e.Id,
            Codigo = e.Codigo,
            Tipo = e.Tipo.ToString(),
            Modelo = e.Modelo,
            Horimetro = e.Horimetro,
            StatusOperacional = e.StatusOperacional.ToString(),
            DataAquisicao = e.DataAquisicao,
            LocalizacaoAtual = e.LocalizacaoAtual
        };
    }
}

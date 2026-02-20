using ProjetoFinal.DTOs;

namespace ProjetoFinal.Services;

public interface IEquipamentoService
{
    Task<EquipamentoResponseDto> CriarAsync(EquipamentoCreateDto dto);
    Task<IEnumerable<EquipamentoResponseDto>> ListarAsync(string? tipo, string? status, string? codigo, int page, int pageSize);
    Task<EquipamentoResponseDto?> ObterPorIdAsync(int id);
    Task<bool> AtualizarAsync(int id, EquipamentoUpdateDto dto);
    Task<bool> RemoverAsync(int id);
}

using ProjetoFinal.DTOs;
using ProjetoFinal.DTOs.Common;

namespace ProjetoFinal.Services;

public interface IEquipamentoService
{
    Task<EquipamentoResponseDto> CriarAsync(EquipamentoCreateDto dto);
    Task<PagedResponse<EquipamentoResponseDto>> ListarAsync(
        string? tipo,
        string? status,
        string? codigo,
        int page,
        int pageSize);
    Task<EquipamentoResponseDto?> ObterPorIdAsync(int id);
    Task<bool> AtualizarAsync(int id, EquipamentoUpdateDto dto);
    Task<bool> RemoverAsync(int id);
}

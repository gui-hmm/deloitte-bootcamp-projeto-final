namespace ProjetoFinal.DTOs;

public class EquipamentoUpdateDto
{
    public decimal Horimetro { get; set; }
    public string StatusOperacional { get; set; } = string.Empty;
    public string? LocalizacaoAtual { get; set; }
}

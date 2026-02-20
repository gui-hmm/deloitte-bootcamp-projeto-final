namespace ProjetoFinal.DTOs;

public class EquipamentoCreateDto
{
    public string Codigo { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public decimal Horimetro { get; set; }
    public string StatusOperacional { get; set; } = string.Empty;
    public DateOnly? DataAquisicao { get; set; }
    public string? LocalizacaoAtual { get; set; }
}

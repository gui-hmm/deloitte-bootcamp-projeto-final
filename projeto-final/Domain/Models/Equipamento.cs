using ProjetoFinal.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFinal.Domain.Models;

[Table("equipamentos")]
public class Equipamento
{
    [Column("id")]
    public int Id { get; private set; }

    [Column("codigo")]
    public string Codigo { get; private set; } = string.Empty;

    [Column("tipo")]
    public TipoEquipamento Tipo { get; private set; }

    [Column("modelo")]
    public string Modelo { get; private set; } = string.Empty;

    [Column("horimetro")]
    public decimal Horimetro { get; private set; }

    [Column("status_operacional")]
    public StatusOperacional StatusOperacional { get; private set; }

    [Column("data_aquisicao")]
    public DateOnly? DataAquisicao { get; private set; }

    [Column("localizacao_atual")]
    public string? LocalizacaoAtual { get; private set; }

    private Equipamento() { }

    public Equipamento(
        string codigo,
        TipoEquipamento tipo,
        string modelo,
        decimal horimetro,
        StatusOperacional status,
        DateOnly? dataAquisicao,
        string? localizacaoAtual)
    {
        Codigo = codigo.Trim();
        Tipo = tipo;
        Modelo = modelo;
        Horimetro = horimetro;
        StatusOperacional = status;
        DataAquisicao = dataAquisicao;
        LocalizacaoAtual = localizacaoAtual;
    }

    public void Atualizar(
        decimal horimetro,
        StatusOperacional status,
        string? localizacaoAtual)
    {
        Horimetro = horimetro;
        StatusOperacional = status;
        LocalizacaoAtual = localizacaoAtual;
    }
}

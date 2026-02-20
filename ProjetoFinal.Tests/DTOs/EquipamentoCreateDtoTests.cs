using Xunit;
using ProjetoFinal.DTOs;
using System;

public class EquipamentoCreateDtoTests
{
    [Fact]
    public void Deve_Setar_E_Ler_Propriedades_Corretamente()
    {
        var dto = new EquipamentoCreateDto
        {
            Codigo = "COD123",
            Tipo = "Caminhao",
            Modelo = "Modelo X",
            Horimetro = 150,
            StatusOperacional = "Operacional",
            DataAquisicao = DateOnly.FromDateTime(DateTime.Today),
            LocalizacaoAtual = "SP"
        };

        Assert.Equal("COD123", dto.Codigo);
        Assert.Equal("Caminhao", dto.Tipo);
        Assert.Equal("Modelo X", dto.Modelo);
        Assert.Equal(150, dto.Horimetro);
        Assert.Equal("Operacional", dto.StatusOperacional);
        Assert.NotNull(dto.DataAquisicao);
        Assert.Equal("SP", dto.LocalizacaoAtual);
    }
}
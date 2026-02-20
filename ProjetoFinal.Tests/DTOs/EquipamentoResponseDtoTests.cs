using Xunit;
using ProjetoFinal.DTOs;

public class EquipamentoResponseDtoTests
{
    [Fact]
    public void Deve_Setar_E_Ler_Propriedades_Corretamente()
    {
        var dto = new EquipamentoResponseDto
        {
            Id = 1,
            Codigo = "COD123",
            Tipo = "Caminhao",
            Modelo = "Modelo X",
            Horimetro = 200,
            StatusOperacional = "Operacional",
            LocalizacaoAtual = "RJ"
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal("COD123", dto.Codigo);
        Assert.Equal("Caminhao", dto.Tipo);
        Assert.Equal("Modelo X", dto.Modelo);
        Assert.Equal(200, dto.Horimetro);
        Assert.Equal("Operacional", dto.StatusOperacional);
        Assert.Equal("RJ", dto.LocalizacaoAtual);
    }
}
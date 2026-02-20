using Xunit;
using ProjetoFinal.DTOs;

public class EquipamentoUpdateDtoTests
{
    [Fact]
    public void Deve_Setar_E_Ler_Propriedades_Corretamente()
    {
        var dto = new EquipamentoUpdateDto
        {
            Horimetro = 500,
            StatusOperacional = "Parado",
            LocalizacaoAtual = "MG"
        };

        Assert.Equal(500, dto.Horimetro);
        Assert.Equal("Parado", dto.StatusOperacional);
        Assert.Equal("MG", dto.LocalizacaoAtual);
    }
}
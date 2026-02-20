using Xunit;
using ProjetoFinal.Domain.Enums;

public class EnumTests
{
    [Fact]
    public void StatusOperacional_DeveTerValoresEsperados()
    {
        Assert.Equal(1, (int)StatusOperacional.Operacional);
        Assert.Equal(2, (int)StatusOperacional.EmManutencao);
        Assert.Equal(3, (int)StatusOperacional.Parado);
    }

    [Fact]
    public void TipoEquipamento_DeveTerValoresEsperados()
    {
        Assert.Equal(1, (int)TipoEquipamento.Caminhao);
        Assert.Equal(5, (int)TipoEquipamento.Trator);
    }
}
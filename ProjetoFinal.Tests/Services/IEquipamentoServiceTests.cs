using Xunit;
using ProjetoFinal.Services;

public class InterfaceTests
{
    [Fact]
    public void EquipamentoService_DeveImplementar_Interface()
    {
        Assert.True(typeof(IEquipamentoService)
            .IsAssignableFrom(typeof(EquipamentoService)));
    }
}
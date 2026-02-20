using Xunit;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Domain.Models;
using ProjetoFinal.Domain.Enums;
using System.Linq;

public class AppDbContextTests
{
    [Fact]
    public void Deve_Configurar_Modelo_Corretamente()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        using var context = new AppDbContext(options);

        var entityType = context.Model.FindEntityType(typeof(Equipamento));

        Assert.NotNull(entityType);
        Assert.NotNull(entityType.FindPrimaryKey());
        Assert.Contains(entityType.GetIndexes(), i => i.IsUnique);
    }
}
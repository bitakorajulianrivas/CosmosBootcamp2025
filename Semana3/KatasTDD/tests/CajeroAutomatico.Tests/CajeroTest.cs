using FluentAssertions;

namespace CajeroAutomatico.Tests;

public class CajeroTest
{
    [Theory]
    [InlineData(500, "1 billete de 500")]
    [InlineData(200, "1 billete de 200")]
    [InlineData(100, "1 billete de 100")]
    [InlineData(50, "1 billete de 50")]
    [InlineData(20, "1 billete de 20")]
    [InlineData(10, "1 billete de 10")]
    [InlineData(5, "1 billete de 5")]
    [InlineData(2, "1 moneda de 2")]
    [InlineData(1, "1 moneda de 1")]
    public void Retirar_SiLaCantidadEsUnaDenominacion_DebeRetornarUnaDenominacion(int montoSolicitado, string valorEnTextoEsperado)
    {
        Cajero cajero = new Cajero();
        
        string montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().Be(valorEnTextoEsperado);
    }
}

public class Cajero
{
    public string Retirar(int montoSolicitado)
    {
        var denominaciones = new Dictionary<int, string>
        {
            { 500, "1 billete de 500" },
            { 200, "1 billete de 200" },
            { 100, "1 billete de 100" },
            { 50,  "1 billete de 50" },
            { 20,  "1 billete de 20" },
            { 10,  "1 billete de 10" },
            { 5,   "1 billete de 5" },
            { 2,   "1 moneda de 2" },
            { 1,   "1 moneda de 1" }
        };
        
        if (denominaciones.TryGetValue(montoSolicitado, out string? descripcion))
            return descripcion;
        
        throw new NotImplementedException();
    }
}
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
    public void Retirar_SiLaCantidadEsUnaDenominacion_DebeRetornarUnaDenominacion(int montoSolicitado, params string[] valorEnTextoEsperado)
    {
        Cajero cajero = new Cajero();
        
        var montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().BeEquivalentTo(valorEnTextoEsperado);
    }

    [Theory]
    [InlineData(1500,  "3 billetes de 500")]
    [InlineData(1000, "2 billetes de 500")]
    [InlineData(400,  "2 billetes de 200")]
    [InlineData(40,  "2 billetes de 20")]
    [InlineData(4,  "2 monedas de 2")]
    public void Retirar_SiLaCantidadEsUnMultiploExacto_RebeRetornarCantidadDeLaMismaDenominacion(int montoSolicitado, params string[] valorEnTextoEsperado)
    {
        Cajero cajero = new Cajero();
        
        var montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().BeEquivalentTo(valorEnTextoEsperado);
    }

    [Theory]
    [InlineData(1825, "3 billetes de 500", "1 billete de 200", "1 billete de 100", "1 billete de 20", "1 billete de 5")]
    [InlineData(1725, "3 billetes de 500", "1 billete de 200", "1 billete de 20", "1 billete de 5")]
    [InlineData(1475, "2 billetes de 500", "2 billetes de 200", "1 billete de 50", "1 billete de 20", "1 billete de 5")]
    [InlineData(128, "1 billete de 100", "1 billete de 20", "1 billete de 5", "1 moneda de 2", "1 moneda de 1")]
    [InlineData(3, "1 moneda de 2", "1 moneda de 1")]
    public void Retirar_SiCantidadTieneVariasDenominaciones_DebeRetirarListaDeDenominaciones(int montoSolicitado, params string[] valorEnTextoEsperado)
    {
        Cajero cajero = new Cajero();
        
        var montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().BeEquivalentTo(valorEnTextoEsperado);
    }
}

public class Cajero
{
    private const string FormatoValorEnTexto = "{0} {1} de {2}";

    private static readonly Dictionary<int, string> Denominaciones = new()
    {
        { 500, "billete" },
        { 200, "billete" },
        { 100, "billete" },
        { 50,  "billete" },
        { 20,  "billete" },
        { 10,  "billete" },
        { 5,   "billete" },
        { 2,   "moneda" },
        { 1,   "moneda" }
    };

    public string[] Retirar(int montoSolicitado)
    {
        var denominacionesRetornadas = new List<string>();
        
        foreach (var denominacion in Denominaciones)
        {
            int unidadesPorDenominacion = montoSolicitado / denominacion.Key;

            if (unidadesPorDenominacion != 0)
            {
                string tipoDenominacion = unidadesPorDenominacion > 1
                    ? denominacion.Value + "s" : denominacion.Value;

                var denominacionRetornada = 
                    string.Format(FormatoValorEnTexto,
                        unidadesPorDenominacion,
                        tipoDenominacion,
                        denominacion.Key);
                    
                denominacionesRetornadas.Add(denominacionRetornada);
                
                montoSolicitado %= denominacion.Key;
            }
        }
        
        return  denominacionesRetornadas.ToArray();
    }
}
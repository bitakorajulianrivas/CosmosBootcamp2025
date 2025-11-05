using CajeroAutomatico.Models;
using CajeroAutomatico.Utilities;
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
        
        montoRetirado.ConvertirATexto().Should().BeEquivalentTo(valorEnTextoEsperado);
    }

    [Theory]
    [InlineData(1000, "2 billetes de 500")]
    [InlineData(400,  "2 billetes de 200")]
    [InlineData(40,  "2 billetes de 20")]
    [InlineData(4,  "2 monedas de 2")]
    public void Retirar_SiLaCantidadEsUnMultiploExacto_RebeRetornarCantidadDeLaMismaDenominacion(int montoSolicitado, params string[] valorEnTextoEsperado)
    {
        Cajero cajero = new Cajero();
        
        var montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.ConvertirATexto().Should().BeEquivalentTo(valorEnTextoEsperado);
    }

    [Theory]
    [InlineData(1500, "2 billetes de 500", "2 billetes de 200", "1 billete de 100")]
    [InlineData(1825, "2 billetes de 500", "3 billetes de 200", "2 billetes de 100", "1 billete de 20", "1 billete de 5")]
    [InlineData(1725, "2 billetes de 500", "3 billetes de 200", "1 billete de 100", "1 billete de 20", "1 billete de 5")]
    [InlineData(1475, "2 billetes de 500", "2 billetes de 200", "1 billete de 50", "1 billete de 20", "1 billete de 5")]
    [InlineData(128, "1 billete de 100", "1 billete de 20", "1 billete de 5", "1 moneda de 2", "1 moneda de 1")]
    [InlineData(3, "1 moneda de 2", "1 moneda de 1")]
    public void Retirar_SiCantidadTieneVariasDenominaciones_DebeRetirarListaDeDenominaciones(int montoSolicitado, params string[] valorEnTextoEsperado)
    {
        Cajero cajero = new Cajero();
        
        var montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.ConvertirATexto().Should().BeEquivalentTo(valorEnTextoEsperado);
    }

    [Fact]
    public void Retirar_SiElCajeroNoDisponeDeDineroSuficiente_DebeLanzarExcepcion()
    {
        Cajero cajero = new Cajero();

        Action action = () => cajero.Retirar(100_000);

        action.Should().ThrowExactly<ArgumentException>()
            .WithMessage(CajeroErrores.ElCajeroNoDisponeDeDineroSuficienteParaEstaTransaccion);
    }

    [Fact]
    public void Retirar_SiElCajeroNoDisponeDeMasBilletes_Debe_DevolverLaMismaCantidadEnOtrasDenominaciones()
    {
        Cajero cajero = new Cajero();
        int montoSolicitado = 1_725;

        List<MontoRetiro> montoRetirado = cajero.Retirar(montoSolicitado);

        montoRetirado.Should().BeEquivalentTo([
            new MontoRetiro(Dinero.BilleteDe(500), Cantidad: 2),
            new MontoRetiro(Dinero.BilleteDe(200), Cantidad: 3),
            new MontoRetiro(Dinero.BilleteDe(100), Cantidad: 1),
            new MontoRetiro(Dinero.BilleteDe(20),  Cantidad: 1),
            new MontoRetiro(Dinero.BilleteDe(5),   Cantidad: 1)]);
    }

    [Fact]
    public void Retirar_SiElCajeroSeQuedaSinFondos_DebeLanzarExcepcion()
    {
        Cajero cajero = new Cajero();
        cajero.Retirar(5_100);

        Action action = () => cajero.Retirar(1);

        action.Should().ThrowExactly<ArgumentException>()
            .WithMessage(CajeroErrores.ElCajeroNoDisponeDeDineroSuficienteParaEstaTransaccion);
    }

    [Fact]
    public void ConsutarFondoDisponible_SiElCajeroNoHaRealizadoRetiros_DebeMostrarLaCantidadDeUnidadesPorCadaDenominacion()
    {
        Cajero cajero = new Cajero();
        
        List<MontoDisponible> fondoDisponible = cajero
            .ConsutarFondoDisponible();

        fondoDisponible.Should().BeEquivalentTo([
            new MontoDisponible(Dinero.BilleteDe(500), Cantidad: 2),
            new MontoDisponible(Dinero.BilleteDe(200), Cantidad: 3),
            new MontoDisponible(Dinero.BilleteDe(100), Cantidad: 5),
            new MontoDisponible(Dinero.BilleteDe(50),  Cantidad: 12),
            new MontoDisponible(Dinero.BilleteDe(20),  Cantidad: 20),
            new MontoDisponible(Dinero.BilleteDe(10),  Cantidad: 50),
            new MontoDisponible(Dinero.BilleteDe(5),   Cantidad: 100),
            new MontoDisponible(Dinero.BilleteDe(2),   Cantidad: 250),
            new MontoDisponible(Dinero.BilleteDe(1),   Cantidad: 500)
        ]);
    }

    [Fact]
    public void ConsutarFondoDisponible_SiElCajeroRealizaRetirosPor1725_DebeMostrarLaCantidadDeUnidadesRestantesPorCadaDenominacion()
    {
        int montoSolicitado = 1_725;
        Cajero cajero = new Cajero();
        cajero.Retirar(montoSolicitado);

        List<MontoDisponible> fondoDisponible = cajero
            .ConsutarFondoDisponible();

        fondoDisponible.Should().BeEquivalentTo([
            new MontoDisponible(Dinero.BilleteDe(500), Cantidad: 0),
            new MontoDisponible(Dinero.BilleteDe(200), Cantidad: 0),
            new MontoDisponible(Dinero.BilleteDe(100), Cantidad: 4),
            new MontoDisponible(Dinero.BilleteDe(50),  Cantidad: 12),
            new MontoDisponible(Dinero.BilleteDe(20),  Cantidad: 19),
            new MontoDisponible(Dinero.BilleteDe(10),  Cantidad: 50),
            new MontoDisponible(Dinero.BilleteDe(5),   Cantidad: 99),
            new MontoDisponible(Dinero.BilleteDe(2),   Cantidad: 250),
            new MontoDisponible(Dinero.BilleteDe(1),   Cantidad: 500)
        ]);
    }

    [Fact]
    public void ConsutarFondoDisponible_SiElCajeroSeQuedaSinFondos_DebeMostrarSinDenominacionesDisponibles()
    {
        Cajero cajero = new Cajero();
        cajero.Retirar(5100);

        List<MontoDisponible> fondoDisponible = cajero
            .ConsutarFondoDisponible();

        fondoDisponible.Should().BeEquivalentTo([
            new MontoDisponible(Dinero.BilleteDe(500), Cantidad: 0),
            new MontoDisponible(Dinero.BilleteDe(200), Cantidad: 0),
            new MontoDisponible(Dinero.BilleteDe(100), Cantidad: 0),
            new MontoDisponible(Dinero.BilleteDe(50),  Cantidad: 0),
            new MontoDisponible(Dinero.BilleteDe(20),  Cantidad: 0),
            new MontoDisponible(Dinero.BilleteDe(10),  Cantidad: 0),
            new MontoDisponible(Dinero.BilleteDe(5),   Cantidad: 0),
            new MontoDisponible(Dinero.BilleteDe(2),   Cantidad: 0),
            new MontoDisponible(Dinero.BilleteDe(1),   Cantidad: 0)
        ]);
    }


    [Fact]
    public void Retirar_SiRealizarnRetirosIgualOMenorACero_DebeLanzarExcepcion()
    {
        Cajero cajero = new Cajero();

        Action action = () => cajero.Retirar(0);

        action.Should().ThrowExactly<ArgumentException>()
            .WithMessage(CajeroErrores.DebeRetirarMinimoUnaUnidad);
    }
}
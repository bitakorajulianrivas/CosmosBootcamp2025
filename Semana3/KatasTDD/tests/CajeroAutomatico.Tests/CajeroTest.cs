using CajeroAutomatico.Models;
using CajeroAutomatico.Utilities;
using FluentAssertions;

namespace CajeroAutomatico.Tests;

public class CajeroTest
{
    private ICajero _cajero;

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
    public void Retirar_SiLaCantidadEsUnaDenominacion_DebeRetornarUnaDenominacion(
        int montoSolicitado, params string[] valorEnTextoEsperado)
    {
        _cajero = new Cajero();
        
        var montoRetirado = _cajero.Retirar(montoSolicitado);
        
        montoRetirado.ConvertirATexto().Should().BeEquivalentTo(valorEnTextoEsperado);
    }

    [Theory]
    [InlineData(1000, "2 billetes de 500")]
    [InlineData(400,  "2 billetes de 200")]
    [InlineData(40,  "2 billetes de 20")]
    [InlineData(4,  "2 monedas de 2")]
    public void Retirar_SiLaCantidadEsUnMultiploExacto_RebeRetornarCantidadDeLaMismaDenominacion(
        int montoSolicitado, params string[] valorEnTextoEsperado)
    {
        _cajero = new Cajero();

        var montoRetirado = _cajero.Retirar(montoSolicitado);
        
        montoRetirado.ConvertirATexto().Should().BeEquivalentTo(valorEnTextoEsperado);
    }

    [Theory]
    [InlineData(1500, "2 billetes de 500", "2 billetes de 200", "1 billete de 100")]
    [InlineData(1825, "2 billetes de 500", "3 billetes de 200", "2 billetes de 100", "1 billete de 20", "1 billete de 5")]
    [InlineData(1725, "2 billetes de 500", "3 billetes de 200", "1 billete de 100", "1 billete de 20", "1 billete de 5")]
    [InlineData(1475, "2 billetes de 500", "2 billetes de 200", "1 billete de 50", "1 billete de 20", "1 billete de 5")]
    [InlineData(128, "1 billete de 100", "1 billete de 20", "1 billete de 5", "1 moneda de 2", "1 moneda de 1")]
    [InlineData(3, "1 moneda de 2", "1 moneda de 1")]
    public void Retirar_SiCantidadTieneVariasDenominaciones_DebeRetirarListaDeDenominaciones(
        int montoSolicitado, params string[] valorEnTextoEsperado)
    {
        _cajero = new Cajero();

        var montoRetirado = _cajero.Retirar(montoSolicitado);
        
        montoRetirado.ConvertirATexto().Should().BeEquivalentTo(valorEnTextoEsperado);
    }

    [Fact]
    public void Retirar_SiElCajeroNoDisponeDeDineroSuficiente_DebeLanzarExcepcion()
    {
        _cajero = new Cajero();

        Action action = () => _cajero.Retirar(100_000);

        action.Should().ThrowExactly<ArgumentException>()
            .WithMessage(CajeroErrores.ElCajeroNoDisponeDeDineroSuficienteParaEstaTransaccion);
    }

    [Fact]
    public void Retirar_SiElCajeroNoDisponeDeMasBilletes_Debe_DevolverLaMismaCantidadEnOtrasDenominaciones()
    {
        _cajero = new Cajero();
        int montoSolicitado = 1_725;

        List<BilleteRetiro> montoRetirado = _cajero.Retirar(montoSolicitado);

        montoRetirado.ConvertirATexto().Should().BeEquivalentTo([
            "2 billetes de 500", 
            "3 billetes de 200", 
            "1 billete de 100", 
            "1 billete de 20", 
            "1 billete de 5"]);
    }

    [Fact]
    public void Retirar_SiElCajeroSeQuedaSinFondos_DebeLanzarExcepcion()
    {
        _cajero = new Cajero();
        _cajero.Retirar(5_100);

        Action action = () => _cajero.Retirar(1);

        action.Should().ThrowExactly<ArgumentException>()
            .WithMessage(CajeroErrores.ElCajeroNoDisponeDeDineroSuficienteParaEstaTransaccion);
    }
    [Fact]
    public void ConsutarInventario_SiElCajeroNoHaRealizadoRetiros_DebeMostrarLaCantidadDeUnidadesPorCadaDenominacion()
    {
        _cajero = new Cajero();

        List<BilleteDisponible> fondoDisponible = _cajero.ConsultarInventario();

        fondoDisponible.Should().BeEquivalentTo([
            new BilleteDisponible(Cantidad: 2, Dinero.BilleteDeQuinientos()),
            new BilleteDisponible(Cantidad: 3, Dinero.BilleteDeDoscientos()),
            new BilleteDisponible(Cantidad: 5, Dinero.BilleteDeCien()),
            new BilleteDisponible(Cantidad: 12, Dinero.BilleteDeCincuenta()),
            new BilleteDisponible(Cantidad: 20, Dinero.BilleteDeVeinte()),
            new BilleteDisponible(Cantidad: 50, Dinero.BilleteDeDiez()),
            new BilleteDisponible(Cantidad: 100, Dinero.BilleteDeCinco()),
            new BilleteDisponible(Cantidad: 250, Dinero.MonedaDeDos()),
            new BilleteDisponible(Cantidad: 500, Dinero.MonedaDeUno())
        ]);
    }

    [Fact]
    public void ConsutarInventario_SiElCajeroRealizaRetirosPor1725_DebeMostrarLaCantidadDeUnidadesRestantesPorCadaDenominacion()
    {
        int montoSolicitado = 1_725;
        _cajero = new Cajero();
        _cajero.Retirar(montoSolicitado);

        List<BilleteDisponible> fondoDisponible = _cajero.ConsultarInventario();

        fondoDisponible.Should().BeEquivalentTo([
            new BilleteDisponible(Cantidad: 0, Dinero.BilleteDeQuinientos()),
            new BilleteDisponible(Cantidad: 0, Dinero.BilleteDeDoscientos()),
            new BilleteDisponible(Cantidad: 4, Dinero.BilleteDeCien()),
            new BilleteDisponible(Cantidad: 12, Dinero.BilleteDeCincuenta()),
            new BilleteDisponible(Cantidad: 19, Dinero.BilleteDeVeinte()),
            new BilleteDisponible(Cantidad: 50, Dinero.BilleteDeDiez()),
            new BilleteDisponible(Cantidad: 99, Dinero.BilleteDeCinco()),
            new BilleteDisponible(Cantidad: 250, Dinero.MonedaDeDos()),
            new BilleteDisponible(Cantidad: 500, Dinero.MonedaDeUno())
        ]);
    }

    [Fact]
    public void ConsutarInventario_SiElCajeroSeQuedaSinFondos_DebeMostrarSinDenominacionesDisponibles()
    {
        _cajero = new Cajero();
        _cajero.Retirar(5100);

        List<BilleteDisponible> fondoDisponible = _cajero
            .ConsultarInventario();

        fondoDisponible.Should().BeEquivalentTo([
            new BilleteDisponible(Cantidad: 0, Dinero.BilleteDeQuinientos()),
            new BilleteDisponible(Cantidad: 0, Dinero.BilleteDeDoscientos()),
            new BilleteDisponible(Cantidad: 0, Dinero.BilleteDeCien()),
            new BilleteDisponible(Cantidad: 0, Dinero.BilleteDeCincuenta()),
            new BilleteDisponible(Cantidad: 0, Dinero.BilleteDeVeinte()),
            new BilleteDisponible(Cantidad: 0, Dinero.BilleteDeDiez()),
            new BilleteDisponible(Cantidad: 0, Dinero.BilleteDeCinco()),
            new BilleteDisponible(Cantidad: 0, Dinero.MonedaDeDos()),
            new BilleteDisponible(Cantidad: 0, Dinero.MonedaDeUno())
        ]);
    }

    [Fact]
    public void Retirar_SiRealizaRetirosIgualOMenorACero_DebeLanzarExcepcion()
    {
        _cajero = new Cajero();

        Action action = () => _cajero.Retirar(0);

        action.Should().ThrowExactly<ArgumentException>()
            .WithMessage(CajeroErrores.DebeRetirarMinimoUnaUnidad);
    }
}
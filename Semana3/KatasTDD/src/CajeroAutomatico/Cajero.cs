using CajeroAutomatico.Models;

namespace CajeroAutomatico;

public class Cajero
{
    private readonly List<MontoInventario> _inventario =
    [
        new (Cantidad: 2, Dinero.BilleteDeQuinientos()),
        new (Cantidad: 3, Dinero.BilleteDeDoscientos()),
        new (Cantidad: 5, Dinero.BilleteDeCien()),
        new (Cantidad: 12, Dinero.BilleteDeCincuenta()),
        new (Cantidad: 20, Dinero.BilleteDeVeinte()),
        new (Cantidad: 50, Dinero.BilleteDeDiez()),
        new (Cantidad: 100, Dinero.BilleteDeCinco()),
        new (Cantidad: 250, Dinero.MonedaDeDos()),
        new (Cantidad: 500, Dinero.MonedaDeUno())
    ];

    public List<MontoRetiro> Retirar(int valorARetirar)
    {
        LanzarExcepcionSiNoIngresaMontoPositivo(valorARetirar);
        LanzarExcepcionSiNoHayFondosSuficienes(valorARetirar);

        List<MontoRetiro> resultado = [];
        int valorRestante = valorARetirar;

        foreach (MontoInventario montoInventario in _inventario)
        {
            MontoRetiro montoCreado = CrearMontoRetiro(montoInventario, valorRestante);

            if (montoCreado.ExisteCantidad())
            {
                resultado.Add(montoCreado);

                valorRestante -= montoCreado.MultiplicarPorCantidad();

                montoInventario.ReducirCantidad(montoCreado.ObtenerCantidad());
            }
        }

        return resultado;
    }

    private static MontoRetiro CrearMontoRetiro(MontoInventario montoInventario, int valorRestante)
    {
        int unidadesPorCadaMonto = montoInventario
            .ObtenerCantidadDeUnidadesPorMonto(valorRestante);
            
        int cantidadDisponibleARetirar = Math
            .Min(unidadesPorCadaMonto, montoInventario.Cantidad);

        return new MontoRetiro(cantidadDisponibleARetirar, montoInventario.Dinero);
    }

    private static void LanzarExcepcionSiNoIngresaMontoPositivo(int montoSolicitado)
    {
        if (montoSolicitado <= 0)
            throw new ArgumentException(CajeroErrores
                .DebeRetirarMinimoUnaUnidad);
    }

    private void LanzarExcepcionSiNoHayFondosSuficienes(int montoSolicitado)
    {
        if (montoSolicitado > TotalInventario())
            throw new ArgumentException(CajeroErrores
                .ElCajeroNoDisponeDeDineroSuficienteParaEstaTransaccion);
    }

    public List<MontoInventario> ConsultarInventario() => _inventario;

    private int TotalInventario() => _inventario
        .Sum(monto => monto.MultiplicarPorCantidad());
}
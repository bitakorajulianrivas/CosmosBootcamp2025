namespace CajeroAutomatico.Models;

public record MontoInventario(int Cantidad, Dinero Dinero)
{
    public int Cantidad { get; private set; } = Cantidad;

    public int ObtenerCantidadDeUnidadesPorMonto(int monto) => monto / Dinero.Valor;

    public int ReducirCantidad(int cantidad) => Cantidad -= cantidad;

    public int MultiplicarPorCantidad() => Dinero.Valor * Cantidad;
}
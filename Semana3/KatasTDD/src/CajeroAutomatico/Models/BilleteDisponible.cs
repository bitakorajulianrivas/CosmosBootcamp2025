namespace CajeroAutomatico.Models;

public record BilleteDisponible(int Cantidad, Dinero Dinero)
{
    public int Cantidad { get; private set; } = Cantidad;

    public int Totalizar() => Dinero.Valor * Cantidad;

    public int ObtenerUnidadesDisponiblesParaRetirar(int valorRestante)
    {
        int unidadesPorCadaMonto = ObtenerCantidadDeBilletesPorValor(valorRestante);

        int cantidadDisponibleARetirar = Math
            .Min(unidadesPorCadaMonto, Cantidad);

        return cantidadDisponibleARetirar;
    }

    public void ReducirBilletesDisponibles(int cantidad) => Cantidad -= cantidad;

    private int ObtenerCantidadDeBilletesPorValor(int valor) => valor / Dinero.Valor;
}
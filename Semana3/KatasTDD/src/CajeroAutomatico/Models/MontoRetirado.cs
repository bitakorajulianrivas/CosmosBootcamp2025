namespace CajeroAutomatico.Models;

public record MontoRetirado(Dinero Dinero, int Cantidad)
{
    public bool EsMasDeUnaUnidad() => Cantidad > 1;
}
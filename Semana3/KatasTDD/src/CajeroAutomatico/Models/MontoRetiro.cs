namespace CajeroAutomatico.Models;

public record MontoRetiro(Dinero Dinero, int Cantidad)
{
    public bool EsMasDeUnaUnidad() => Cantidad > 1;
}
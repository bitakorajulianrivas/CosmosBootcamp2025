using CajeroAutomatico.Enums;

namespace CajeroAutomatico.Models;

public class MontoRetiro(int cantidad, Dinero dinero)
{
    public int ObtenerCantidad() => cantidad;
    public int ObtenerValor() => dinero.Valor;
    public Tipo ObtenerTipo() => dinero.Tipo;

    public bool EsMasDeUnaUnidad() => cantidad > 1;
    public bool ExisteCantidad() => cantidad > 0;
    public int MultiplicarPorCantidad() => dinero.Valor * cantidad;
}
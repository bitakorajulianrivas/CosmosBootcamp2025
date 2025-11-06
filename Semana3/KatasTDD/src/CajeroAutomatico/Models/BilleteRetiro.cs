using CajeroAutomatico.Enums;

namespace CajeroAutomatico.Models;

public class BilleteRetiro(int cantidad, Dinero dinero)
{
    public int ObtenerCantidad() => cantidad;
    public int ObtenerValor() => dinero.Valor;
    public Tipo ObtenerTipo() => dinero.Tipo;

    public bool EsMasDeUnaUnidad() => cantidad > 1;
    public bool TieneBilletes() => cantidad > 0;
    public int Totalizar() => dinero.Valor * cantidad;
}
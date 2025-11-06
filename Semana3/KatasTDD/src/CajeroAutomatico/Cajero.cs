using CajeroAutomatico.Models;

namespace CajeroAutomatico;

public class Cajero : ICajero
{
    private readonly List<BilleteDisponible> _billetesDisponibles;

    public Cajero()
    {
        _billetesDisponibles = [
            new BilleteDisponible(Cantidad: 2, Dinero.BilleteDeQuinientos()),
            new BilleteDisponible(Cantidad: 3, Dinero.BilleteDeDoscientos()),
            new BilleteDisponible(Cantidad: 5, Dinero.BilleteDeCien()),
            new BilleteDisponible(Cantidad: 12, Dinero.BilleteDeCincuenta()),
            new BilleteDisponible(Cantidad: 20, Dinero.BilleteDeVeinte()),
            new BilleteDisponible(Cantidad: 50, Dinero.BilleteDeDiez()),
            new BilleteDisponible(Cantidad: 100, Dinero.BilleteDeCinco()),
            new BilleteDisponible(Cantidad: 250, Dinero.MonedaDeDos()),
            new BilleteDisponible(Cantidad: 500, Dinero.MonedaDeUno())
        ];
    }

    public List<BilleteRetiro> Retirar(int valorARetirar)
    {
        LanzarExcepcionSiElValorARetirarEsMenorOIgualACero(valorARetirar);
        LanzarExcepcionSiNoHayFondosSuficienes(valorARetirar);

        List<BilleteRetiro> billetesARetirar = [];
        int valorRestanteARetirar = valorARetirar;

        foreach (BilleteDisponible billeteDisponible in _billetesDisponibles)
        {
            BilleteRetiro billeteARetirar = 
                RetirarCantidadDeBilletesPorCadaCifra(billeteDisponible, valorRestanteARetirar);

            if (billeteARetirar.TieneBilletes() == false)
                continue;

            billetesARetirar.Add(billeteARetirar);

            billeteDisponible.ReducirBilletesDisponibles(billeteARetirar.ObtenerCantidad());

            valorRestanteARetirar -= billeteARetirar.Totalizar();
        }

        return billetesARetirar;
    }

    public List<BilleteDisponible> ConsultarInventario() => _billetesDisponibles;

    private static BilleteRetiro RetirarCantidadDeBilletesPorCadaCifra(
        BilleteDisponible billeteDisponible, int valorRestante)
    {
        int cantidadBilleteARetirar = billeteDisponible
            .ObtenerUnidadesDisponiblesParaRetirar(valorRestante);

        return new BilleteRetiro(cantidadBilleteARetirar,
            billeteDisponible.Dinero);
    }

    private static void LanzarExcepcionSiElValorARetirarEsMenorOIgualACero(int valorARetirar)
    {
        if (valorARetirar <= 0)
            throw new ArgumentException(CajeroErrores
                .DebeRetirarMinimoUnaUnidad);
    }

    private void LanzarExcepcionSiNoHayFondosSuficienes(int valorARetirar)
    {
        if (valorARetirar > TotalizarInventario())
            throw new ArgumentException(CajeroErrores
                .ElCajeroNoDisponeDeDineroSuficienteParaEstaTransaccion);
    }

    private int TotalizarInventario() => _billetesDisponibles
        .Sum(monto => monto.Totalizar());
}

public interface ICajero
{
    List<BilleteRetiro> Retirar(int valorARetirar);
    List<BilleteDisponible> ConsultarInventario();
}
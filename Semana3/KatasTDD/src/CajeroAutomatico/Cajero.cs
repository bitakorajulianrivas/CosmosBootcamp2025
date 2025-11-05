using CajeroAutomatico.Models;

namespace CajeroAutomatico;

public class Cajero
{
    private readonly List<Dinero> _dineros = [
        Dinero.BilleteDe(500),
        Dinero.BilleteDe(200),
        Dinero.BilleteDe(100),
        Dinero.BilleteDe(50),
        Dinero.BilleteDe(20),
        Dinero.BilleteDe(10),
        Dinero.BilleteDe(5),
        Dinero.MonedaDe(2),
        Dinero.MonedaDe(1)];

    public List<MontoRetiro> Retirar(int montoSolicitado)
    {
        List<MontoRetiro> montoARetirar = [];

        foreach (var dinero in _dineros)
        {
            int unidadesPorMonto = dinero
                .ObtenerUnidadesAPartirDe(montoSolicitado);

            if(unidadesPorMonto > 0)
                montoARetirar.Add(new MontoRetiro(dinero, unidadesPorMonto));

            montoSolicitado = montoSolicitado % dinero.ObtenerValor();
        }

        return montoARetirar;
    }
}
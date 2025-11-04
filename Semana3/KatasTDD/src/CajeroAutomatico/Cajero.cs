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

    private Dictionary<int, int> _inventario = new()
    { 
        //Valor, Cantidad
        { 500, 2 },
        { 200, 3 },
        { 100, 5 },
        { 50, 12 },
        { 20, 20 },
        { 10, 50 },
        { 5, 100 },
        { 2, 250 },
        { 1, 500 }
    };

    public List<MontoRetirado> Retirar(int montoSolicitado)
    {
        var montoARetirar = new List<MontoRetirado>();

        foreach (var dinero in _dineros)
        {
            int unidadesPorMonto = dinero
                .ObtenerUnidadesAPartirDe(montoSolicitado);

            if(unidadesPorMonto > 0)
                montoARetirar.Add(
                    new MontoRetirado(dinero, unidadesPorMonto));

            montoSolicitado = montoSolicitado % dinero.ObtenerValor();
        }

        return montoARetirar;
    }
}
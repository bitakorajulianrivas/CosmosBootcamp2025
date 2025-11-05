using System.Runtime.CompilerServices;
using CajeroAutomatico.Models;

namespace CajeroAutomatico;

public class Cajero
{
    private readonly List<Dinero> _dinerosEstablecidos =
    [
        Dinero.BilleteDe(500),
        Dinero.BilleteDe(200),
        Dinero.BilleteDe(100),
        Dinero.BilleteDe(50),
        Dinero.BilleteDe(20),
        Dinero.BilleteDe(10),
        Dinero.BilleteDe(5),
        Dinero.MonedaDe(2),
        Dinero.MonedaDe(1)
    ];

    private readonly Dictionary<int, int> _fondosDisponibles = new()
    {
        //valor, cantidad
        {500, 2},
        {200, 3},
        {100, 5},
        {50, 12},
        {20, 20},
        {10, 50},
        {5, 100},
        {2, 250},
        {1, 500}
    };

    public List<MontoRetiro> Retirar(int montoSolicitado)
    {
        LanzarExcepcionSiNoRetiraUnidades(montoSolicitado);
        LanzarExcepcionSiNoDisponeDeFondosSuficienes(montoSolicitado);

        List<MontoRetiro> resultado = [];
        int valorRestante = montoSolicitado;

        foreach (var dinero in _dinerosEstablecidos)
        {
            int unidadesDivisiblesPorMonto = dinero
                .ObtenerUnidadesDivisiblesAPartirDe(valorRestante);

            int cantidadDeUnidadesDiponibles = _fondosDisponibles[dinero.Valor];

            int cantidadDeUnidadesARetirar = Math
                .Min(unidadesDivisiblesPorMonto,
                    cantidadDeUnidadesDiponibles);

            if (cantidadDeUnidadesARetirar <= 0)
                continue;

            resultado.Add(new MontoRetiro(dinero, cantidadDeUnidadesARetirar));
            valorRestante -= dinero.Valor * cantidadDeUnidadesARetirar;
            _fondosDisponibles[dinero.Valor] -= cantidadDeUnidadesARetirar;
        }

        return resultado;
    }

    private static void LanzarExcepcionSiNoRetiraUnidades(int montoSolicitado)
    {
        if (montoSolicitado <= 0)
            throw new ArgumentException(CajeroErrores.DebeRetirarMinimoUnaUnidad);
}

    private void LanzarExcepcionSiNoDisponeDeFondosSuficienes(int montoSolicitado)
    {
        if (montoSolicitado > TotalFondoDisponible())
            throw new ArgumentException(CajeroErrores.ElCajeroNoDisponeDeDineroSuficienteParaEstaTransaccion);
    }

    private int TotalFondoDisponible() => _fondosDisponibles
        .Sum(elemento => elemento.Key * elemento.Value);

    public List<MontoDisponible> ConsutarFondoDisponible()
    {
        return (from fondo in _fondosDisponibles
            join dinero in _dinerosEstablecidos
                on fondo.Key equals dinero.Valor
            select new MontoDisponible(dinero, fondo.Value))
            .ToList();
    }
}
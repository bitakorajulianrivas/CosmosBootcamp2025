using CajeroAutomatico.Enums;

namespace CajeroAutomatico.Models;

public class Dinero
{
    private readonly Tipo _tipo;

    public static Dinero MonedaDe(int valor) => new (Tipo.Moneda, valor);
    public static Dinero BilleteDe(int valor) => new (Tipo.Billete, valor);

    private Dinero(Tipo tipo, int valor)
    {
        _tipo = tipo;
        Valor = valor;
    }

    public int Valor { get; }

    public int ObtenerUnidadesDivisiblesAPartirDe(int monto) => monto / Valor;

    public string ObtenerTipoEnMinuscula() => _tipo.ToString().ToLower();
}
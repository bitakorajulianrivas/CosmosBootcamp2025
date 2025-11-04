using CajeroAutomatico.Enums;

namespace CajeroAutomatico.Models;

public class Dinero
{
    private readonly Tipo _tipo;
    private readonly int _valor;

    public static Dinero MonedaDe(int valor) => new (Tipo.Moneda, valor);
    public static Dinero BilleteDe(int valor) => new (Tipo.Billete, valor);

    private Dinero(Tipo tipo, int valor)
    {
        _tipo = tipo;
        _valor = valor;
    }

    public int ObtenerValor() => _valor;

    public int ObtenerUnidadesAPartirDe(int monto) => monto / _valor;

    public string ObtenerTipoEnMinuscula() => _tipo.ToString().ToLower();
}
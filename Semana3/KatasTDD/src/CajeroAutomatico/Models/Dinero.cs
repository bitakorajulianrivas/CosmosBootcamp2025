using CajeroAutomatico.Enums;

namespace CajeroAutomatico.Models;

public class Dinero
{
    public static Dinero BilleteDeQuinientos() => new(Tipo.Billete, 500);

    public static Dinero BilleteDeDoscientos() => new(Tipo.Billete, 200);

    public static Dinero BilleteDeCien() => new(Tipo.Billete, 100);

    public static Dinero BilleteDeCincuenta() => new(Tipo.Billete, 50);

    public static Dinero BilleteDeVeinte() => new(Tipo.Billete, 20);

    public static Dinero BilleteDeDiez() => new(Tipo.Billete, 10);

    public static Dinero BilleteDeCinco() => new(Tipo.Billete, 5);

    public static Dinero MonedaDeDos() => new(Tipo.Moneda, 2);

    public static Dinero MonedaDeUno() => new(Tipo.Moneda, 1);

    private Dinero(Tipo tipo, int valor)
    {
        Tipo = tipo;
        Valor = valor;
    }

    public int Valor { get; }
    public Tipo Tipo { get; }
}
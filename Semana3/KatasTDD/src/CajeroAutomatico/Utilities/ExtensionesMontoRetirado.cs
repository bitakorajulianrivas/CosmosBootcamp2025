using CajeroAutomatico.Models;

namespace CajeroAutomatico.Utilities;

public static class ExtensionesMontoRetirado
{
    private const string FormatoValorEnTexto = "{0} {1} de {2}";

    public static IEnumerable<string> ConvertirATexto(this IEnumerable<MontoRetirado> montos) => 
        montos.Select(montoRetirado => montoRetirado
            .ConvertirATexto());

    private static string ConvertirATexto(this MontoRetirado monto) =>
        string.Format(FormatoValorEnTexto,
            monto.Cantidad,
            monto.ConvertirTipoEnPrural(),
            monto.Dinero.ObtenerValor());

    private static string ConvertirTipoEnPrural(this MontoRetirado monto) =>
        monto.EsMasDeUnaUnidad()
            ? monto.Dinero.ObtenerTipoEnMinuscula() + "s"
            : monto.Dinero.ObtenerTipoEnMinuscula();
}
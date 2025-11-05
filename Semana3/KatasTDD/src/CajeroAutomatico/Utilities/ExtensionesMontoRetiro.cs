using CajeroAutomatico.Models;

namespace CajeroAutomatico.Utilities;

public static class ExtensionesMontoRetiro
{
    private const string FormatoValorEnTexto = "{0} {1} de {2}";

    public static IEnumerable<string> ConvertirATexto(this IEnumerable<MontoRetiro> montos) => 
        montos.Select(montoRetirado => montoRetirado
            .ConvertirATexto());

    private static string ConvertirATexto(this MontoRetiro monto) =>
        string.Format(FormatoValorEnTexto,
            monto.ObtenerCantidad(),
            monto.ConvertirTipoEnPrural(),
            monto.ObtenerValor());

    private static string ConvertirTipoEnPrural(this MontoRetiro monto) =>
        monto.EsMasDeUnaUnidad()
            ? monto.ObtenerTipo().ToString().ToLower() + "s"
            : monto.ObtenerTipo().ToString().ToLower();
}
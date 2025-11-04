using System.Text;

namespace CajeroAutomatico;

public class Cajero
{
    private const string FormatoValorEnTexto = "{0} {1} de {2}";

    private static readonly Dictionary<int, string> DenominacionesPorTipo = new()
    {
        { 500, "billete" },
        { 200, "billete" },
        { 100, "billete" },
        { 50,  "billete" },
        { 20,  "billete" },
        { 10,  "billete" },
        { 5,   "billete" },
        { 2,   "moneda" },
        { 1,   "moneda" }
    };
    
    private static readonly Dictionary<int, int> DenominacionesPorCantidad = new()
    {
        { 500, 2 },
        { 200, 3 },
        { 100, 5 },
        { 50,  12 },
        { 20,  20 },
        { 10,  50 },
        { 5,   100 },
        { 2,   250 },
        { 1,   500 }
    };

    public string[] Retirar(int montoSolicitado)
    {
        var denominacionesRetornadas = new List<string>();
        
        foreach (var denominacion in DenominacionesPorTipo)
        {
            int unidadesPorDenominacion = montoSolicitado / denominacion.Key;

            if (unidadesPorDenominacion != 0)
            {
                string tipoDenominacion = unidadesPorDenominacion > 1
                    ? denominacion.Value + "s" : denominacion.Value;

                var denominacionRetornada = 
                    string.Format(FormatoValorEnTexto,
                        unidadesPorDenominacion,
                        tipoDenominacion,
                        denominacion.Key);
                    
                denominacionesRetornadas.Add(denominacionRetornada);
                
                montoSolicitado %= denominacion.Key;
            }
        }
        
        return  denominacionesRetornadas.ToArray();
    }

    public string MostrarDistribucion()
    {
        string contenido = "| Valor | Tipo    | Número de unidades|\n" +
                           "|-------|---------|-------------------|\n";
        
        foreach (var denominacion in DenominacionesPorTipo)
        {
            contenido += $"| {denominacion.Key}   | " +
                         $"{DenominacionesPorTipo[denominacion.Key]}    | " +
                         $"{DenominacionesPorCantidad[denominacion.Key]}                 |\n";
        }

        return contenido;
    }
}
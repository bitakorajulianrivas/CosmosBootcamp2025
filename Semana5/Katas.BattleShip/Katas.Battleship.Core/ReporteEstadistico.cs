namespace Katas.Battleship.Core;

public class ReporteEstadistico(int disparosAcertados, int disparosFallidos, List<Barco> barcosDerribados) : IReporte
{
    private const string MensajeTotalDisparos = "Total disparos: {0}.\n" + "Perdidos: {1}.\n" + "Acertados: {2}.\n";
    private const string MensajeBarcosDerribados = "Barcos derribados: [{0}]";

    public string Imprimir()
    {
        int disparosTotales = disparosAcertados + disparosFallidos;
        
        var resultado = string.Format(MensajeTotalDisparos, disparosTotales, disparosFallidos, disparosAcertados);

        var barcos = barcosDerribados
            .Select(barco => barco.ToString())
            .ToArray();
        
        string informeBarcosDerribados =
            barcosDerribados.Any()
                ? string.Format(MensajeBarcosDerribados, string.Join("\n", barcos))
                : string.Empty;

        return resultado + informeBarcosDerribados;
    }
}
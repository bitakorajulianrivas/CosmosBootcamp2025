namespace Katas.Battleship.Core;

public class Reporte(char[,] tablero, int disparosAcertados, int disparosFallidos, List<Barco> barcosDerribados)
{
    private const char SaltoDeLinea = '\n';
    private const string Separador = "-------------------------------------------|";
    private const string MensajeTotalDisparos = "Total disparos: {0}.\n" + "Perdidos: {1}.\n" + "Acertados: {2}.\n";

    private readonly string _tablaEncabezado = 
        SaltoDeLinea + 
        "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | " + SaltoDeLinea +
        Separador + " " + 
        SaltoDeLinea;
    
    private readonly string _tablaPieDePagina = 
        Separador + " \n" + SaltoDeLinea;


    public string ImprimirTablero()
    {
        int columnas = 10;
        int filas = 10;

        
        string respuesta = _tablaEncabezado;

        for (int columna = 0; columna < columnas; columna++)
        {
            respuesta += $" {columna} |";
            
            for (int fila = 0; fila < filas; fila++)
            {
                var casilla = tablero[fila, columna] == '\0' 
                    ? ' ' : tablero[fila, columna];
                
                respuesta += $" {casilla} |";
            }

            respuesta += " " + SaltoDeLinea;
        }
        
        respuesta += _tablaPieDePagina;

        return respuesta;
    }

    public string ImprimirReporte()
    {
        int disparosTotales = disparosAcertados + disparosFallidos;
        
        var resultado = string.Format(MensajeTotalDisparos, disparosTotales, disparosFallidos, disparosAcertados);

        var barcos = barcosDerribados
            .Select(barco => barco.ToString())
            .ToArray();
        
        string informeBarcosDerribados =
            barcosDerribados.Any()
                ? "Barcos derribados: [" + string.Join("\n", barcos) + "]"
                : string.Empty;

        return resultado + informeBarcosDerribados;
    }
}
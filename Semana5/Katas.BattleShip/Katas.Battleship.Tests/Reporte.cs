namespace Katas.Battleship.Tests;

public class Reporte
{
    private readonly char[,] _tablero;
    private readonly int _disparosAcertados;
    private readonly int _disparosFallidos;
    private readonly List<Barco> _barcosDerribados;

    public Reporte(char[,] tablero, int disparosAcertados, int disparosFallidos, List<Barco> barcosDerribados)
    {
        _tablero = tablero;
        _disparosAcertados = disparosAcertados;
        _disparosFallidos = disparosFallidos;
        _barcosDerribados = barcosDerribados;
    }

    public string ImprimirTablero()
    {
        int columnas = 10;
        int filas = 10;
        
        string respuestaTablero = "\n" +
                                  "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                  "-------------------------------------------| \n";

        for (int columna = 0; columna < columnas; columna++)
        {
            respuestaTablero += $" {columna} |";
            
            for (int fila = 0; fila < filas; fila++)
            {
                var casilla = _tablero[fila, columna] == '\0' 
                    ? ' ' : _tablero[fila, columna];
                
                respuestaTablero += $" {casilla} |";
            }

            respuestaTablero += " \n";
        }

        respuestaTablero +=
            "-------------------------------------------| \n" +
            "\n";

        return respuestaTablero;
    }

    public string ImprimirReporte()
    {
        int disparosTotales = _disparosAcertados + _disparosFallidos;
        
        var resultado = string.Format(
            "Total disparos: {0}.\n" + "Perdidos: {1}.\n" + "Acertados: {2}.\n",
            disparosTotales, _disparosFallidos, _disparosAcertados);

        var barcos = _barcosDerribados
            .Select(barco => string.Format("{0}: ({1},{2}).\n",
                barco.Tipo, barco.Posicion.EjeX, barco.Posicion.EjeY))
            .ToArray();
        
        string informeBarcosDerribados =
            _barcosDerribados.Any()
                ? "Barcos derribados: [" + string.Join("", barcos) + "]"
                : string.Empty;

        return resultado + informeBarcosDerribados;
    }
}
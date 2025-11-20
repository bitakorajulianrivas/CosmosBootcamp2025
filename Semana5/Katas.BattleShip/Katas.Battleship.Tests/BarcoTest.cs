using FluentAssertions;

namespace Katas.Battleship.Tests;

public class BarcoTest
{
    [Fact]
    public void Si_ObtieneCoordenadas_Debe_RetornarCantidadDePosiciones()
    {
        var barco = Barco.Carrier(Posicion.Horizontal(1, 1));

        barco.ObtenerCoordenadas().Should().BeEquivalentTo([
            (1, 1), (2, 1), (3, 1), (4, 1)
        ]);
    }

    [Fact]
    public void Si_ObtieneCoordenadas_Debe_RetornarCantidadDePosicionesVerticales()
    {
        var barco = Barco.Carrier(Posicion.Vertical(1, 1));

        barco.ObtenerCoordenadas().Should().BeEquivalentTo([
            (1, 1), (1, 2), (1, 3), (1, 4)
        ]);
    }
}
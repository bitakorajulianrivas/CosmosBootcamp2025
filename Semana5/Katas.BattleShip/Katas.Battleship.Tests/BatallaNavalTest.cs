using FluentAssertions;
using Katas.Battleship.Core;

namespace Katas.Battleship.Tests;

public class BatallaNavalTest
{
    [Fact]
    public void Si_AgregoJugador3_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNaval();
        var jugador1 = new Jugador("pollo");
        batallaNaval.AgregarJugador(jugador1);
        var jugador2 = new Jugador("gato");
        batallaNaval.AgregarJugador(jugador2);
        var jugador3 = new Jugador("perro");

        Action action = () => batallaNaval.AgregarJugador(jugador3);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Solo se permiten 2 jugadores.");
    }

    [Fact]
    public void Si_AgregoJugadores_Debe_ExistirUnTableroVacioParaCadaJugador()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 1 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 2 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 3 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 4 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 5 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        var tableroJugador1 = batallaNaval.Imprimir();
        batallaNaval.FinalizarTurno();
        var tableroJugador2 = batallaNaval.Imprimir();

        tableroJugador1.Should().Be(tableroEsperado);
        tableroJugador2.Should().Be(tableroEsperado);
    }

    [Fact]
    public void SI_InicioSinJugadores_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .Construir();

        var action = () => batallaNaval.Iniciar();

        action.Should().Throw<ArgumentException>()
            .WithMessage("No Estan los Jugadores Configurados.");
    }

    [Fact]
    public void Si_AgregoBarcosAlJuego_Debe_ExistirBarcosEnElTablero()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [Barco.Gunship(Posicion.Horizontal(2, 2))])
            .AgregarJugador("Gato")
            .Construir();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 1 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 2 |   |   | G |   |   |   |   |   |   |   | \n" +
                                 " 3 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 4 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 5 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        var tableroJugador1 = batallaNaval.Imprimir();

        tableroJugador1.Should().Be(tableroEsperado);
    }

    [Fact]
    public void SI_Agrego2BarcoEnLaMismaPosicion_Debe_LanzarExcepcion()
    {
        Action action = () => new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Gunship(Posicion.Horizontal(2, 2)),
                Barco.Gunship(Posicion.Horizontal(2, 2))
            ]);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Ya existe barco en la posición enviada.");
    }

    [Fact]
    public void Si_AgregoDestroyer_Debe_ExistirBarcoConTresCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [Barco.Destroyer(Posicion.Horizontal(1, 1))])
            .AgregarJugador("Gato")
            .Construir();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 1 |   | D | D | D |   |   |   |   |   |   | \n" +
                                 " 2 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 3 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 4 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 5 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        var tableroJugador1 = batallaNaval.Imprimir();

        tableroJugador1.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_AgregoDestroyerConOrientacionVertical_Debe_ExistirBarcoConTresCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [Barco.Destroyer(Posicion.Vertical(1, 1))])
            .AgregarJugador("Gato")
            .Construir();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 1 |   | D |   |   |   |   |   |   |   |   | \n" +
                                 " 2 |   | D |   |   |   |   |   |   |   |   | \n" +
                                 " 3 |   | D |   |   |   |   |   |   |   |   | \n" +
                                 " 4 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 5 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        var tableroJugador1 = batallaNaval.Imprimir();

        tableroJugador1.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_AgregoCarrier_DebeExistirBarcoConCuatroCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [Barco.Carrier(Posicion.Horizontal(1, 1))])
            .AgregarJugador("Gato")
            .Construir();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 1 |   | C | C | C | C |   |   |   |   |   | \n" +
                                 " 2 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 3 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 4 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 5 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        var tableroJugador1 = batallaNaval.Imprimir();

        tableroJugador1.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_AgregoCarrierConOrientacionVertical_DebeExistirBarcoConCuatroCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [Barco.Carrier(Posicion.Vertical(1, 1))])
            .AgregarJugador("Gato")
            .Construir();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 1 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 2 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 3 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 4 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 5 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        var tableroJugador1 = batallaNaval.Imprimir();

        tableroJugador1.Should().Be(tableroEsperado);
    }

    [Fact]
    public void SI_Tengo1CarrierYAgregoOtro_Debe_LanzarExcepcion()
    {
        Action action = () => new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Carrier(Posicion.Horizontal(5, 5))
            ]);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Solo se puede asignar 1 barcos de tipo Carrier.");
    }

    [Fact]
    public void Si_TengoUnCarrierYDosDestroyerYAgregoUnTercero_Debe_LanzarExcepcion()
    {
        Action action = () => new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Destroyer(Posicion.Horizontal(4, 4))
            ]);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Solo se puede asignar 2 barcos de tipo Destroyer.");
    }

    [Fact]
    public void Si_TengoCuatroGunshipsYAgregoUnQuinto_Debe_LanzarExcepcion()
    {
        Action action = () => new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Gunship(Posicion.Horizontal(1, 1)),
                Barco.Gunship(Posicion.Horizontal(2, 2)),
                Barco.Gunship(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5))
            ]);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Solo se puede asignar 4 barcos de tipo Gunship.");
    }

    [Theory]
    [InlineData(11, 11)]
    [InlineData(-1, -1)]
    [InlineData(11, -1)]
    [InlineData(-1, 8)]
    [InlineData(9, -8)]
    [InlineData(10, -1)]
    public void Si_AgregoBarcosQueEsteFueraDelasCoordenadas_Debe_LanzarExcepcion(int x, int y)
    {
        Action action = () => new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [Barco.Gunship(Posicion.Horizontal(x, y))]);

        action.Should().Throw<ArgumentException>()
            .WithMessage("El barco se encuentra fuera del tablero.");
    }

    [Fact]
    public void Si_AgregoCarrierHorizontalEnPosicion8_0_Debe_LanzarExcepcion()
    {
        Action action = () => new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [Barco.Carrier(Posicion.Horizontal(8, 0))]);
   
        action.Should().Throw<ArgumentException>()
            .WithMessage("El barco se encuentra fuera del tablero.");
    }

    [Fact]
    public void Si_InicioJuego_Debe_CadaJugadorDebeTener7BarcosAsignado()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();

        var tableroJugador1Esperado = batallaNaval.Imprimir();
        batallaNaval.FinalizarTurno();
        var tableroJugador2Esperado = batallaNaval.Imprimir();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 1 |   | C | C | C | C |   |   |   |   |   | \n" +
                                 " 2 |   |   | D | D | D |   |   |   |   |   | \n" +
                                 " 3 |   |   |   | D | D | D |   |   |   |   | \n" +
                                 " 4 |   |   |   |   | G |   |   |   |   |   | \n" +
                                 " 5 |   |   |   |   |   | G |   |   |   |   | \n" +
                                 " 6 |   |   |   |   |   |   | G |   |   |   | \n" +
                                 " 7 |   |   |   |   |   |   |   | G |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        tableroJugador1Esperado.Should().Be(tableroEsperado);
        tableroJugador2Esperado.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_InicioJuegoYNoTengoLos7BarcosAsignadoPorJugador1_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6))
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .Construir();

        Action action = () => batallaNaval.Iniciar();

        action.Should().Throw<ArgumentException>()
            .WithMessage("Falta barcos por asignar.");
    }

    [Fact]
    public void Si_InicioJuegoYNoTengoLos7BarcosAsignadoPorJugador2_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
            ])
            .Construir();

        Action action = () => batallaNaval.Iniciar();

        action.Should().Throw<ArgumentException>()
            .WithMessage("Falta barcos por asignar.");
    }

    [Fact]
    public void Si_ImprimeSinBarcos_Debe_MostrarElTableroDelJugador1Vacio()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 1 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 2 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 3 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 4 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 5 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        string tablero = batallaNaval.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_ImprimeTableroDelJugador1_Debe_MostrarElTableroConBarcos()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato")
            .Construir();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 1 |   | C | C | C | C |   |   |   |   |   | \n" +
                                 " 2 |   |   | D | D | D |   |   |   |   |   | \n" +
                                 " 3 |   |   |   | D | D | D |   |   |   |   | \n" +
                                 " 4 |   |   |   |   | G |   |   |   |   |   | \n" +
                                 " 5 |   |   |   |   |   | G |   |   |   |   | \n" +
                                 " 6 |   |   |   |   |   |   | G |   |   |   | \n" +
                                 " 7 |   |   |   |   |   |   |   | G |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        string tablero = batallaNaval.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_ImprimeTableroDelJugador2_DebeMostrarElTableroConBarcos()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.FinalizarTurno();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   | D | D | D |   |   |   |   |   |   | \n" +
                                 " 1 |   |   |   |   |   |   |   |   | D |   | \n" +
                                 " 2 | G |   |   |   |   |   |   |   | D |   | \n" +
                                 " 3 |   |   |   |   |   |   |   |   | D |   | \n" +
                                 " 4 |   | C |   | G |   |   |   |   |   |   | \n" +
                                 " 5 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   | C |   |   |   |   | G |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 | G |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        string tablero = batallaNaval.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_DisparaSinIniciarElJuego_DebeLanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        Action action = () => batallaNaval.Disparar(x: 0, y: 0);

        action.Should().Throw<ArgumentException>()
            .WithMessage("No puede disparar sin iniciar el juego.");
    }

    [Fact]
    public void
        Si_ElJugador1Dispara_Debe_MarcarlaCasillaApuntadaEnTableroDelJugador2YtenerUnTableroAuxiliarConLosDisparosRealizados()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        batallaNaval.Disparar(0, 0);
        batallaNaval.FinalizarTurno();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 | o | D | D | D |   |   |   |   |   |   | \n" +
                                 " 1 |   |   |   |   |   |   |   |   | D |   | \n" +
                                 " 2 | G |   |   |   |   |   |   |   | D |   | \n" +
                                 " 3 |   |   |   |   |   |   |   |   | D |   | \n" +
                                 " 4 |   | C |   | G |   |   |   |   |   |   | \n" +
                                 " 5 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   | C |   |   |   |   | G |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 | G |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";


        string tablero = batallaNaval.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void SiDisparoDosVecesEnLaMismaCoordenada_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        //Jugador 1
        batallaNaval.Disparar(1, 4);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 1);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        Action action = () => batallaNaval.Disparar(1, 4);

        action.Should().Throw<ArgumentException>()
            .WithMessage("No puede disparar en una misma posición.");
    }

    [Fact]
    public void Si_ElJugador1DisparayAtina_Debe_MarcarlaCasillaConXDelJugador2YTableroDisparoX()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        batallaNaval.Disparar(1, 4);
        batallaNaval.FinalizarTurno();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   | D | D | D |   |   |   |   |   |   | \n" +
                                 " 1 |   |   |   |   |   |   |   |   | D |   | \n" +
                                 " 2 | G |   |   |   |   |   |   |   | D |   | \n" +
                                 " 3 |   |   |   |   |   |   |   |   | D |   | \n" +
                                 " 4 |   | x |   | G |   |   |   |   |   |   | \n" +
                                 " 5 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   | C |   |   |   |   | G |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 | G |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";


        string tablero = batallaNaval.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_Realizo1DisparosYFinalizaTurno_Debe_CambiarJugadorActualYJugadorOponente()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        batallaNaval.Disparar(1, 4);
        batallaNaval.FinalizarTurno();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   | D | D | D |   |   |   |   |   |   | \n" +
                                 " 1 |   |   |   |   |   |   |   |   | D |   | \n" +
                                 " 2 | G |   |   |   |   |   |   |   | D |   | \n" +
                                 " 3 |   |   |   |   |   |   |   |   | D |   | \n" +
                                 " 4 |   | x |   | G |   |   |   |   |   |   | \n" +
                                 " 5 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   | C |   |   |   |   | G |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 | G |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";


        string tablero = batallaNaval.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_Realizo2Disparos_Debe_CambiarJugadorActualYJugadorOponente()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        //Jugador 1
        batallaNaval.Disparar(1, 4);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 1);
        batallaNaval.FinalizarTurno();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 1 |   | x | C | C | C |   |   |   |   |   | \n" +
                                 " 2 |   |   | D | D | D |   |   |   |   |   | \n" +
                                 " 3 |   |   |   | D | D | D |   |   |   |   | \n" +
                                 " 4 |   |   |   |   | G |   |   |   |   |   | \n" +
                                 " 5 |   |   |   |   |   | G |   |   |   |   | \n" +
                                 " 6 |   |   |   |   |   |   | G |   |   |   | \n" +
                                 " 7 |   |   |   |   |   |   |   | G |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        string tablero = batallaNaval.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_PresentaInformeDeBatalla_DebeRetornarInforme()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        //Jugador 1
        batallaNaval.Disparar(1, 4);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 1);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(1, 0);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(2, 2);
        batallaNaval.FinalizarTurno();

        var informeEsperado =
            "Total disparos: 2.\n" +
            "Perdidos: 0.\n" +
            "Acertados: 2.\n";

        batallaNaval.Imprimir(esReporte: true).Should().Be(informeEsperado);
    }

    [Fact]
    public void Si_PresentaInformeDeBatalla_DebeRetornarInformeConDisparosFallidos()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato",
            [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        //Jugador 1
        batallaNaval.Disparar(1, 5);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 2);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(1, 1);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(2, 3);
        batallaNaval.FinalizarTurno();

        var informeEsperado =
            "Total disparos: 2.\n" +
            "Perdidos: 2.\n" +
            "Acertados: 0.\n";

        batallaNaval.Imprimir(esReporte: true).Should().Be(informeEsperado);
    }

    [Fact]
    public void Si_PresentaInformeDeBatalla_Debe_RetornarInformeConBarcosDerribados()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        //Jugador 1
        batallaNaval.Disparar(0, 2);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 1);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(1, 0);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(2, 2);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(2, 0);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(2, 3);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(3, 0);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(2, 4);

        var informeEsperado =
            "Total disparos: 4.\n" +
            "Perdidos: 0.\n" +
            "Acertados: 4.\n" +
            "Barcos derribados: [" +
            "Destroyer: (1,0).\n" +
            "Gunship: (0,2).]";

        batallaNaval.Imprimir(esReporte: true).Should().Be(informeEsperado);
    }

    [Fact]
    public void Si_RealizoUnDisparoFallido_Debe_RetornarMensajeTiroFallido()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        //Jugador 1
        string mensaje = batallaNaval.Disparar(0, 5);

        mensaje.Should().Be("Disparo fallido en la posicion (0, 5)");
    }

    [Fact]
    public void Si_RealizoUnDisparoAcertado_Debe_RetornarMensajeTiroAcertado()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        //Jugador 1
        string mensaje = batallaNaval.Disparar(1, 4);

        mensaje.Should().Be("Disparo acertado en la posicion (1, 4)");
    }

    [Fact]
    public void Si_RealizoUnDisparoAcertadoHundiendoElBarco_Debe_RetornarMensajeBarcoHundido()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        //Jugador 1
        string mensaje = batallaNaval.Disparar(0, 2);

        mensaje.Should().Be("Se ha hundido el barco Gunship (0, 2)");
    }

    [Fact]
    public void SiElBarcoEstaHundido_Debe_CambiarXMinusculasPorMayusculas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        //Jugador 1
        batallaNaval.Disparar(1, 0);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 1);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(2, 0);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(2, 2);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(3, 0);
        batallaNaval.FinalizarTurno();

        string tableroEsperado = "\n" +
                                 "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                 "-------------------------------------------| \n" +
                                 " 0 |   | X | X | X |   |   |   |   |   |   | \n" +
                                 " 1 |   |   |   |   |   |   |   |   | D |   | \n" +
                                 " 2 | G |   |   |   |   |   |   |   | D |   | \n" +
                                 " 3 |   |   |   |   |   |   |   |   | D |   | \n" +
                                 " 4 |   | C |   | G |   |   |   |   |   |   | \n" +
                                 " 5 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 6 |   | C |   |   |   |   |   |   |   |   | \n" +
                                 " 7 |   | C |   |   |   |   | G |   |   |   | \n" +
                                 " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                 " 9 | G |   |   |   |   |   |   |   |   |   | \n" +
                                 "-------------------------------------------| \n" +
                                 "\n";

        string tablero = batallaNaval.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_ElJuegoFinaliza_Debe_NombrarElGanadoryMostrarInformeDeEstadistica()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo", [
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarJugador("Gato", [
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

        batallaNaval.Iniciar();
        //Jugador 1
        batallaNaval.Disparar(1, 4);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 1);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(1, 5);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 2);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(1, 6);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 3);
        batallaNaval.FinalizarTurno();
        //Jugador 1  Hundió Carrier
        batallaNaval.Disparar(1, 7);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 4);
        batallaNaval.FinalizarTurno();
        //Jugador 1  
        batallaNaval.Disparar(1, 0);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 5);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(2, 0);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 6);
        batallaNaval.FinalizarTurno();
        //Jugador 1 hundió Destroyer
        batallaNaval.Disparar(3, 0);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 7);
        batallaNaval.FinalizarTurno();
        //Jugador 1  
        batallaNaval.Disparar(8, 1);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 8);
        batallaNaval.FinalizarTurno();
        //Jugador 1
        batallaNaval.Disparar(8, 2);
        batallaNaval.FinalizarTurno();
        //Jugador 2
        batallaNaval.Disparar(1, 9);
        batallaNaval.FinalizarTurno();
        //Jugador 1  Hundió Destroyer
        batallaNaval.Disparar(8, 3);
        batallaNaval.FinalizarTurno();
        //Jugador 2  
        batallaNaval.Disparar(2, 0);
        batallaNaval.FinalizarTurno();
        //Jugador 1 Hundió Gunship
        batallaNaval.Disparar(0, 2);
        batallaNaval.FinalizarTurno();
        //Jugador 2 
        batallaNaval.Disparar(2, 1);
        batallaNaval.FinalizarTurno();
        //Jugador 1 Hundió Gunship
        batallaNaval.Disparar(0, 9);
        batallaNaval.FinalizarTurno();
        //Jugador 2    
        batallaNaval.Disparar(2, 2);
        batallaNaval.FinalizarTurno();
        //Jugador 1 Hundió Gunship
        batallaNaval.Disparar(3, 4);
        batallaNaval.FinalizarTurno();
        //Jugador 2    
        batallaNaval.Disparar(2, 3);
        batallaNaval.FinalizarTurno();
        //Jugador 1 Hundió Gunship
        batallaNaval.Disparar(6, 7);
        batallaNaval.FinalizarTurno();

        string reporteEsperadoJugador1 =
            "Total disparos: 14.\n" +
            "Perdidos: 0.\n" +
            "Acertados: 14.\n" +
            "Barcos derribados: [" +
            "Carrier: (1,4).\n" +
            "Destroyer: (1,0).\n" +
            "Destroyer: (8,1).\n" +
            "Gunship: (0,2).\n" +
            "Gunship: (0,9).\n" +
            "Gunship: (3,4).\n" +
            "Gunship: (6,7).]";

        string tableroEsperadoJugador1 = "\n" +
                                         "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                         "-------------------------------------------| \n" +
                                         " 0 |   | X | X | X |   |   |   |   |   |   | \n" +
                                         " 1 |   |   |   |   |   |   |   |   | X |   | \n" +
                                         " 2 | X |   |   |   |   |   |   |   | X |   | \n" +
                                         " 3 |   |   |   |   |   |   |   |   | X |   | \n" +
                                         " 4 |   | X |   | X |   |   |   |   |   |   | \n" +
                                         " 5 |   | X |   |   |   |   |   |   |   |   | \n" +
                                         " 6 |   | X |   |   |   |   |   |   |   |   | \n" +
                                         " 7 |   | X |   |   |   |   | X |   |   |   | \n" +
                                         " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
                                         " 9 | X |   |   |   |   |   |   |   |   |   | \n" +
                                         "-------------------------------------------| \n" +
                                         "\n";

        string reporteEsperadoJugador2 =
            "Total disparos: 13.\n" +
            "Perdidos: 10.\n" +
            "Acertados: 3.\n";

        string tableroEsperadoJugador2 = "\n" +
                                         "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
                                         "-------------------------------------------| \n" +
                                         " 0 |   |   | o |   |   |   |   |   |   |   | \n" +
                                         " 1 |   | x | x | C | C |   |   |   |   |   | \n" +
                                         " 2 |   | o | x | D | D |   |   |   |   |   | \n" +
                                         " 3 |   | o | o | D | D | D |   |   |   |   | \n" +
                                         " 4 |   | o |   |   | G |   |   |   |   |   | \n" +
                                         " 5 |   | o |   |   |   | G |   |   |   |   | \n" +
                                         " 6 |   | o |   |   |   |   | G |   |   |   | \n" +
                                         " 7 |   | o |   |   |   |   |   | G |   |   | \n" +
                                         " 8 |   | o |   |   |   |   |   |   |   |   | \n" +
                                         " 9 |   | o |   |   |   |   |   |   |   |   | \n" +
                                         "-------------------------------------------| \n" +
                                         "\n";

        string informeEsperado = reporteEsperadoJugador1 +
                                 tableroEsperadoJugador1 +
                                 reporteEsperadoJugador2 +
                                 tableroEsperadoJugador2;

        batallaNaval.Imprimir().Should().Be(informeEsperado);
    }
}
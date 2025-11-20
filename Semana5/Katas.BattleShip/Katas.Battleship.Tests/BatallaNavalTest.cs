using FluentAssertions;

namespace Katas.Battleship.Tests;

public class BatallaNavalTest
{
    [Fact]
    public void SI_AgregoJugador1_Debe_ExistirJugador1()
    {
        var jugador1 = new Jugador("pollo");
        var batallaNaval = new BatallaNaval();

        batallaNaval.AgregarJugador(jugador1);

        batallaNaval.ObtenerJugadorActual().Apodo.Should().Be("pollo");
    }

    [Fact]
    public void Si_AgregoJugador2_Debe_ExistirJugador2()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("pollo")
            .AgregarJugador("gato")
            .Construir();

        batallaNaval.ObtenerJugadorOponente().Apodo.Should().Be("gato");
    }

    [Fact]
    public void Si_AgregoJugador3_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNaval();
        var jugador1 = new Jugador("pollo");
        batallaNaval.AgregarJugador(jugador1);
        var jugador2 = new Jugador("gato");
        batallaNaval.AgregarJugador(jugador2);
        var jugador3 = new Jugador("perro");

        var action = () => batallaNaval.AgregarJugador(jugador3);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Solo se permiten 2 jugadores.");
    }

    [Fact]
    public void Si_inicioUnJuego_Debe_ExistirUnTableroParaCadaJugadorY2Jugadores()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        batallaNaval.ObtenerJugadorActual().Tablero.Should().NotBeNull();
        batallaNaval.ObtenerJugadorOponente().Tablero.Should().NotBeNull();

        batallaNaval.ObtenerJugadorActual().Should().NotBeNull();
        batallaNaval.ObtenerJugadorOponente().Should().NotBeNull();
    }

    [Fact]
    public void SI_InicioSinJugadores_Debe_LanzarExcepcion()
    {
        var batallaNaval =  new BatallaNavalBuilder()
            .Construir();

        var action = () => batallaNaval.Iniciar();
        
        action.Should().Throw<ArgumentException>()
            .WithMessage("No Estan los Jugadores Configurados.");
    }

    [Fact]
    public void Si_AgregoBarcosAlJuego_Debe_ExistirBarcosEnElTablero()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([Barco.Gunship(Posicion.Horizontal(2, 2))])
            .Construir();

        batallaNaval.ObtenerJugadorActual().Tablero[2, 2].Should().Be('G');
    }

    [Fact]
    public void SI_Agrego2BarcoEnLaMismaPosicion_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        
        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Gunship(Posicion.Horizontal(2, 2)));

        Action action = () => batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Gunship(Posicion.Horizontal(2, 2)));

        action.Should().Throw<ArgumentException>()
            .WithMessage("Ya existe barco en la posición enviada.");
    }

    [Fact]
    public void Si_AgregoDestroyer_Debe_ExistirBarcoConTresCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Destroyer(Posicion.Horizontal(1, 1)));

        batallaNaval.ObtenerJugadorActual().Tablero[1, 1].Should().Be('D');
        batallaNaval.ObtenerJugadorActual().Tablero[2, 1].Should().Be('D');
        batallaNaval.ObtenerJugadorActual().Tablero[3, 1].Should().Be('D');
    }

    [Fact]
    public void Si_AgregoDestroyerConOrientacionVertical_Debe_ExistirBarcoConTresCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Destroyer(Posicion.Vertical(1, 1)));

        batallaNaval.ObtenerJugadorActual().Tablero[1, 1].Should().Be('D');
        batallaNaval.ObtenerJugadorActual().Tablero[1, 2].Should().Be('D');
        batallaNaval.ObtenerJugadorActual().Tablero[1, 3].Should().Be('D');
    }

    [Fact]
    public void Si_AgregoCarrier_DebeExistirBarcoConCuatroCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Carrier(Posicion.Horizontal(1, 1)));

        batallaNaval.ObtenerJugadorActual().Tablero[1, 1].Should().Be('C');
        batallaNaval.ObtenerJugadorActual().Tablero[2, 1].Should().Be('C');
        batallaNaval.ObtenerJugadorActual().Tablero[3, 1].Should().Be('C');
        batallaNaval.ObtenerJugadorActual().Tablero[4, 1].Should().Be('C');
    }

    [Fact]
    public void Si_AgregoCarrierConOrientacionVertical_DebeExistirBarcoConCuatroCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Carrier(Posicion.Vertical(1, 1)));

        batallaNaval.ObtenerJugadorActual().Tablero[1, 1].Should().Be('C');
        batallaNaval.ObtenerJugadorActual().Tablero[1, 2].Should().Be('C');
        batallaNaval.ObtenerJugadorActual().Tablero[1, 3].Should().Be('C');
        batallaNaval.ObtenerJugadorActual().Tablero[1, 4].Should().Be('C');
    }

    [Fact]
    public void SI_Tengo1CarrierYAgregoOtro_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Carrier(Posicion.Horizontal(1, 1)));

        Action action = () => batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Carrier(Posicion.Horizontal(5, 5)));
        action.Should().Throw<ArgumentException>()
            .WithMessage("Solo se puede asignar 1 barcos de tipo Carrier.");
    }

    [Fact]
    public void Si_TengoUnCarrierYDosDestroyerYAgregoUnTercero_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Carrier(Posicion.Horizontal(1, 1)));
        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Destroyer(Posicion.Horizontal(2, 2)));
        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Destroyer(Posicion.Horizontal(3, 3)));

        Action action = () => batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Destroyer(Posicion.Horizontal(4, 4)));

        action.Should().Throw<ArgumentException>().WithMessage("Solo se puede asignar 2 barcos de tipo Destroyer.");
    }

    [Fact]
    public void Si_TengoCuatroGunshipsYAgregoUnQuinto_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Gunship(Posicion.Horizontal(1, 1)));
        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Gunship(Posicion.Horizontal(2, 2)));
        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Gunship(Posicion.Horizontal(3, 3)));
        batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Gunship(Posicion.Horizontal(4, 4)));

        Action action = () => batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Gunship(Posicion.Horizontal(5, 5)));

        action.Should().Throw<ArgumentException>().WithMessage("Solo se puede asignar 4 barcos de tipo Gunship.");
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
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .Construir();

        Action action = () => batallaNaval.ObtenerJugadorActual().AgregarBarco(Barco.Gunship(Posicion.Horizontal(x, y)));

        action.Should().Throw<ArgumentException>()
            .WithMessage("El barco se encuentra fuera del tablero.");
    }

    [Fact]
    public void Si_InicioJuego_Debe_CadaJugadorDebeTener7BarcosAsignado()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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

        batallaNaval.ObtenerJugadorActual().TieneTodosLosBarcosAsginados().Should().BeTrue();
        batallaNaval.ObtenerJugadorOponente().TieneTodosLosBarcosAsginados().Should().BeTrue();
    }

    [Fact]
    public void Si_InicioJuegoYNoTengoLos7BarcosAsignadoPorJugador1_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6))
            ])
            .AgregarBarcosJugador2([
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
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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

        string tablero = batallaNaval.Imprimir("Pollo");

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_ImprimeTableroDelJugador1_Debe_MostrarElTableroConBarcos()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
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

        string tablero = batallaNaval.Imprimir("Pollo");

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_ImprimeTableroDelJugador2_DebeMostrarElTableroConBarcos()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador2([
                Barco.Carrier(Posicion.Vertical(1, 4)),
                Barco.Destroyer(Posicion.Horizontal(1, 0)),
                Barco.Destroyer(Posicion.Vertical(8, 1)),
                Barco.Gunship(Posicion.Horizontal(0, 2)),
                Barco.Gunship(Posicion.Horizontal(0, 9)),
                Barco.Gunship(Posicion.Horizontal(3, 4)),
                Barco.Gunship(Posicion.Horizontal(6, 7)),
            ])
            .Construir();

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

        string tablero = batallaNaval.Imprimir("Gato");

        tablero.Should().Be(tableroEsperado);
    }

    [Fact]
    public void Si_DisparaSinIniciarElJuego_DebeLanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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

        batallaNaval.ObtenerJugadorOponente().Tablero[0, 0].Should().Be('o');
        batallaNaval.ObtenerJugadorActual().TableroDisparos[0, 0].Should().Be('o');
    }

    [Fact]
    public void SiDisparoDosVecesEnLaMismaCoordenada_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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

        batallaNaval.ObtenerJugadorOponente().Tablero[1, 4].Should().Be('x');
        batallaNaval.ObtenerJugadorActual().TableroDisparos[1, 4].Should().Be('x');
    }

    [Fact]
    public void Si_Realizo1DisparosYFinalizaTurno_Debe_CambiarJugadorActualYJugadorOponente()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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

        batallaNaval.ObtenerJugadorActual().Apodo.Should().Be("Gato");
        batallaNaval.ObtenerJugadorOponente().Apodo.Should().Be("Pollo");
    }

    [Fact]
    public void Si_Realizo2Disparos_Debe_CambiarJugadorActualYJugadorOponente()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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
        batallaNaval.Disparar(1, 1);
        batallaNaval.FinalizarTurno();

        batallaNaval.ObtenerJugadorActual().Apodo.Should().Be("Pollo");
        batallaNaval.ObtenerJugadorOponente().Apodo.Should().Be("Gato");
    }

    [Fact]
    public void Si_PresentaInformeDeBatalla_DebeRetornarInforme()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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

        batallaNaval.Imprimir("Pollo", esReporte: true).Should().Be(informeEsperado);
    }

    [Fact]
    public void Si_PresentaInformeDeBatalla_DebeRetornarInformeConDisparosFallidos()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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

        batallaNaval.Imprimir("Pollo", esReporte: true).Should().Be(informeEsperado);
    }

    [Fact]
    public void Si_PresentaInformeDeBatalla_Debe_RetornarInformeConBarcosDerribados()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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
        batallaNaval.FinalizarTurno();

        var informeEsperado =
            "Total disparos: 4.\n" +
            "Perdidos: 0.\n" +
            "Acertados: 4.\n" +
            "Barcos derribados: [" +
            "Destroyer: (1,0).\n" +
            "Gunship: (0,2).\n" +
            "]";

        batallaNaval.Imprimir("Gato", esReporte:true).Should().Be(informeEsperado);
    }

    [Fact]
    public void Si_RealizoUnDisparoFallido_Debe_RetornarMensajeTiroFallido()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .AgregarBarcosJugador1([
                Barco.Carrier(Posicion.Horizontal(1, 1)),
                Barco.Destroyer(Posicion.Horizontal(2, 2)),
                Barco.Destroyer(Posicion.Horizontal(3, 3)),
                Barco.Gunship(Posicion.Horizontal(4, 4)),
                Barco.Gunship(Posicion.Horizontal(5, 5)),
                Barco.Gunship(Posicion.Horizontal(6, 6)),
                Barco.Gunship(Posicion.Horizontal(7, 7)),
            ])
            .AgregarBarcosJugador2([
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

        string tablero = batallaNaval.Imprimir("Gato");

        tablero.Should().Be(tableroEsperado);
    }
}
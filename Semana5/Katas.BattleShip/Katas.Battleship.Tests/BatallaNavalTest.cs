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

        batallaNaval.Jugador1.Apodo.Should().Be("pollo");
    }

    [Fact]
    public void Si_AgregoJugador2_Debe_ExistirJugador2()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("pollo")
            .AgregarJugador("gato")
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador2.Apodo.Should().Be("gato");
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
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador1.Tablero.Should().NotBeNull();
        batallaNaval.Jugador2.Tablero.Should().NotBeNull();

        batallaNaval.Jugador1.Should().NotBeNull();
        batallaNaval.Jugador2.Should().NotBeNull();
    }

    [Fact]
    public void SI_InicioSinJugadores_Debe_LanzarExcepcion()
    {
        var action = () => new BatallaNavalBuilder()
            .ValidarJugadores()
            .Construir();

        action.Should().Throw<ArgumentException>()
            .WithMessage("No Estan los Jugadores Configurados.");
    }

    [Fact]
    public void Si_AgregoBarcosAlJuego_Debe_ExistirBarcosEnElTablero()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 2, y: 2);

        batallaNaval.Jugador1.Tablero[2, 2].Should().Be('G');
    }

    [Fact]
    public void SI_Agrego2BarcoEnLaMismaPosicion_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 2, y: 2);

        Action action = () => batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 2, y: 2);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Ya existe barco en la posición enviada.");
    }

    [Fact]
    public void Si_AgregoDestroyer_Debe_ExistirBarcoConTresCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador1.AgregarBarco(Barco.Destroyer(), x: 1, y: 1);

        batallaNaval.Jugador1.Tablero[1, 1].Should().Be('D');
        batallaNaval.Jugador1.Tablero[2, 1].Should().Be('D');
        batallaNaval.Jugador1.Tablero[3, 1].Should().Be('D');
    }

    [Fact]
    public void Si_AgregoDestroyerConOrientacionVertical_Debe_ExistirBarcoConTresCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador1.AgregarBarco(Barco.Destroyer(), x: 1, y: 1, esVertical: true);

        batallaNaval.Jugador1.Tablero[1, 1].Should().Be('D');
        batallaNaval.Jugador1.Tablero[1, 2].Should().Be('D');
        batallaNaval.Jugador1.Tablero[1, 3].Should().Be('D');
    }

    [Fact]
    public void Si_AgregoCarrier_DebeExistirBarcoConCuatroCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador1.AgregarBarco(Barco.Carrier(), x: 1, y: 1);

        batallaNaval.Jugador1.Tablero[1, 1].Should().Be('C');
        batallaNaval.Jugador1.Tablero[2, 1].Should().Be('C');
        batallaNaval.Jugador1.Tablero[3, 1].Should().Be('C');
        batallaNaval.Jugador1.Tablero[4, 1].Should().Be('C');
    }

    [Fact]
    public void Si_AgregoCarrierConOrientacionVertical_DebeExistirBarcoConCuatroCasillas()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador1.AgregarBarco(Barco.Carrier(), x: 1, y: 1, esVertical: true);

        batallaNaval.Jugador1.Tablero[1, 1].Should().Be('C');
        batallaNaval.Jugador1.Tablero[1, 2].Should().Be('C');
        batallaNaval.Jugador1.Tablero[1, 3].Should().Be('C');
        batallaNaval.Jugador1.Tablero[1, 4].Should().Be('C');
    }

    [Fact]
    public void SI_Tengo1CarrierYAgregoOtro_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador1.AgregarBarco(Barco.Carrier(), x: 1, y: 1);

        Action action = () => batallaNaval.Jugador1.AgregarBarco(Barco.Carrier(), x: 5, y: 5);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Solo se puede asignar 1 barcos de tipo Carrier.");
    }

    [Fact]
    public void Si_TengoUnCarrierYDosDestroyerYAgregoUnTercero_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador1.AgregarBarco(Barco.Carrier(), x: 1, y: 1);
        batallaNaval.Jugador1.AgregarBarco(Barco.Destroyer(), x: 2, y: 2);
        batallaNaval.Jugador1.AgregarBarco(Barco.Destroyer(), x: 3, y: 3);

        Action action = () => batallaNaval.Jugador1.AgregarBarco(Barco.Destroyer(), x: 4, y: 4);

        action.Should().Throw<ArgumentException>().WithMessage("Solo se puede asignar 2 barcos de tipo Destroyer.");
    }

    [Fact]
    public void Si_TengoCuatroGunshipsYAgregoUnQuinto_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .Construir();

        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 1, y: 1);
        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 2, y: 2);
        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 3, y: 3);
        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 4, y: 4);

        Action action = () => batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 5, y: 5);

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
            .ValidarJugadores()
            .Construir();

        Action action = () => batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x, y);

        action.Should().Throw<ArgumentException>()
            .WithMessage("El barco se encuentra fuera del tablero.");
    }

    [Fact]
    public void Si_InicioJuego_Debe_CadaJugadorDebeTener7BarcosAsignado()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .AgregarBarcosJugador1([
                (Barco.Carrier(), Posicion.Horizontal(1, 1)),
                (Barco.Destroyer(), Posicion.Horizontal(2, 2)),
                (Barco.Destroyer(), Posicion.Horizontal(3, 3)),
                (Barco.Gunship(), Posicion.Horizontal(4, 4)),
                (Barco.Gunship(), Posicion.Horizontal(5, 5)),
                (Barco.Gunship(), Posicion.Horizontal(6, 6)),
                (Barco.Gunship(), Posicion.Horizontal(7, 7))
            ])
            .AgregarBarcosJugador2([
                (Barco.Carrier(), Posicion.Horizontal(1, 1)),
                (Barco.Destroyer(), Posicion.Horizontal(2, 2)),
                (Barco.Destroyer(), Posicion.Horizontal(3, 3)),
                (Barco.Gunship(), Posicion.Horizontal(4, 4)),
                (Barco.Gunship(), Posicion.Horizontal(5, 5)),
                (Barco.Gunship(), Posicion.Horizontal(6, 6)),
                (Barco.Gunship(), Posicion.Horizontal(7, 7))
            ])
            .Construir();

        batallaNaval.Iniciar();

        batallaNaval.Jugador1.NumeroDeBarcosAsginados.Should().Be(7);
        batallaNaval.Jugador2.NumeroDeBarcosAsginados.Should().Be(7);
    }

    [Fact]
    public void Si_InicioJuegoYNoTengoLos7BarcosAsignadoPorJugador1_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNavalBuilder()
            .AgregarJugador("Pollo")
            .AgregarJugador("Gato")
            .ValidarJugadores()
            .AgregarBarcosJugador1([
                (Barco.Carrier(), Posicion.Horizontal(1, 1)),
                (Barco.Destroyer(), Posicion.Horizontal(2, 2)),
                (Barco.Destroyer(), Posicion.Horizontal(3, 3)),
                (Barco.Gunship(), Posicion.Horizontal(4, 4)),
                (Barco.Gunship(), Posicion.Horizontal(5, 5)),
                (Barco.Gunship(), Posicion.Horizontal(6, 6))
            ])
            .AgregarBarcosJugador2([
                (Barco.Carrier(), Posicion.Horizontal(1, 1)),
                (Barco.Destroyer(), Posicion.Horizontal(2, 2)),
                (Barco.Destroyer(), Posicion.Horizontal(3, 3)),
                (Barco.Gunship(), Posicion.Horizontal(4, 4)),
                (Barco.Gunship(), Posicion.Horizontal(5, 5)),
                (Barco.Gunship(), Posicion.Horizontal(6, 6)),
                (Barco.Gunship(), Posicion.Horizontal(7, 7))
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
            .ValidarJugadores()
            .AgregarBarcosJugador1([
                (Barco.Carrier(), Posicion.Horizontal(1, 1)),
                (Barco.Destroyer(), Posicion.Horizontal(2, 2)),
                (Barco.Destroyer(), Posicion.Horizontal(3, 3)),
                (Barco.Gunship(), Posicion.Horizontal(4, 4)),
                (Barco.Gunship(), Posicion.Horizontal(5, 5)),
                (Barco.Gunship(), Posicion.Horizontal(6, 6)),
                (Barco.Gunship(), Posicion.Horizontal(7, 7))
            ])
            .AgregarBarcosJugador2([
                (Barco.Carrier(), Posicion.Horizontal(1, 1)),
                (Barco.Destroyer(), Posicion.Horizontal(2, 2)),
                (Barco.Destroyer(), Posicion.Horizontal(3, 3)),
                (Barco.Gunship(), Posicion.Horizontal(4, 4)),
                (Barco.Gunship(), Posicion.Horizontal(5, 5)),
                (Barco.Gunship(), Posicion.Horizontal(6, 6))
            ])
            .Construir();
        
        Action action = () => batallaNaval.Iniciar();
        
        action.Should().Throw<ArgumentException>()
            .WithMessage("Falta barcos por asignar.");
    }
}

public class BatallaNavalBuilder
{
    private readonly BatallaNaval _batallaNaval = new();

    public BatallaNavalBuilder()
    {
        _batallaNaval = new BatallaNaval();
    }

    public static BatallaNavalBuilder Crear() => new BatallaNavalBuilder();

    public BatallaNavalBuilder AgregarJugador(string apodo)
    {
        var jugador1 = new Jugador(apodo);
        _batallaNaval.AgregarJugador(jugador1);
        return this;
    }

    public BatallaNavalBuilder AgregarBarcosJugador1((Barco barco, Posicion posicion)[] barcoPosiciones)
    {
        foreach (var barcoPosicion in barcoPosiciones)
            _batallaNaval.Jugador1.AgregarBarco(barcoPosicion.barco, barcoPosicion.posicion);

        return this;
    }

    public BatallaNavalBuilder AgregarBarcosJugador2((Barco barco, Posicion posicion)[] barcoPosiciones)
    {
        foreach (var barcoPosicion in barcoPosiciones)
            _batallaNaval.Jugador2.AgregarBarco(barcoPosicion.barco, barcoPosicion.posicion);

        return this;
    }

    public BatallaNavalBuilder ValidarJugadores()
    {
        _batallaNaval.ValidarExistenJugadores();
        return this;
    }

    public BatallaNaval Construir() => _batallaNaval;
}

public record Posicion
{
    public static Posicion Horizontal(int ejeX, int ejeY) => new Posicion(ejeX, ejeY, false);
    public static Posicion Vertical(int ejeX, int ejeY) => new Posicion(ejeX, ejeY, true);

    private Posicion(int EjeX, int EjeY, bool EsVertical)
    {
        this.EjeX = EjeX;
        this.EjeY = EjeY;
        this.EsVertical = EsVertical;
    }

    public int EjeX { get; }
    public int EjeY { get; }
    public bool EsVertical { get; }
}
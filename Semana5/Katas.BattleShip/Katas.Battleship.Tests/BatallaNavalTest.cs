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
        var batallaNaval = new BatallaNaval();

        var jugador1 = new Jugador("pollo");
        var jugador2 = new Jugador("gato");
        batallaNaval.AgregarJugador(jugador1);

        batallaNaval.AgregarJugador(jugador2);

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
        var batallaNaval = CrearJuegoYAgregarJugadores();
        
        batallaNaval.Jugador1.Tablero.Should().NotBeNull();
        batallaNaval.Jugador2.Tablero.Should().NotBeNull();

        batallaNaval.Jugador1.Should().NotBeNull();
        batallaNaval.Jugador2.Should().NotBeNull();
    }

    [Fact]
    public void SI_InicioSinJugadores_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNaval();
        var action = () => batallaNaval.Iniciar();

        action.Should().Throw<ArgumentException>()
            .WithMessage("No Estan los Jugadores Configurados.");
    }

    [Fact]
    public void Si_AgregoBarcosAlJuego_Debe_ExistirBarcosEnElTablero()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();
        
        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 2 , y: 2);

        batallaNaval.Jugador1.Tablero[2, 2].Should().Be('G');
    }

    [Fact]
    public void SI_Agrego2BarcoEnLaMismaPosicion_Debe_LanzarExcepcion ()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();
        
        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 2 , y: 2);

       Action action=()=> batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 2 , y: 2);
        
       action.Should().Throw<ArgumentException>()
           .WithMessage("Ya existe barco en la posición enviada.");
    }

    [Fact]
    public void Si_AgregoDestroyer_Debe_ExistirBarcoConTresCasillas()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();

        batallaNaval.Jugador1.AgregarBarco(Barco.Destroyer(), x: 1, y: 1);
        
        batallaNaval.Jugador1.Tablero[1, 1].Should().Be('D');
        batallaNaval.Jugador1.Tablero[2, 1].Should().Be('D');
        batallaNaval.Jugador1.Tablero[3, 1].Should().Be('D');
    }

    [Fact]
    public void Si_AgregoDestroyerConOrientacionVertical_Debe_ExistirBarcoConTresCasillas()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();

        batallaNaval.Jugador1.AgregarBarco(Barco.Destroyer(), x: 1, y: 1, esVertical: true);
        
        batallaNaval.Jugador1.Tablero[1, 1].Should().Be('D');
        batallaNaval.Jugador1.Tablero[1, 2].Should().Be('D');
        batallaNaval.Jugador1.Tablero[1, 3].Should().Be('D');
    }

    [Fact]
    public void Si_AgregoCarrier_DebeExistirBarcoConCuatroCasillas()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();

        batallaNaval.Jugador1.AgregarBarco(Barco.Carrier(), x: 1, y: 1);

        batallaNaval.Jugador1.Tablero[1, 1].Should().Be('C');
        batallaNaval.Jugador1.Tablero[2, 1].Should().Be('C');
        batallaNaval.Jugador1.Tablero[3, 1].Should().Be('C');
        batallaNaval.Jugador1.Tablero[4, 1].Should().Be('C');
    }

    [Fact]
    public void Si_AgregoCarrierConOrientacionVertical_DebeExistirBarcoConCuatroCasillas()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();

        batallaNaval.Jugador1.AgregarBarco(Barco.Carrier(), x: 1, y: 1, esVertical: true);

        batallaNaval.Jugador1.Tablero[1, 1].Should().Be('C');
        batallaNaval.Jugador1.Tablero[1, 2].Should().Be('C');
        batallaNaval.Jugador1.Tablero[1, 3].Should().Be('C');
        batallaNaval.Jugador1.Tablero[1, 4].Should().Be('C');
    }

    [Fact]
    public void SI_Tengo1CarrierYAgregoOtro_Debe_LanzarExcepcion()
    { 
        var batallaNaval = CrearJuegoYAgregarJugadores();
        batallaNaval.Jugador1.AgregarBarco(Barco.Carrier(), x: 1, y: 1  );
        
        Action action = ()=> batallaNaval.Jugador1.AgregarBarco(Barco.Carrier(), x: 5, y: 5  );
        action.Should().Throw<ArgumentException>()
            .WithMessage("Solo se puede asignar 1 barcos de tipo Carrier.");
    }

    [Fact]
    public void Si_TengoUnCarrierYDosDestroyerYAgregoUnTercero_Debe_LanzarExcepcion()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();
        batallaNaval.Jugador1.AgregarBarco(Barco.Carrier(), x: 1, y: 1);
        batallaNaval.Jugador1.AgregarBarco(Barco.Destroyer(), x: 2, y: 2);
        batallaNaval.Jugador1.AgregarBarco(Barco.Destroyer(), x: 3, y: 3);
        
        Action action = () => batallaNaval.Jugador1.AgregarBarco(Barco.Destroyer(), x: 4, y: 4);

        action.Should().Throw<ArgumentException>().WithMessage("Solo se puede asignar 2 barcos de tipo Destroyer.");
    }

    [Fact]
    public void Si_TengoCuatroGunshipsYAgregoUnQuinto_Debe_LanzarExcepcion()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();
        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 1, y: 1);
        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 2, y: 2);
        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 3, y: 3);
        batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 4, y: 4);
        
        Action action = () => batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 5, y: 5);
        
        action.Should().Throw<ArgumentException>().WithMessage("Solo se puede asignar 4 barcos de tipo Gunship.");
    }

    [Fact]
    public void Si_AgregoBarcosQueEsteFueraDelasCoordenadas_Debe_LanzarExcepcion()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();
        Action action = () =>  batallaNaval.Jugador1.AgregarBarco(Barco.Gunship(), x: 11, y: 11);
        
        action.Should().Throw<ArgumentException>().WithMessage("El Barco se encuetra Fuera del tablero ");
    }
    
    private static BatallaNaval CrearJuegoYAgregarJugadores()
    {
        var batallaNaval = new BatallaNaval();
        var jugador1 = new Jugador("pollo");
        batallaNaval.AgregarJugador(jugador1);
        var jugador2 = new Jugador("gato");
        batallaNaval.AgregarJugador(jugador2);
        
        batallaNaval.Iniciar();
        
        return batallaNaval;
    }
}
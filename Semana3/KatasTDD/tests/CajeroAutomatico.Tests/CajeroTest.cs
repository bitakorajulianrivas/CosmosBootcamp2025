using FluentAssertions;

namespace CajeroAutomatico.Tests;

public class CajeroTest
{
    [Fact]
    public void Retirar_SiLaCantidadSonQuinientos_DebeRetornarUnBilleteDeQuinientos()
    {
        int montoSolicitado = 500;
        Cajero cajero = new Cajero();
        
        string montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().Be("1 billete de 500");
    }
}

public class Cajero
{
    public string Retirar(int montoSolitado)
    {
        if(montoSolitado == 500)
            return "1 billete de 500";
        
        throw new NotImplementedException();
    }
}
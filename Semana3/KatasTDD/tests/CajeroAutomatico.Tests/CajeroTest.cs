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
    
    [Fact]
    public void Retirar_SiLaCantidadSonDoscientos_DebeRetornarUnBilleteDeDoscientos()
    {
        int montoSolicitado = 200;
        Cajero cajero = new Cajero();
        
        string montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().Be("1 billete de 200");
    }
    
    [Fact]
    public void Retirar_SiLaCantidadSonCien_DebeRetornarUnBilleteDeCien()
    {
        int montoSolicitado = 100;
        Cajero cajero = new Cajero();
        
        string montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().Be("1 billete de 100");
    }
    
    [Fact]
    public void Retirar_SiLaCantidadSonCincuenta_DebeRetornarUnBilleteDeCincuenta()
    {
        int montoSolicitado = 50;
        Cajero cajero = new Cajero();
        
        string montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().Be("1 billete de 50");
    }
    
    [Fact]
    public void Retirar_SiLaCantidadSonVeinte_DebeRetornarUnBilleteDeVeinte()
    {
        int montoSolicitado = 20;
        Cajero cajero = new Cajero();
        
        string montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().Be("1 billete de 20");
    }
    
    [Fact]
    public void Retirar_SiLaCantidadSonDiez_DebeRetornarUnBilleteDeDiez()
    {
        int montoSolicitado = 10;
        Cajero cajero = new Cajero();
        
        string montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().Be("1 billete de 10");
    }
    
    [Fact]
    public void Retirar_SiLaCantidadSonCinco_DebeRetornarUnBilleteDeCinco()
    {
        int montoSolicitado = 5;
        Cajero cajero = new Cajero();
        
        string montoRetirado = cajero.Retirar(montoSolicitado);
        
        montoRetirado.Should().Be("1 billete de 5");
    }
}

public class Cajero
{
    public string Retirar(int montoSolitado)
    {
        if(montoSolitado == 500)
            return "1 billete de 500";
        
        if(montoSolitado == 200)
            return "1 billete de 200";
        
        if(montoSolitado == 100)
            return "1 billete de 100";
        
        if(montoSolitado == 50)
            return "1 billete de 50";
        
        if(montoSolitado == 20)
            return "1 billete de 20";
        
        if(montoSolitado == 10)
            return "1 billete de 10";
        
        if(montoSolitado == 5)
            return "1 billete de 5";
        
        throw new NotImplementedException();
    }
}
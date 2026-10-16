Console.WriteLine("Ingrese el código del tanque:");
string CodigoTanque= Console.ReadLine()!;
Console.WriteLine("Ingrese la temperatura actual del tanque:");
decimal TemperaturaActual = decimal.Parse(Console.ReadLine()!);
TanqueFermentacion MiTanque = new TanqueFermentacion(CodigoTanque, TemperaturaActual);
Console.WriteLine($"El tanque {MiTanque.CodigoTanque} tiene una tempoeratuta de {MiTanque.TemperaturaActual} grados. {MiTanque.ObtenerEstadoTemperatura()}");
public class TanqueFermentacion
{
    public string CodigoTanque { get; set;}
    public decimal TemperaturaActual { get; set;}
    public TanqueFermentacion(string CodigoTanque, decimal TemperaturaActual)
    {
        this.CodigoTanque = CodigoTanque;
        this.TemperaturaActual = TemperaturaActual;
    }
    public string ObtenerEstadoTemperatura()
    {
        if (TemperaturaActual < 10m)
            return "ALERTA: Temperatura demasiado baja";
        else if (TemperaturaActual >= 10m && TemperaturaActual<=18m)
            return "ÓPTIMO: Temperatura en rango correcto";
        else 
            return "ALERTA: Temperatura demasiado alta";
    }
}
TanqueFermentacion MiTanque = new TanqueFermentacion();
Console.WriteLine("Ingrese el código del tanque:");
string codigo= Console.ReadLine()!;
Console.WriteLine("Ingrese la temperatura actual del tanque:");
decimal temperatura = decimal.Parse(Console.ReadLine()!);
MiTanque.CodigoTanque = codigo;
MiTanque.TemperaturaActual = temperatura;
Console.WriteLine($"El tanque {MiTanque.CodigoTanque} tiene una tempoeratuta de {MiTanque.TemperaturaActual} grados. {MiTanque.ObtenerEstadoTemperatura()}");
public class TanqueFermentacion
{
    public string CodigoTanque { get; set;}
    public decimal TemperaturaActual { get; set;}
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
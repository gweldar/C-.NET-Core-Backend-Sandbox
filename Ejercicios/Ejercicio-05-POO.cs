List<TanqueFermentacion> listaTanques = new List<TanqueFermentacion>();
for (int i =1; i<=3; i++)
{
    Console.WriteLine($"Ingrese el código del tanque {i}:");
    string CodigoTanque= Console.ReadLine()!;
    Console.WriteLine($"Ingrese la temperatura actual del tanque {i}:");
    decimal TemperaturaActual = decimal.Parse(Console.ReadLine()!);
    TanqueFermentacion MiTanque = new TanqueFermentacion(CodigoTanque, TemperaturaActual);
    listaTanques.Add(MiTanque);
}
Console.WriteLine("\n=== INFORME FINAL DE TANQUES ===");
foreach (TanqueFermentacion t in listaTanques)
{
    Console.WriteLine($"Tanque {t.CodigoTanque} tiene una temperatura de {t.TemperaturaActual} ºC. Estado:{t.ObtenerEstadoTemperatura()}");
}
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
turnoembotellado miturno = new turnoembotellado();
Console.WriteLine("dime el nombre del operario: ");
string nombre =Console.ReadLine()!;
Console.WriteLine("dime el total de botellas producidas en el turno: ");
int botellas =int.Parse(Console.ReadLine()!);
Console.WriteLine("dime el numero de botellas defectuosas: ");
int botellasdef = int.Parse(Console.ReadLine()!);
Console.WriteLine("dime el total de horas trabajadas en el turno: ");
decimal horas = decimal.Parse(Console.ReadLine()!);
miturno.operario = nombre;
miturno.botellas = botellas;
miturno.botellasdef = botellasdef;
miturno.horas = horas;

Console.WriteLine(miturno.obtenerestado());
Console.WriteLine($"Operario: {miturno.operario} produjo con exito un total de {miturno.obtenerbotellasbuenas()} botellas, con un rendimiento de {miturno.obtenerrendimiento()} botellas por hora.");
public class turnoembotellado
{
    public string operario { get; set; }
    public int botellas { get; set; }
    public int botellasdef { get; set; }
    public decimal horas { get; set; }

    public int obtenerbotellasbuenas()
    {
        return botellas - botellasdef;
    }
    public decimal obtenerrendimiento()
    {
        return (decimal)obtenerbotellasbuenas() /horas;
    }
    public string obtenerestado()
    {
        decimal rendimiento = obtenerrendimiento();
        if (rendimiento>= 1000m)
            return "Rendimiento SOBRESALIENTE";
        else if (rendimiento >=500m)
            return "Rendimiento NORMAL";
        else
            return "Rendimiento BAJO - Revisa línea";
    }
}
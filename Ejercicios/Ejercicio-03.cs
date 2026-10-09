Console.WriteLine("dime el nombre del operario: ");
string operario =Console.ReadLine()!;
Console.WriteLine("dime el total de botellas producidas en el turno: ");
int botellas =int.Parse(Console.ReadLine()!);
Console.WriteLine("dime el numero de botellas defectuosas: ");
int botellasdef = int.Parse(Console.ReadLine()!);
Console.WriteLine("dime el total de horas trabajadas en el turno: ");
decimal horas = decimal.Parse(Console.ReadLine()!);
int botellasbuenas = botellas - botellasdef;
decimal rendimiento = (decimal)botellasbuenas / horas;
if (rendimiento>= 1000)
{
    Console.WriteLine("Rendimiento SOBRESALIENTE");
}
else if (rendimiento >=500)
{
    Console.WriteLine("Rendimiento NORMAL");
}
else
{
    Console.WriteLine("Rendimiento BAJO - Revisa línea");
}
Console.WriteLine($"Operario: {operario} produjo {botellasbuenas} botellas, con un rendimiento de {rendimiento} botellas por hora.");
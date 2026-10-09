Console.Write("dime los hectolitros del lote: ");
decimal hectolitros = decimal.Parse(Console.ReadLine()!);
Console.Write("dime las botellas defectuosas: ");
int botellasdefectuosas = int.Parse(Console.ReadLine()!);
decimal totalbotellas =hectolitros * 300;
decimal porcentaje_mermas =(decimal)botellasdefectuosas / totalbotellas*100;
if (porcentaje_mermas > 2m)
{
    Console.WriteLine("Lote bajo REVISIÓN");
}
else
{
    Console.WriteLine("Lote APROBADO");
}
Console.WriteLine($"Total botellas: {totalbotellas} | Mermas: {porcentaje_mermas}");
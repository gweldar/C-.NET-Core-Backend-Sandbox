Console.Write("dime los litros actuales del tanque: ");
decimal litrosact = decimal.Parse(Console.ReadLine()!);
Console.Write("dime los litros maximos del tanque: ");
decimal litrosmax = decimal.Parse(Console.ReadLine()!);
decimal litrosrest =litrosmax - litrosact;
if (litrosmax > litrosact)
{
    Console.WriteLine($"CAPACIDAD DISPONIBLE - Faltan {litrosrest} litros para llenar");
}
else if (litrosmax == litrosact)
{
    Console.WriteLine("TANQUE LLENO - Detener llenado");
}
else
{
    Console.WriteLine("ERROR - La cantidad actual de litros supera la capacidad máxima del tanque");
}
Console.Write("Dime un el numero del lote: ");
string lote=Console.ReadLine()!;
Console.Write("dime el numero de litros producidos: ");
decimal litros =decimal.Parse(Console.ReadLine()!);
decimal botellas = litros / 0.33m;
Console.WriteLine($"El lote es {lote} el total de litros es {litros} y el total de botellas posibles es {botellas}");
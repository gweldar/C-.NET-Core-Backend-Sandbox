Console.Write("Dime un el numero del lote: ");
string lote=Console.ReadLine()!;
Console.Write("dime el numero de botellas: ");
int numbotellas =int.Parse(Console.ReadLine()!);
Console.Write("dime el numero de botellas defectuosas: ");
int numbotellasd =int.Parse(Console.ReadLine()!);
int Diff = numbotellas - numbotellasd;
Console.WriteLine($"El lote es {lote} el total de botellas es {numbotellas} y el total no defectuosas es {Diff}");
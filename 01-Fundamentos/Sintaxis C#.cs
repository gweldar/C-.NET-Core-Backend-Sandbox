Console.WriteLine("como te llamas?: ");
string nombre = Console.ReadLine();
Console.WriteLine("cuantos años tienes?: ");
int edad = int.Parse(Console.ReadLine()!);
if (edad >= 18)
{
    string estado = "Bienvenido al equipo Backend " + nombre;
    Console.WriteLine(estado);
}
else
{
    string estado = "Acceso denegado a la planta " + nombre;
    Console.WriteLine(estado);
}
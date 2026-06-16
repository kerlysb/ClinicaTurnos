namespace ClinicaTurnos
{
    public static class Consola
    {
        public static void Titulo(string texto)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  === {texto.ToUpper()} ===");
            Console.ResetColor();
        }

        public static void Linea() =>
            Console.WriteLine("  " + new string('-', 68));

        public static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  [!] {msg}");
            Console.ResetColor();
        }

        public static void Ok(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  [OK] {msg}");
            Console.ResetColor();
        }

        public static void Pausar()
        {
            Console.Write("\n  Presione ENTER para continuar...");
            Console.ReadLine();
        }

        public static void LimpiarPantalla() => Console.Clear();
        
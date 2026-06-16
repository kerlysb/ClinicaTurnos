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
        public static int LeerEntero(string mensaje, int min = int.MinValue, int max = int.MaxValue)
        {
            int valor;
            while (true)
            {
                Console.Write($"  {mensaje}: ");
                string? entrada = Console.ReadLine();
                if (int.TryParse(entrada, out valor) && valor >= min && valor <= max)
                    return valor;
                Error($"Ingrese un numero entero entre {min} y {max}.");
            }
        }

        public static float LeerFlotante(string mensaje, float min = 0f, float max = 999f)
        {
            float valor;
            while (true)
            {
                Console.Write($"  {mensaje}: ");
                string? entrada = Console.ReadLine();
                if (float.TryParse(entrada, out valor) && valor >= min && valor <= max)
                    return valor;
                Error($"Ingrese un decimal entre {min} y {max}.");
            }
        }

        public static char LeerChar(string mensaje, char[] validos)
        {
            while (true)
            {
                Console.Write($"  {mensaje}: ");
                string? entrada = Console.ReadLine()?.ToUpper();
                if (entrada?.Length == 1 && validos.Contains(entrada[0]))
                    return entrada[0];
                Error($"Opciones validas: {string.Join(", ", validos)}");
            }
        }

        public static string LeerTexto(string mensaje)
        {
            while (true)
            {
                Console.Write($"  {mensaje}: ");
                string? entrada = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(entrada))
                    return entrada;
                Error("El campo no puede estar vacio.");
            }
        }
    }
}


namespace ClinicaTurnos
{
    public class Menu
    {
        private readonly GestorPacientes _gestorPacientes = new();
        private readonly GestorDoctores  _gestorDoctores  = new();
        private readonly GestorTurnos    _gestorTurnos;

        public Menu()
        {
            _gestorTurnos = new GestorTurnos(_gestorPacientes, _gestorDoctores);
            CargarDatos();
        }
        public void Ejecutar()
        {
            while (true)
            {
                Consola.LimpiarPantalla();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  ================================================");
                Console.WriteLine("       CLINICA SALUD TOTAL - SISTEMA DE TURNOS    ");
                Console.WriteLine("  ================================================");
                Console.ResetColor();
                Consola.Linea();
                Console.WriteLine("  1. Gestion de Pacientes");
                Console.WriteLine("  2. Gestion de Doctores");
                Console.WriteLine("  3. Gestion de Turnos");
                Console.WriteLine("  4. Reportes y Estadisticas");
                Console.WriteLine("  0. Salir");
                Consola.Linea();
                int op = Consola.LeerEntero("Seleccione", 0, 4);
                switch (op)
                {
                    case 1: MenuPacientes(); break;
                    case 2: MenuDoctores();  break;
                    case 3: MenuTurnos();    break;
                    case 4: MenuReportes();  break;
                    case 0: Console.WriteLine("\n  Hasta luego.\n"); return;
                }
            }
        }
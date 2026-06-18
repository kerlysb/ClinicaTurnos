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
         private void MenuPacientes()
        {
            while (true)
            {
                Consola.LimpiarPantalla();
                Consola.Titulo("GESTION DE PACIENTES");
                Console.WriteLine("  1. Registrar nuevo paciente");
                Console.WriteLine("  2. Listar pacientes");
                Console.WriteLine("  3. Buscar por cedula");
                Console.WriteLine("  4. Eliminar paciente");
                Console.WriteLine("  0. Volver");
                Consola.Linea();
                int op = Consola.LeerEntero("Opcion", 0, 4);
                switch (op)
                {
                    case 1:
                        if (_gestorPacientes.Registrar(CapturarPaciente()))
                            Consola.Ok("Paciente registrado exitosamente.");
                        Consola.Pausar(); break;
                    case 2:
                        _gestorPacientes.Listar();
                        Consola.Pausar(); break;
                    case 3:
                        string ced = Consola.LeerTexto("Cedula a buscar");
                        var p = _gestorPacientes.BuscarPorCedula(ced);
                        if (p != null) MostrarDetallePaciente(p.Value);
                        else Consola.Error("Paciente no encontrado.");
                        Consola.Pausar(); break;
                    case 4:
                        int id = Consola.LeerEntero("ID a eliminar", 1);
                        if (_gestorPacientes.Eliminar(id)) Consola.Ok("Eliminado.");
                        else Consola.Error("No encontrado.");
                        Consola.Pausar(); break;
                    case 0: return;
                }
            }
        }
        private void MenuDoctores()
        {
            while (true)
            {
                Consola.LimpiarPantalla();
                Consola.Titulo("GESTION DE DOCTORES");
                Console.WriteLine("  1. Registrar nuevo doctor");
                Console.WriteLine("  2. Listar doctores");
                Console.WriteLine("  3. Ver matriz de ocupacion");
                Console.WriteLine("  0. Volver");
                Consola.Linea();
                int op = Consola.LeerEntero("Opcion", 0, 3);
                switch (op)
                {
                    case 1:
                        if (_gestorDoctores.Registrar(CapturarDoctor()))
                            Consola.Ok("Doctor registrado exitosamente.");
                        Consola.Pausar(); break;
                    case 2:
                        _gestorDoctores.Listar();
                        Consola.Pausar(); break;
                    case 3:
                        _gestorDoctores.MostrarMatrizOcupacion();
                        Consola.Pausar(); break;
                    case 0: return;
                }
            }
        }
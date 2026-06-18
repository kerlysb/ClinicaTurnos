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
        private void MenuTurnos()
        {
            while (true)
            {
                Consola.LimpiarPantalla();
                Consola.Titulo("GESTION DE TURNOS");
                Console.WriteLine("  1. Agendar turno");
                Console.WriteLine("  2. Ver todos los turnos");
                Console.WriteLine("  3. Ver turnos pendientes");
                Console.WriteLine("  4. Confirmar turno");
                Console.WriteLine("  5. Marcar como atendido");
                Console.WriteLine("  6. Cancelar turno");
                Console.WriteLine("  7. Turnos por paciente");
                Console.WriteLine("  8. Turnos por doctor");
                Console.WriteLine("  0. Volver");
                Consola.Linea();
                int op = Consola.LeerEntero("Opcion", 0, 8);
                switch (op)
                {
                    case 1:
                        if (_gestorTurnos.Agendar(CapturarTurno()))
                            Consola.Ok("Turno agendado exitosamente.");
                        Consola.Pausar(); break;
                    case 2: _gestorTurnos.ListarTodos();     Consola.Pausar(); break;
                    case 3: _gestorTurnos.ListarPendientes(); Consola.Pausar(); break;
                    case 4:
                        int ic = Consola.LeerEntero("ID turno a confirmar", 1);
                        if (_gestorTurnos.Confirmar(ic)) Consola.Ok("Turno confirmado.");
                        else Consola.Error("No se pudo confirmar.");
                        Consola.Pausar(); break;
                    case 5:
                        int ia = Consola.LeerEntero("ID turno a marcar atendido", 1);
                        if (_gestorTurnos.MarcarAtendido(ia)) Consola.Ok("Marcado como atendido.");
                        else Consola.Error("Debe estar confirmado primero.");
                        Consola.Pausar(); break;
                    case 6:
                        int ica = Consola.LeerEntero("ID turno a cancelar", 1);
                        if (_gestorTurnos.Cancelar(ica)) Consola.Ok("Turno cancelado.");
                        else Consola.Error("No se pudo cancelar.");
                        Consola.Pausar(); break;
                    case 7:
                        int ip = Consola.LeerEntero("ID del paciente", 1);
                        _gestorTurnos.ListarPorPaciente(ip); Consola.Pausar(); break;
                    case 8:
                        int id2 = Consola.LeerEntero("ID del doctor", 1);
                        _gestorTurnos.ListarPorDoctor(id2);  Consola.Pausar(); break;
                    case 0: return;
                }
            }
        }

        private void MenuReportes()
        {
            Consola.LimpiarPantalla();
            _gestorTurnos.MostrarResumen();
            _gestorDoctores.MostrarMatrizOcupacion();
            Console.WriteLine($"  Pacientes: {_gestorPacientes.Total}  |  Doctores: {_gestorDoctores.Total}  |  Turnos: {_gestorTurnos.Total}");
            Consola.Linea();
            Consola.Pausar();
        }
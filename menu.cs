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
        private Paciente CapturarPaciente()
        {
            Consola.Titulo("NUEVO PACIENTE");
            Paciente p = new();
            p.Nombre   = Consola.LeerTexto("Nombre");
            p.Apellido = Consola.LeerTexto("Apellido");
            p.Cedula   = Consola.LeerTexto("Cedula");
            p.Telefono = Consola.LeerTexto("Telefono");
            p.Edad     = Consola.LeerEntero("Edad", 0, 120);
            p.Peso     = Consola.LeerFlotante("Peso en kg (ej: 70.5)", 1f, 300f);
            p.Altura   = Consola.LeerFlotante("Altura en m (ej: 1.65)", 0.5f, 2.5f);
            p.Genero   = Consola.LeerChar("Genero (M/F/O)", new[] { 'M', 'F', 'O' });
            return p;
        }

        private Doctor CapturarDoctor()
        {
            Consola.Titulo("NUEVO DOCTOR");
            Doctor d = new();
            d.Nombre       = Consola.LeerTexto("Nombre");
            d.Apellido     = Consola.LeerTexto("Apellido");
            d.Especialidad = Consola.LeerTexto("Especialidad");
            d.Genero       = Consola.LeerChar("Genero (M/F)", new[] { 'M', 'F' });
            return d;
        }

        private Turno CapturarTurno()
        {
            Consola.Titulo("NUEVO TURNO");
            _gestorPacientes.Listar();
            _gestorDoctores.Listar();
            Turno t = new();
            t.IdPaciente   = Consola.LeerEntero("ID del paciente", 1);
            t.IdDoctor     = Consola.LeerEntero("ID del doctor", 1);
            Console.WriteLine("  Dias: 0=Lunes  1=Martes  2=Miercoles  3=Jueves  4=Viernes");
            t.Dia          = Consola.LeerEntero("Dia (0-4)", 0, 4);
            Console.WriteLine("  Horas: 0=08:00  1=09:00  2=10:00  ...  9=17:00");
            t.HoraSlot     = Consola.LeerEntero("Hora slot (0-9)", 0, 9);
            t.Motivo       = Consola.LeerTexto("Motivo de consulta");
            t.Especialidad = Consola.LeerTexto("Especialidad");
            t.Fecha        = DateTime.Now.ToString("dd/MM/yyyy");
            return t;
        }

        private void MostrarDetallePaciente(Paciente p)
        {
            Consola.Titulo("DETALLE PACIENTE");
            Console.WriteLine($"  ID       : {p.Id}");
            Console.WriteLine($"  Nombre   : {p.NombreCompleto}");
            Console.WriteLine($"  Cedula   : {p.Cedula}");
            Console.WriteLine($"  Telefono : {p.Telefono}");
            Console.WriteLine($"  Edad     : {p.Edad} anios");
            Console.WriteLine($"  Peso     : {p.Peso:F1} kg");
            Console.WriteLine($"  Altura   : {p.Altura:F2} m");
            Console.WriteLine($"  IMC      : {p.CalcularIMC():F2}");
            Console.WriteLine($"  Genero   : {p.Genero}");
        }

        private void CargarDatos()
        {
            _gestorDoctores.Registrar(new Doctor { Nombre = "Ana",    Apellido = "Torres",  Especialidad = "Medicina General", Genero = 'F' });
            _gestorDoctores.Registrar(new Doctor { Nombre = "Carlos", Apellido = "Mendoza", Especialidad = "Pediatria",        Genero = 'M' });
            _gestorDoctores.Registrar(new Doctor { Nombre = "Sofia",  Apellido = "Rios",    Especialidad = "Cardiologia",      Genero = 'F' });

            _gestorPacientes.Registrar(new Paciente { Nombre = "Luis",  Apellido = "Garcia",   Cedula = "0912345678", Telefono = "0991111111", Edad = 35, Peso = 75.5f, Altura = 1.72f, Genero = 'M' });
            _gestorPacientes.Registrar(new Paciente { Nombre = "Maria", Apellido = "Lopez",    Cedula = "0923456789", Telefono = "0992222222", Edad = 28, Peso = 60.0f, Altura = 1.60f, Genero = 'F' });
            _gestorPacientes.Registrar(new Paciente { Nombre = "Pedro", Apellido = "Alvarado", Cedula = "0934567890", Telefono = "0993333333", Edad = 52, Peso = 88.2f, Altura = 1.78f, Genero = 'M' });

            _gestorTurnos.Agendar(new Turno { IdPaciente = 1, IdDoctor = 1, Dia = 0, HoraSlot = 0, Motivo = "Chequeo general",    Especialidad = "Medicina General", Fecha = "16/06/2025" });
            _gestorTurnos.Agendar(new Turno { IdPaciente = 2, IdDoctor = 2, Dia = 1, HoraSlot = 2, Motivo = "Control pediatrico", Especialidad = "Pediatria",        Fecha = "17/06/2025" });
            _gestorTurnos.Confirmar(1);
        }
    }
}
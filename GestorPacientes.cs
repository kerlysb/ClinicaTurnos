namespace ClinicaTurnos
{
    public class GestorPacientes
    {
        private Paciente[] _pacientes = new Paciente[Constantes.MAX_PACIENTES];
        private int _total = 0;
        private int _siguienteId = 1;

        public int Total => _total;

        public bool Registrar(Paciente p)
        {
            if (_total >= Constantes.MAX_PACIENTES)
            {
                Consola.Error("Capacidad maxima alcanzada.");
                return false;
            }
            for (int i = 0; i < _total; i++)
            {
                if (_pacientes[i].Cedula == p.Cedula)
                {
                    Consola.Error("Ya existe un paciente con esa cedula.");
                    return false;
                }
            }
            p.Id = _siguienteId++;
            p.Activo = true;
            _pacientes[_total++] = p;
            return true;
        }
        public bool Eliminar(int id)
        {
            for (int i = 0; i < _total; i++)
            {
                if (_pacientes[i].Id == id)
                {
                    _pacientes[i].Activo = false;
                    return true;
                }
            }
            return false;
        }

        public Paciente? BuscarPorId(int id)
        {
            for (int i = 0; i < _total; i++)
                if (_pacientes[i].Id == id && _pacientes[i].Activo)
                    return _pacientes[i];
            return null;
        }

        public Paciente? BuscarPorCedula(string cedula)
        {
            for (int i = 0; i < _total; i++)
                if (_pacientes[i].Cedula == cedula && _pacientes[i].Activo)
                    return _pacientes[i];
            return null;
        }
        public void Listar()
        {
            Consola.Titulo("LISTA DE PACIENTES");
            Console.WriteLine($"  {"ID",-5} {"Nombre Completo",-24} {"Cedula",-13} {"Edad",-6} {"Gen",-5} {"Peso",-7} {"Altura",-8} {"IMC"}");
            Consola.Linea();
            int activos = 0;
            for (int i = 0; i < _total; i++)
            {
                if (!_pacientes[i].Activo) continue;
                Paciente p = _pacientes[i];
                Console.WriteLine($"  {p.Id,-5} {p.NombreCompleto,-24} {p.Cedula,-13} {p.Edad,-6} {p.Genero,-5} {p.Peso,-7:F1} {p.Altura,-8:F2} {p.CalcularIMC():F1}");
                activos++;
            }
            if (activos == 0) Console.WriteLine("  No hay pacientes registrados.");
            Consola.Linea();
            Console.WriteLine($"  Total activos: {activos}");
        }

        public Paciente[] ObtenerTodos() => _pacientes;
    }
}

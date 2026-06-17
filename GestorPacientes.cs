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

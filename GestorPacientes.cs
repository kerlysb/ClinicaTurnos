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
        
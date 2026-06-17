namespace ClinicaTurnos
{
    public class GestorDoctores
    {
        private Doctor[] _doctores = new Doctor[Constantes.MAX_DOCTORES];
        private int _total = 0;
        private int _siguienteId = 1;
        private int[,] _matrizOcupacion = new int[Constantes.DIAS_SEMANA, Constantes.HORAS_DIA];

        public int Total => _total;

        public bool Registrar(Doctor d)
        {
            if (_total >= Constantes.MAX_DOCTORES)
            {
                Consola.Error("Capacidad maxima de doctores alcanzada.");
                return false;
            }
            d.Id = _siguienteId++;
            d.Activo = true;
            d.InicializarHorario();
            _doctores[_total++] = d;
            return true;
        }

        public Doctor? BuscarPorId(int id)
        {
            for (int i = 0; i < _total; i++)
                if (_doctores[i].Id == id && _doctores[i].Activo)
                    return _doctores[i];
            return null;
        }

        private int IndiceDeId(int id)
        {
            for (int i = 0; i < _total; i++)
                if (_doctores[i].Id == id) return i;
            return -1;
        }
        public bool EstaDisponible(int idDoctor, int dia, int hora)
        {
            int idx = IndiceDeId(idDoctor);
            if (idx == -1) return false;
            return _doctores[idx].HorarioDisponible[dia, hora];
        }

        public void MarcarOcupado(int idDoctor, int dia, int hora)
        {
            int idx = IndiceDeId(idDoctor);
            if (idx == -1) return;
            _doctores[idx].HorarioDisponible[dia, hora] = false;
            _matrizOcupacion[dia, hora]++;
        }

        public void LiberarSlot(int idDoctor, int dia, int hora)
        {
            int idx = IndiceDeId(idDoctor);
            if (idx == -1) return;
            _doctores[idx].HorarioDisponible[dia, hora] = true;
            if (_matrizOcupacion[dia, hora] > 0)
                _matrizOcupacion[dia, hora]--;
        }
        public void Listar()
        {
            Consola.Titulo("LISTA DE DOCTORES");
            Console.WriteLine($"  {"ID",-5} {"Nombre",-28} {"Especialidad",-20} {"Gen"}");
            Consola.Linea();
            int activos = 0;
            for (int i = 0; i < _total; i++)
            {
                if (!_doctores[i].Activo) continue;
                Doctor d = _doctores[i];
                Console.WriteLine($"  {d.Id,-5} {d.NombreCompleto,-28} {d.Especialidad,-20} {d.Genero}");
                activos++;
            }
            if (activos == 0) Console.WriteLine("  No hay doctores registrados.");
            Consola.Linea();
        }

        public void MostrarMatrizOcupacion()
        {
            string[] dias = { "LUN", "MAR", "MIE", "JUE", "VIE" };
            Consola.Titulo("MATRIZ DE OCUPACION (turnos por dia/hora)");
            Console.Write($"  {"HORA",-8}");
            for (int d = 0; d < 5; d++) Console.Write($" {dias[d],5}");
            Console.WriteLine();
            Consola.Linea();
            for (int h = 0; h < Constantes.HORAS_DIA; h++)
            {
                Console.Write($"  {8 + h:D2}:00   ");
                for (int d = 0; d < 5; d++)
                {
                    int ocu = _matrizOcupacion[d, h];
                    Console.ForegroundColor = ocu == 0 ? ConsoleColor.Green : ConsoleColor.Yellow;
                    Console.Write($" {ocu,5}");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Consola.Linea();
            Console.WriteLine("  Verde = sin turnos    Amarillo = con turnos");
        }

        public Doctor[] ObtenerTodos() => _doctores;
    }
}
        
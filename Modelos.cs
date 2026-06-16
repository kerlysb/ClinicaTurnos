namespace ClinicaTurnos
{
    public static class Constantes
    {
        public const int MAX_PACIENTES = 50;
        public const int MAX_TURNOS    = 100;
        public const int MAX_DOCTORES  = 10;
        public const int DIAS_SEMANA   = 7;
        public const int HORAS_DIA     = 10;
    }
    public struct Paciente
    {
        public int    Id;
        public string Nombre;
        public string Apellido;
        public string Cedula;
        public string Telefono;
        public int    Edad;
        public float  Peso;
        public float  Altura;
        public char   Genero;
        public bool   Activo;

        public double CalcularIMC()
        {
            if (Altura <= 0) return 0;
            return Math.Round(Peso / (Altura * Altura), 2);
        }

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
    public struct Doctor
    {
        public int    Id;
        public string Nombre;
        public string Apellido;
        public string Especialidad;
        public char   Genero;
        public bool   Activo;
        public bool[,] HorarioDisponible;

        public string NombreCompleto => $"Dr(a). {Nombre} {Apellido}";

        public void InicializarHorario()
        {
            HorarioDisponible = new bool[Constantes.DIAS_SEMANA, Constantes.HORAS_DIA];
            for (int d = 0; d < 5; d++)
                for (int h = 0; h < Constantes.HORAS_DIA; h++)
                    HorarioDisponible[d, h] = true;
        }
    }
public struct Turno
    {
        public int    Id;
        public int    IdPaciente;
        public int    IdDoctor;
        public int    Dia;
        public int    HoraSlot;
        public bool   Confirmado;
        public bool   Atendido;
        public string Especialidad;
        public string Motivo;
        public string Fecha;

        public string HoraTexto  => $"{8 + HoraSlot:D2}:00";

        public string DiaNombre => Dia switch
        {
            0 => "Lunes", 1 => "Martes", 2 => "Miercoles",
            3 => "Jueves", 4 => "Viernes", _ => "---"
        };

        public string Estado
        {
            get
            {
                if (Atendido)   return "ATENDIDO";
                if (Confirmado) return "CONFIRMADO";
                return "PENDIENTE";
            }
        }
    }
}
    
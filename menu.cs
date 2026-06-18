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
namespace Supercole.Clases
{
    class Materia
    {
        private string id_materia;
        private string materia_nombre;

        public Materia(string id_materia, string materia_nombre)
        {
            this.id_materia = id_materia;
            this.materia_nombre = materia_nombre;
        }

        public string Id_materia { get => id_materia; set => id_materia = value; }
        public string Materia_nombre { get => materia_nombre; set => materia_nombre = value; }
    }
}

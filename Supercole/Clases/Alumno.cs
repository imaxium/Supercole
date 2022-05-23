using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supercole.Clases
{
    class Alumno
    {
        string id_alumno;
        string alumno_nombre;
        int alumno_telefono;
        int alumno_matricula;
        string id_materia;
        //esto es una prueba
        string id_clase;

        public Alumno(string id_alumno, string alumno_nombre, int alumno_telefono, int alumno_matricula, string id_materia, string id_clase)
        {
            this.id_alumno = id_alumno;
            this.alumno_nombre = alumno_nombre;
            this.alumno_telefono = alumno_telefono;
            this.alumno_matricula = alumno_matricula;
            this.id_materia = id_materia;
            this.id_clase = id_clase;
        }

        public string Id_alumno { get => id_alumno; set => id_alumno = value; }
        public string Alumno_nombre { get => alumno_nombre; set => alumno_nombre = value; }
        public int Alumno_telefono { get => alumno_telefono; set => alumno_telefono = value; }
        public int Alumno_matricula { get => alumno_matricula; set => alumno_matricula = value; }
        public string Id_materia { get => id_materia; set => id_materia = value; }
        public string Id_clase { get => id_clase; set => id_clase = value; }
    }
}

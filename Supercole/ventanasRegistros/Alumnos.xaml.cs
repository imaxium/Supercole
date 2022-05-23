using Npgsql;
using Supercole.Clases;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Supercole.ventanasRegistros
{

    public partial class Alumnos : Window
    {
        string cs = "Host=localhost;port=5433;Username=postgres;Password=1400;Database=db_supercole";
        NpgsqlConnection con;
        List<Alumno> listaAlumnos;
        List<Clase> listaClases;
        List<Materia> listaMaterias;

        string id_materia;
        string id_clase;
        string id_alumno;

        public Alumnos()
        {
            InitializeComponent();
            cargarDatosEnComboBox();
            cargarDatosEnDataGrid();
        }

        private void cargarDatosEnDataGrid()
        {
            listaAlumnos = new List<Alumno>();
            con = new NpgsqlConnection(cs);

            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string consulta = "SELECT * FROM  public.\"Alumnos\";";

            cmd = new NpgsqlCommand(consulta, con);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                listaAlumnos.Add(new Alumno(dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), Convert.ToInt32(dr.GetValue(2).ToString()), Convert.ToInt32(dr.GetValue(3).ToString()), dr.GetValue(4).ToString(), dr.GetValue(5).ToString()));
            }
            datagridAlumnos.ItemsSource = listaAlumnos;

            cmd.Connection.Close();
            con.Close();
        }

        private void cargarDatosEnComboBox()
        {
            listaClases = new List<Clase>();
            con = new NpgsqlConnection(cs);

            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string consulta = "SELECT * FROM  public.\"Clases\";";

            cmd = new NpgsqlCommand(consulta, con);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                listaClases.Add(new Clase(dr.GetValue(0).ToString(), dr.GetValue(1).ToString()));
                ComboboxClase.Items.Add(dr.GetValue(1).ToString());
            }

            cmd.Connection.Close();
            con.Close();

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            listaMaterias = new List<Materia>();
            con = new NpgsqlConnection(cs);

            con.Open();

            NpgsqlCommand cmd2 = new NpgsqlCommand();
            cmd.Connection = con;

            string consulta2 = "SELECT * FROM  public.\"Materias\";";

            cmd = new NpgsqlCommand(consulta2, con);

            NpgsqlDataReader dr2 = cmd.ExecuteReader();

            while (dr2.Read())
            {
                listaMaterias.Add(new Materia(dr2.GetValue(0).ToString(), dr2.GetValue(1).ToString()));
                ComboboxMateria.Items.Add(dr2.GetValue(1).ToString());
            }

            cmd.Connection.Close();
            con.Close();

        }

        private void ComboboxCurso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCurso.Visibility = ComboboxMateria.SelectedItem == null ? Visibility.Visible : Visibility.Hidden;

            foreach (Materia m in listaMaterias)
            {
                if (m.Materia_nombre.Equals(ComboboxMateria.SelectedItem.ToString()))
                {
                    id_materia = m.Id_materia;
                }
            }

        }

        private void ComboboxClase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtClase.Visibility = ComboboxClase.SelectedItem == null ? Visibility.Visible : Visibility.Hidden;

            foreach (Clase m in listaClases)
            {
                if (m.Clase_nombre.Equals(ComboboxClase.SelectedItem.ToString()))
                {
                    id_clase = m.Id_clase;
                }
            }
        }

        private void registrarAlumno(object sender, RoutedEventArgs e)
        {
            string ultimoID = null;

            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sentencia = "SELECT id_alumno FROM public.\"Alumnos\" order by id_alumno desc limit 1; ";

            cmd = new NpgsqlCommand(sentencia, con);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ultimoID = dr.GetValue(0).ToString();
            }
            Console.WriteLine(ultimoID);
            obtenerID oID = new obtenerID();
            id_alumno = oID.generarID(ultimoID, "alumno");
            cmd.Connection.Close();

            cmd.Connection.Open();
            cmd.CommandText = "INSERT INTO public.\"Alumnos\" VALUES('" + id_alumno + "', '" + textBoxNombreAlumno.Text + "', '" + textBoxTelefonoAlumno.Text + "', '" + textBoxMatriculaAlumno.Text + "', '" + id_materia + "', '" + id_clase + "' );";
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            con.Close();

            cargarDatosEnDataGrid();

            textBoxNombreAlumno.Text = "";
            textBoxTelefonoAlumno.Text = "";
            textBoxMatriculaAlumno.Text = "";
            textBoxBuscarPorNombre.Text = "";
        }

        private void modificarAlumno(object sender, RoutedEventArgs e)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sentencia = "UPDATE public.\"Alumnos\" " +
                "SET alumno_nombre = '" + textBoxNombreAlumno.Text + "', alumno_telefono = '" + Convert.ToInt32(textBoxTelefonoAlumno.Text) + "', alumno_matricula = '" + Convert.ToInt32(textBoxMatriculaAlumno.Text) + "', id_clase =  '" + id_clase + "', id_materia = '"+ id_materia +"' "+
                "WHERE id_alumno = '" + textBoxCodigoAlumno.Text + "';";

            cmd.CommandText = sentencia;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            con.Close();

            cargarDatosEnDataGrid();

            textBoxCodigoAlumno.Text = "";
            textBoxNombreAlumno.Text = "";
            textBoxTelefonoAlumno.Text = "";
            textBoxMatriculaAlumno.Text = "";
            textBoxBuscarPorNombre.Text = "";

        }

        private void buscarAlumno(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (textBoxBuscarPorNombre.Text.Equals(""))
            {
                cargarDatosEnDataGrid();
            }
            foreach (Alumno a in listaAlumnos)
            {
                if (a.Alumno_nombre.ToString().Equals(textBoxBuscarPorNombre.Text))
                {
                    List<Alumno> alumno = new List<Alumno>();
                    alumno.Add(a);
                    datagridAlumnos.ItemsSource = alumno;
                }
            }
        }

        private void mostrarAlumno(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var alumno = datagridAlumnos.SelectedItem as Alumno;
            textBoxCodigoAlumno.Text = alumno.Id_alumno;
            textBoxNombreAlumno.Text = alumno.Alumno_nombre;
            textBoxTelefonoAlumno.Text = alumno.Alumno_telefono.ToString();
            textBoxMatriculaAlumno.Text = alumno.Alumno_matricula.ToString();

            foreach (Materia m in listaMaterias)
                if (m.Id_materia.Equals(alumno.Id_materia))
                    ComboboxMateria.SelectedItem = m.Materia_nombre;

            foreach (Clase c in listaClases)
                if (c.Id_clase.Equals(alumno.Id_clase))
                    ComboboxClase.SelectedItem = c.Clase_nombre;
        }

        private void elminarUsuario(object sender, RoutedEventArgs e)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sentencia = " DELETE FROM public.\"Alumnos\" WHERE id_alumno = '" + textBoxCodigoAlumno.Text + "';";

            cmd.CommandText = sentencia;
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            con.Close();

            cargarDatosEnDataGrid();

            textBoxCodigoAlumno.Text = "";
            textBoxNombreAlumno.Text = "";
            textBoxTelefonoAlumno.Text = "";
            textBoxMatriculaAlumno.Text = "";
            textBoxBuscarPorNombre.Text = "";
        }
    }
}

using Npgsql;
using Supercole.Clases;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Supercole.ventanasRegistros
{

    public partial class Cursos : Window
    {
        string cs = "Host=localhost;port=5433;Username=postgres;Password=1400;Database=db_supercole";
        NpgsqlConnection con;
        List<Materia> listaMaterias;
        List<Alumno> listaAlumnos;

        string id_materia;
        string materia_nombre;

        public Cursos()
        {
            InitializeComponent();
            cargarDatosEnDataGrid();
            cargarAlumnos();
        }

        private void cargarDatosEnDataGrid()
        {
            listaMaterias = new List<Materia>();

            con = new NpgsqlConnection(cs);

            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string consulta = "SELECT * FROM  public.\"Materias\";";

            cmd = new NpgsqlCommand(consulta, con);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                listaMaterias.Add(new Materia(dr.GetValue(0).ToString(), dr.GetValue(1).ToString()));
            }

            DataGridMaterias.ItemsSource = listaMaterias;

            cmd.Connection.Close();
            con.Close();
        }

        private void RegistrarMateria(object sender, RoutedEventArgs e)
        {
            materia_nombre = textBoxMateriaNombre.Text;
            string ultimoID = null;

            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sentencia = "SELECT id_materia FROM public.\"Materias\" order by id_materia desc limit 1; ";

            cmd = new NpgsqlCommand(sentencia, con);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ultimoID = dr.GetValue(0).ToString();
            }
            Console.WriteLine(ultimoID);
            obtenerID oID = new obtenerID();
            id_materia = oID.generarID(ultimoID, "materia");
            cmd.Connection.Close();

            cmd.Connection.Open();
            cmd.CommandText = "INSERT INTO public.\"Materias\" VALUES('" + id_materia + "', '" + materia_nombre + "')";
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            con.Close();

            cargarDatosEnDataGrid();

            textBoxMateriaNombre.Text = "";
        }

        private void seleccionarMateria(object sender, MouseButtonEventArgs e)
        {
            var materia = DataGridMaterias.SelectedItem as Materia;

            textBoxIdMateria.Text = materia.Id_materia;
            textBoxMateriaNombre.Text = materia.Materia_nombre;

        }

        private void modificarUsuario(object sender, RoutedEventArgs e)
        {
            materia_nombre = textBoxMateriaNombre.Text;

            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sentencia = "UPDATE public.\"Materias\" " +
                "SET materia_nombre = '" + materia_nombre + "' " +
                "WHERE id_materia = '" + textBoxIdMateria.Text + "';";

            cmd.CommandText = sentencia;
            cmd.ExecuteNonQuery();

            cargarDatosEnDataGrid();

            textBoxMateriaNombre.Text = "";
            textBoxIdMateria.Text = "";

            cmd.Connection.Close();
            con.Close();
        }

        private void eliminarMateria(object sender, RoutedEventArgs e)
        {
            Alumno alumno = listaAlumnos.Find(
                delegate(Alumno a)
                {
                    return a.Id_materia == textBoxIdMateria.Text;
                }
                );

            if (alumno == null)
            {
                con = new NpgsqlConnection(cs);
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "DELETE FROM public.\"Materias\" WHERE id_materia = '" + textBoxIdMateria.Text + "';";
                cmd.ExecuteNonQuery();

                textBoxIdMateria.Text = "";
                textBoxMateriaNombre.Text = "";

                cmd.Connection.Close();
                con.Close();
            }
            else
            {
                MessageBox.Show("no se ha podido eliminar la materia", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
   
            cargarDatosEnDataGrid();
        }

        private void cargarAlumnos()
        {
            listaAlumnos = new List<Alumno>();

            con = new NpgsqlConnection(cs);

            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string consulta = "SELECT * FROM  public.\"Alumnos\";";

            cmd = new NpgsqlCommand(consulta, con);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    listaAlumnos.Add(new Alumno(dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), Convert.ToInt32(dr.GetValue(2).ToString()), Convert.ToInt32(dr.GetValue(3).ToString()), dr.GetValue(4).ToString(), dr.GetValue(5).ToString()));
                }
            }
            else
            {
                //en caso de que no haya alumnos en la base de datos
                listaAlumnos.Add(new Alumno("x", "x", 1, 4, "x", "x"));
            }

            cmd.Connection.Close();
            con.Close();
        }
    }
}

using Npgsql;
using Supercole.Clases;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Supercole.ventanasRegistros
{

    public partial class Clases : Window
    {
        string cs = "Host=localhost;port=5433;Username=postgres;Password=1400;Database=db_supercole";
        NpgsqlConnection con;
        List<Clase> listaClases;
        List<Alumno> listaAlumnos;

        string id_clase;
        string clase_nombre;

        public Clases()
        {
            InitializeComponent();
            cargarDatosEnDataGrid();
            cargarAlumnos();
        }

        private void cargarDatosEnDataGrid()
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
            }

            DataGridClases.ItemsSource = listaClases;

            cmd.Connection.Close();
            con.Close();
        }

        private void registrarClase(object sender, RoutedEventArgs e)
        {
            clase_nombre = textBoxNombreClase.Text;
            string ultimoID = null;

            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sentencia = "SELECT id_clase FROM public.\"Clases\" order by id_clase desc limit 1; ";

            cmd = new NpgsqlCommand(sentencia, con);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ultimoID = dr.GetValue(0).ToString();
            }
            Console.WriteLine(ultimoID);
            obtenerID oID = new obtenerID();
            id_clase = oID.generarID(ultimoID, "clase");
            cmd.Connection.Close();

            cmd.Connection.Open();
            cmd.CommandText = "INSERT INTO public.\"Clases\" VALUES('" + id_clase + "', '" + clase_nombre + "')";
            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            con.Close();

            cargarDatosEnDataGrid();

            textBoxNombreClase.Text = "";
        }

        private void modificarClase(object sender, RoutedEventArgs e)
        {
            clase_nombre = textBoxNombreClase.Text;

            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sentencia = "UPDATE public.\"Clases\" " +
                "SET clase_nombre = '" + clase_nombre + "' " +
                "WHERE id_clase = '" + textBoxIdClase.Text + "';";

            cmd.CommandText = sentencia;
            cmd.ExecuteNonQuery();

            cargarDatosEnDataGrid();

            textBoxNombreClase.Text = "";
            textBoxIdClase.Text = "";

            cmd.Connection.Close();
            con.Close();
        }

        private void seleccionarClase(object sender, MouseButtonEventArgs e)
        {
            var clase = DataGridClases.SelectedItem as Clase;

            textBoxIdClase.Text = clase.Id_clase;
            textBoxNombreClase.Text = clase.Clase_nombre;
        }

        private void eliminarClase(object sender, RoutedEventArgs e)
        {
            Alumno alumno = listaAlumnos.Find(
                delegate(Alumno a)
                {
                    return a.Id_clase == textBoxIdClase.Text;
                }
                );
            if(alumno == null)
            {
                con = new NpgsqlConnection(cs);
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "DELETE FROM public.\"Clases\" WHERE id_clase = '" + textBoxIdClase.Text + "';";
                cmd.ExecuteNonQuery();

                textBoxIdClase.Text = "";
                textBoxNombreClase.Text = "";

                cmd.Connection.Close();
                con.Close();
            }
            else
            {
                MessageBox.Show("no se ha podido eliminar la clase", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
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

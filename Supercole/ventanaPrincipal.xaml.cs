using Npgsql;
using Supercole.ventanasRegistros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Supercole
{

    public partial class ventanaPrincipal : Window
    {
        string cs = "Host=localhost;port=5433;Username=postgres;Password=1400;Database=db_supercole";
        NpgsqlConnection con;

        public ventanaPrincipal(string nombre, string tipoUsuario)
        {
            InitializeComponent();
            mostrarHora();
            labelNombreDeUsuario.Content = nombre;
            identificarTipoDeUsuario(tipoUsuario);
            mostrarDatos();
        }

        private void mostrarDatos()
        {
            con = new NpgsqlConnection(cs);

            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string NumAlumnos = "SELECT COUNT(*) FROM public.\"Alumnos\";";

            cmd = new NpgsqlCommand(NumAlumnos, con);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                LabelAlumnosRegistrados.Content = dr.GetValue(0).ToString();
               
            }
            cmd.Connection.Close();


            //************************************************************************************

            NpgsqlCommand cmd2 = new NpgsqlCommand();
            cmd2.Connection = con;
            cmd2.Connection.Open();

            string NumClases = "SELECT COUNT(*) FROM public.\"Clases\";";

            cmd2 = new NpgsqlCommand(NumClases, con);

            NpgsqlDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                LabelClasesRegistradas.Content = dr2.GetValue(0).ToString();
                
            }
            cmd2.Connection.Close();

            //*****************************************************************************************

            NpgsqlCommand cmd3 = new NpgsqlCommand();
            cmd3.Connection = con;
            cmd3.Connection.Open();

            string NumMaterias = "SELECT COUNT(*) FROM public.\"Materias\";";

            cmd3 = new NpgsqlCommand(NumMaterias, con);

            NpgsqlDataReader dr3 = cmd3.ExecuteReader();

            while (dr3.Read())
            {
                LabelMateriasRegistrados.Content = dr3.GetValue(0).ToString();
                
            }
            cmd3.Connection.Close();

            con.Close();
        }

        private void identificarTipoDeUsuario(string tipoUsuario)
        {
            if (tipoUsuario.Equals("T0002"))
            {
                MenuItemRegistros.Visibility = Visibility.Collapsed;
            }
            else if (tipoUsuario.Equals("T0003"))
            {
                //MenuItemInformes.Visibility = Visibility.Collapsed;
                MenuItemUsuarios.Visibility = Visibility.Collapsed;
                MenuItemMaterias.Visibility = Visibility.Collapsed;
                MenuItemClases.Visibility = Visibility.Collapsed;
            }
        }

        private void mostrarHora()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            LabelHora.Content = "Hora: " + DateTime.Now.ToLongTimeString();
            LabelFecha.Content = "Fecha: " + DateTime.Now.ToShortDateString();
        }

        private void RegistroUsuarios(object sender, RoutedEventArgs e)
        {
            Usuarios ventanaUsuarios = new Usuarios();
            ventanaUsuarios.Owner = this;
            ventanaUsuarios.ShowDialog();
        }

        private void RegistroAlumnos(object sender, RoutedEventArgs e)
        {
            Alumnos ventanaAlumnos = new Alumnos();
            ventanaAlumnos.Owner = this;
            ventanaAlumnos.ShowDialog();
        }

        private void RegistroCursos(object sender, RoutedEventArgs e)
        {
            Cursos ventanaCursos = new Cursos();
            ventanaCursos.Owner = this;
            ventanaCursos.ShowDialog();
        }

        private void RegistroClases(object sender, RoutedEventArgs e)
        {
            Supercole.ventanasRegistros.Clases ventanaClases = new Supercole.ventanasRegistros.Clases();
            ventanaClases.Owner = this;
            ventanaClases.ShowDialog();
        }

        private void actualizarDatos(object sender, RoutedEventArgs e)
        {
            mostrarDatos();
        }

        private void salirDeAplicacion(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            
        }

        private void actualizarContraseña(object sender, MouseButtonEventArgs e)
        {
            VentanaCambioContraseña vcc = new VentanaCambioContraseña(labelNombreDeUsuario.Content.ToString());
            vcc.Owner = this;
            vcc.ShowDialog();
        }
    }
}

using Npgsql;
using Supercole.Clases;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Supercole.ventanasRegistros
{

    public partial class Usuarios : Window
    {
        string cs = "Host=localhost;port=5433;Username=postgres;Password=1400;Database=db_supercole";
        NpgsqlConnection con;
        List<Usuario> listaUsuarios;

        string id_codigo;
        string usuario_nombre;
        string usuario_nickname;
        string usuario_contrasena;
        string id_tipo_usuario;

        public Usuarios()
        {
            InitializeComponent();
            mostrarDatosEnDataGrid();
        }
        private void mostrarDatosEnDataGrid()
        {
            listaUsuarios = new List<Usuario>();

            con = new NpgsqlConnection(cs);

            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string consulta = "SELECT * FROM  public.\"Usuarios\";";

            cmd = new NpgsqlCommand(consulta, con);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                listaUsuarios.Add(new Usuario(dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), dr.GetValue(2).ToString(), dr.GetValue(4).ToString()));
            }

            datagridUsuarios.ItemsSource = listaUsuarios;

            cmd.Connection.Close();
            con.Close();
        }

        private void ComboboxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtTipo.Visibility = ComboboxTipo.SelectedItem == null ? Visibility.Visible : Visibility.Hidden;

            if (ComboboxTipo.SelectedIndex == 0)
            {
                id_tipo_usuario = "T0001";
            }
            else if (ComboboxTipo.SelectedIndex == 1)
            {
                id_tipo_usuario = "T0002";
            }
            else if (ComboboxTipo.SelectedIndex == 2)
            {
                id_tipo_usuario = "T0003";

            }

        }

        private void registrarUsuario(object sender, RoutedEventArgs e)
        {
            usuario_nombre = textBoxNombre.Text;
            usuario_nickname = textBoxUsuario.Text;
            usuario_contrasena = "1234";
            String ultimoID = null;

            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sentencia = "SELECT id_codigo FROM public.\"Usuarios\" order by id_codigo desc limit 1; ";

            cmd = new NpgsqlCommand(sentencia, con);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ultimoID = dr.GetValue(0).ToString();
            }

            obtenerID oID = new obtenerID();
            id_codigo = oID.generarID(ultimoID, "usuario");
            cmd.Connection.Close();

            cmd.Connection.Open();
            cmd.CommandText = "INSERT INTO public.\"Usuarios\" VALUES('" + id_codigo + "', '" + usuario_nombre + "','" + usuario_nickname + "','" + usuario_contrasena + "','" + id_tipo_usuario + "')";
            cmd.ExecuteNonQuery();

            mostrarDatosEnDataGrid();

            textBoxNombre.Text = "";
            textBoxUsuario.Text = "";
        }

        private void BuscarPorNombre(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (buscarPorNombre.Text.Equals(""))
            {
                mostrarDatosEnDataGrid();
            }

            foreach (Usuario us in listaUsuarios)
            {
                if (us.Usuario_nombre.ToString().Equals(buscarPorNombre.Text))
                {
                    List<Usuario> usuario = new List<Usuario>();
                    usuario.Add(us);
                    datagridUsuarios.ItemsSource = usuario;
                }
            }


        }

        private void modificarUsuario(object sender, RoutedEventArgs e)
        {
            id_codigo = textBoxCodigo.Text;
            usuario_nombre = textBoxNombre.Text;
            usuario_nickname = textBoxUsuario.Text;

            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sentencia = "UPDATE public.\"Usuarios\" " +
                "SET usuario_nombre = '" + usuario_nombre + "', usuario_nickname = '" + usuario_nickname + "', id_tipo_usuario = '" + id_tipo_usuario + "' " +
                "WHERE id_codigo = '" + id_codigo + "';";

            cmd.CommandText = sentencia;
            cmd.ExecuteNonQuery();

            mostrarDatosEnDataGrid();

            textBoxNombre.Text = "";
            textBoxUsuario.Text = "";
            textBoxCodigo.Text = "";
            buscarPorNombre.Text = "";
           
        }

        private void eliminarUsuario(object sender, RoutedEventArgs e)
        {
            id_codigo = textBoxCodigo.Text;
            usuario_nombre = textBoxNombre.Text;
            usuario_nickname = textBoxUsuario.Text;

            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "DELETE FROM public.\"Usuarios\" WHERE id_codigo = '" + id_codigo + "';";
            cmd.ExecuteNonQuery();

            mostrarDatosEnDataGrid();

            textBoxNombre.Text = "";
            textBoxUsuario.Text = "";
            textBoxCodigo.Text = "";
            buscarPorNombre.Text = "";
        }

        private void mostrarUsuario(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var usuario = datagridUsuarios.SelectedItem as Usuario;

            textBoxCodigo.Text = usuario.Id_codigo;
            textBoxNombre.Text = usuario.Usuario_nombre;
            textBoxUsuario.Text = usuario.Usuario_nickname;

            if (usuario.Id_tipo_usuario.Equals("T0001"))
            {
                ComboboxTipo.SelectedIndex = 0;
            }
            else if (usuario.Id_tipo_usuario.Equals("T0002"))
            {
                ComboboxTipo.SelectedIndex = 1;
            }
            else if (usuario.Id_tipo_usuario.Equals("T0003"))
            {
                ComboboxTipo.SelectedIndex = 2;
            }
        }
    }
}

using Npgsql;
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

namespace Supercole.ventanasRegistros
{
    public partial class VentanaCambioContraseña : Window
    {
        string cs = "Host=localhost;port=5433;Username=postgres;Password=1400;Database=db_supercole";
        NpgsqlConnection con;

        public VentanaCambioContraseña(String nombreUsuario)
        {
            InitializeComponent();
            textBoxNombreUsuario.Text = nombreUsuario;
        }

        private void actualizarContrasena(object sender, RoutedEventArgs e)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string sentencia = " UPDATE public.\"Usuarios\" SET usuario_contrasena = '" + TextBoxContrasena.Password.ToString()+ "' WHERE usuario_nickname = '"+ textBoxNombreUsuario.Text.ToString()+"' ;";

            cmd.CommandText = sentencia;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            con.Close();
            MessageBox.Show("Contraseña actualizada");
        }
    }
}

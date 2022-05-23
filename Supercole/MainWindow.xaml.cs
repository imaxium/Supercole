using Npgsql;
using System.Windows;

namespace Supercole
{
    public partial class MainWindow : Window
    {
        string cs = "Host=localhost;port=5433;Username=postgres;Password=1400;Database=db_supercole";
        NpgsqlConnection con;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void cerrarAplicacion(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void entrarVentanaPrincipal(object sender, RoutedEventArgs e)
        {

            con = new NpgsqlConnection(cs);

            con.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = con;

            string consulta = "SELECT * FROM  public.\"Usuarios\" WHERE usuario_nickname = @usuario AND usuario_contrasena= @contrasena;";

            cmd = new NpgsqlCommand(consulta, con);

            cmd.Parameters.AddWithValue("@usuario", textBoxUsuario.Text);
            cmd.Parameters.AddWithValue("@contrasena", TextBoxContraseña.Password.ToString());

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    
                    ventanaPrincipal vp = new ventanaPrincipal(dr.GetValue(1).ToString(), dr.GetValue(4).ToString());
                    vp.Owner = this;
                    vp.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("usuario o contraseña incorrectas");
            }

            con.Close();

        }
    }
}

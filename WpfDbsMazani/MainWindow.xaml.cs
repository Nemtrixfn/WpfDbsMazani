using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace WpfDbsMazani
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sedlacek.jakub\source\repos\WpfDbsMazani\WpfDbsMazani\tisk.mdf;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ZobrazData();
        }

        public void ZobrazData()
        {
            List<Filamenty> filamenty = new List<Filamenty>();
            using(SqlCommand cmd = new SqlCommand("SELECT * FROM filamenty", conn))
            {
                conn.Open();
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        filamenty.Add(new Filamenty((int)reader["id"], reader["nazev"].ToString()));
                    }
                }
                conn.Close();
                lsvFilamenty.ItemsSource = filamenty;
            }


        }

        private void Smazat1_Click(object sender, RoutedEventArgs e)
        {
            List<Filamenty> filamenty = new List<Filamenty>();
            if (lsvFilamenty.ItemsSource != null)
            {
                Filamenty filament = (Filamenty)lsvFilamenty.SelectedItem;
                if (MessageBox.Show($"Chcete smazat položku {filament}?","Mazání dat",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    using (SqlCommand cmd = new SqlCommand($"DELETE FROM filamenty WHERE id={filament.id}", conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                    ZobrazData();

                }
                else
                {
                    MessageBox.Show("Položka nebyla smazána");
                }
            }
            else
            {
                MessageBox.Show("Vyberte položku ke smazání");
            }
        }
    }
}
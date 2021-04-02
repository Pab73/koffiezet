using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WpfApp31._4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        EenKoffiezet koffiezet = new EenKoffiezet();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            koffiezet.KoffiezetAanEvent += KoffiesetStaatAan;
            koffiezet.KoffiezetUitEvent += KoffiesetStaatUit;
            koffiezet.KoffieSchenkEvent += koffieSchenk;
            koffiezet.koffieStopSchenkEvent += koffieStopSchenk;
            koffiezet.Power = false;
        }

        private void koffieStopSchenk()
        {
            lblSchnekStatus.Content = "";
            MessageBox.Show("YOUR COFFEE IS READY !", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void koffieSchenk()
        {
            lblSchnekStatus.Content = koffiezet.StatusKoffieSchenken;
        }

        private void KoffiesetStaatUit()
        {
            Background = Brushes.Black;
            //geen koffie meer te krijgen
            lblBonenReservoir.Content = $"STATUS: SLEEP, BEANS HOPPER: {EenKoffiezet.BonenReservoir}  ";
            lblSchnekStatus.Content = "";
        }

        private void KoffiesetStaatAan()
        {
            //mogelijkheid om koffie te zetten.
            Background = Brushes.DarkCyan;
            lblBonenReservoir.Content = $"BEANS HOPPER: {EenKoffiezet.BonenReservoir} ";
        }

        private void btnKoffiezetAan_Click(object sender, RoutedEventArgs e)
        {
            koffiezet.Power = true;
        }

        private void btnKoffiezetUit_Click(object sender, RoutedEventArgs e)
        {
            koffiezet.Power = false;
        }

        private void btnEenKoffie_Click(object sender, RoutedEventArgs e)
        {
            if (koffiezet.Tim.IsEnabled)
                return;
            if (EenKoffiezet.BonenReservoir <= 0 && koffiezet.Power)
            {
                BonenOpMenu();
                return;
            }
            koffiezet.ZetDeKoffie();
            if (koffiezet.Power)
            {
                lblBonenReservoir.Content = $"BEANS HOPPER: {EenKoffiezet.BonenReservoir} ";
            }
        }

        private void BonenOpMenu()
        {
            DialogResult dialogbox = MessageBox.Show("NO MORE BEANS, FEED THE HOPPER?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogbox == System.Windows.Forms.DialogResult.Yes)
            {
                using (SoundPlayer mPlayer = new SoundPlayer(Properties.Resources.seeds_cristal_jar_4))
                {
                    mPlayer.Play();
                }
                EenKoffiezet.BonenReservoir = 15;
                lblBonenReservoir.Content = $"BEANS HOPPER: {EenKoffiezet.BonenReservoir} ";
            }
            else if (dialogbox == System.Windows.Forms.DialogResult.No)
            {
                koffiezet.Power = false;
            }
        }

        private void btnVulBonen_Click(object sender, RoutedEventArgs e)
        {
            if (koffiezet.Tim.IsEnabled)
                return;
            if (koffiezet.Power && EenKoffiezet.BonenReservoir < 15)
            {
                EenKoffiezet.BonenReservoir = 15;
                lblBonenReservoir.Content = $"BEANS HOPPER: {EenKoffiezet.BonenReservoir} ";
                using (SoundPlayer mPlayer = new SoundPlayer(Properties.Resources.seeds_cristal_jar_4))
                {
                    mPlayer.Play();
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

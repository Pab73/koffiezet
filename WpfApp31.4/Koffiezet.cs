using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp31._4
{
    public class Koffiezet
    {
        private int _pakBonen;
        private int _bonenReservoir = 15;
        private bool _power;
        private bool _isPouring;
        private string _statusKoffieSchenken;

        public delegate void KoffieHandler();
        public event KoffieHandler KoffiezetAanEvent;
        public event KoffieHandler KoffiezetUitEvent;
        public event KoffieHandler KoffieSchenkEvent;
        public event KoffieHandler koffieStopSchenkEvent;

        private DispatcherTimer _tim = new DispatcherTimer();
        private DateTime _startuur;


        public DispatcherTimer Tim
        {
            get => _tim;
            //set { _tim = value; }
        }
        private DateTime startuur
        {
            get => _startuur;
            set
            {
                _startuur = value;
            }
        }

        public Koffiezet()
        {
            Tim.Interval = new TimeSpan(0, 0, 1);
            Tim.Tick += Tim_Tick;
        }
        public void ZetDeKoffie()
        {
            if (Power)
            {
                StatusKoffieSchenken = "YOUR COFFEE IS BEING MADE...";
                IsPouring = true;
                startuur = DateTime.Now;
                Tim.Stop();
                Tim.Start();
                BonenReservoir -= 5;

            }
        }

        void Tim_Tick(object sender, EventArgs e)
        {
            using (SoundPlayer mPlayer = new SoundPlayer("C://Users/XYZ/source/repos/WpfApp31.4/WpfApp31.4/Sound/pouring-water-to-coffee-maker.wav"))
            {
                mPlayer.PlaySync();
            }
            DateTime eindtijd = startuur.AddSeconds(7.5);
            if (eindtijd <= DateTime.Now)
            {
                Tim.Stop();
                IsPouring = false;
            }
        }

        public string StatusKoffieSchenken
        {
            get => _statusKoffieSchenken;
            set => _statusKoffieSchenken = value;
        }

        public int PakBonen
        {
            get => _pakBonen;
            set
            {
                _pakBonen = 30;
            }
        }
        public int BonenReservoir
        {
            get => _bonenReservoir;
            set
            {
                _bonenReservoir = value;
            }
        }

        public bool Power
        {
            get => _power;
            set
            {
                _power = value;
                if (value == true)
                {
                    KoffiezetAanEvent?.Invoke();
                }
                else
                {
                    KoffiezetUitEvent?.Invoke();
                }
            }
        }

        public bool IsPouring
        {
            get => _isPouring;
            set
            {
                _isPouring = value;
                if (value == true)
                {
                    KoffieSchenkEvent?.Invoke();
                }
                else
                {
                    koffieStopSchenkEvent?.Invoke();
                }
            }
        }
    }
}

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
    class EenKoffiezet : DeSuperKlasseKoffiezet
    {
        private bool _power;
        private bool _isPouring;
        private string _statusKoffieSchenken;

        public delegate void KoffieHandler();
        public event KoffieHandler KoffiezetAanEvent;
        public event KoffieHandler KoffiezetUitEvent;
        public event KoffieHandler KoffieSchenkEvent;
        public event KoffieHandler koffieStopSchenkEvent;

        public EenKoffiezet()
        {
            Tim.Interval = new TimeSpan(0, 0, 1);
            Tim.Tick += Tim_Tick;
        }
        public override void ZetDeKoffie()
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

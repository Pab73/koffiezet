using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfApp31._4
{
    abstract class DeSuperKlasseKoffiezet
    {
        static int _bonenReservoir = 15;

        private DispatcherTimer _tim = new DispatcherTimer();

        private DateTime _startuur;

        public DispatcherTimer Tim
        {
            get => _tim;
        }
        public DateTime startuur
        {
            get => _startuur;
            set
            {
                _startuur = value;
            }
        }
        public abstract void ZetDeKoffie();

        public static int BonenReservoir
        {
            get => _bonenReservoir;
            set
            {
                _bonenReservoir = value;
            }
        }

    }
}

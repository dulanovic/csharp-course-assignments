using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class RezultatNaTurniru
    {
        private Turnir turnir;
        private int brojOsvojenihBodova;

        public int BrojOsvojenihBodova
        {
            get { return brojOsvojenihBodova; }
            set { brojOsvojenihBodova = value; }
        }


        public Turnir Turnir
        {
            get { return turnir; }
            set { turnir = value; }
        }

        public RezultatNaTurniru(Turnir turnir, int brojOsvojenihBodova)
        {
            this.turnir = turnir;
            this.brojOsvojenihBodova = brojOsvojenihBodova;
        }
    }
}

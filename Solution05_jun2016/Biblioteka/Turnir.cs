using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public enum Vrsta
    {
        GrandSlam, Masters, Drugi
    }

    public class Turnir
    {
        private int godina;
        private string naziv;
        private int maksimalanBrojBodova;
        private Vrsta vrsta;

        public Vrsta Vrsta
        {
            get { return vrsta; }
            set { vrsta = value; }
        }


        public int MaksimalanBrojBodova
        {
            get { return maksimalanBrojBodova; }
            set { maksimalanBrojBodova = value; }
        }


        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }


        public int Godina
        {
            get { return godina; }
            set { godina = value; }
        }

        public Turnir(int godina, string naziv, int maksimalanBrojBodova, Vrsta vrsta)
        {
            this.godina = godina;
            this.naziv = naziv;
            this.maksimalanBrojBodova = maksimalanBrojBodova;
            this.vrsta = vrsta;
        }

        public Turnir()
        {

        }
    }
}

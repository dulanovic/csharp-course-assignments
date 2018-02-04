using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public enum Klasa
    {
        Ekonomska,
        Biznis
    }

    public class Sediste
    {
        private int broj;
        private Klasa klasa;
                
        public Klasa Klasa
        {
            get { return klasa; }
            set { klasa = value; }
        }


        public int Broj
        {
            get { return broj; }
            set { broj = value; }
        }

        public Sediste(int broj, Klasa klasa)
        {
            this.broj = broj;
            this.klasa = klasa;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public delegate void Delegat();

    public class Let
    {
        private string relacija;
        private DateTime datum;
        private List<Putnik> listaPutnika;
        public static event Delegat Obavesti;

        public List<Putnik> ListaPutnika
        {
            get { return listaPutnika; }
            set { listaPutnika = value; }
        }


        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }


        public string Relacija
        {
            get { return relacija; }
            set { relacija = value; }
        }

        public Let(string relacija, DateTime datum)
        {
            this.relacija = relacija;
            this.datum = datum;
            this.listaPutnika = new List<Putnik>();
        }

        public string DajPodatke()
        {
            return string.Format("{0}, {1}", datum, relacija);
        }

        public void DodajPutnika(Putnik p)
        {
            bool zauzetoMesto = false;

            foreach (Putnik pu in listaPutnika)
            {
                if (p.Sediste.Broj == pu.Sediste.Broj)
                {
                    zauzetoMesto = true;
                }
            }
            if (!zauzetoMesto && listaPutnika.Count < 100)
            {
                bool nadjenPutnik = false;
                int indeksNadjenog = 0;

                for (int i = 0; i < listaPutnika.Count; i++)
                {
                    if (listaPutnika[i].Ime == p.Ime && listaPutnika[i].Prezime == p.Prezime)
                    {
                        indeksNadjenog = i;
                        nadjenPutnik = true;
                    }
                }
                if (nadjenPutnik)
                {
                    listaPutnika[indeksNadjenog] = p;
                }
                else
                {
                    listaPutnika.Add(p);
                }
                if (p.Sediste.Klasa == Klasa.Biznis)
                {
                    if (Obavesti != null)
                    {
                        Obavesti();
                    }
                }
            }

        }
    }
}

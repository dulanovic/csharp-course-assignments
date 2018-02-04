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
            string relacijaBezSamoglasnika = relacija.Replace("a", "");
            relacijaBezSamoglasnika = relacijaBezSamoglasnika.Replace("e", "");
            relacijaBezSamoglasnika = relacijaBezSamoglasnika.Replace("i", "");
            relacijaBezSamoglasnika = relacijaBezSamoglasnika.Replace("o", "");
            relacijaBezSamoglasnika = relacijaBezSamoglasnika.Replace("u", "");
            relacijaBezSamoglasnika = relacijaBezSamoglasnika.Replace("A", "");
            relacijaBezSamoglasnika = relacijaBezSamoglasnika.Replace("E", "");
            relacijaBezSamoglasnika = relacijaBezSamoglasnika.Replace("I", "");
            relacijaBezSamoglasnika = relacijaBezSamoglasnika.Replace("O", "");
            relacijaBezSamoglasnika = relacijaBezSamoglasnika.Replace("U", "");
            string[] relacijaNiz = relacijaBezSamoglasnika.Split('-');
            for (int i = 0; i < relacijaNiz.Length; i++)
            {
                relacijaNiz[i] = relacijaNiz[i].Trim();
            }
            string prvi = relacijaNiz[0];
            string drugi = relacijaNiz[1];
            string relacijaSkraceno = string.Format("{0}{1} - {2}{3}", prvi[0], prvi[prvi.Length - 1], drugi[0], drugi[drugi.Length - 1]);

            return string.Format("{0} {1}", datum.ToShortDateString(), relacijaSkraceno);
        }

        public void DodajPutnika(Putnik putnik)
        {
            if (listaPutnika.Count == 100)
            {
                return;
            }
            foreach (Putnik p in listaPutnika)
            {
                if (putnik.Sediste.Broj == p.Sediste.Broj)
                {
                    return;
                }
            }
            bool nadjen = false;
            int indeks = 0;
            for (int i = 0; i < listaPutnika.Count; i++)
            {
                if (putnik.Ime == listaPutnika[i].Ime && putnik.Prezime == listaPutnika[i].Prezime)
                {
                    nadjen = true;
                    indeks = i;
                }
            }
            if (nadjen)
            {
                listaPutnika[indeks] = putnik;
            }
            else
            {
                listaPutnika.Add(putnik);
            }
            if (putnik.Sediste.Klasa == Klasa.Biznis)
            {
                if (Obavesti != null)
                {
                    Obavesti();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class Let
    {
        private string relacija;
        private List<Putnik> listaPutnika;
        private DateTime datum;

        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }


        public List<Putnik> ListaPutnika
        {
            get { return listaPutnika; }
            set { listaPutnika = value; }
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
            relacijaBezSamoglasnika.Replace("e", "");
            relacijaBezSamoglasnika.Replace("i", "");
            relacijaBezSamoglasnika.Replace("o", "");
            relacijaBezSamoglasnika.Replace("u", "");
            string[] relacijaNiz = relacijaBezSamoglasnika.Split('-');
            string polazak = relacijaNiz[0];
            string destinacija = relacijaNiz[1];
            string polazak2 = string.Format("{0}{1}", polazak[0], polazak[polazak.Length - 2]);
            string destinacija2 = string.Format("{0}{1}", destinacija[0], destinacija[destinacija.Length - 2]);
            string ispis = string.Format("{0} {1}-{2}", datum, polazak2, destinacija2);
            return ispis;
        }

        public void DodajPutnika(Putnik p)
        {
            foreach (Putnik p1 in listaPutnika)
            {
                if (p.Sediste.Broj == p1.Sediste.Broj)
                {
                    Console.WriteLine("Sediste je zauzeto!");
                    return;
                }
            }
            if(listaPutnika.Count == 100)
            {
                Console.WriteLine("Nema slobodnih mesta na ovom letu!");
                return;
            }

            bool istiPutnik = false;
            int redniBrojNaListi = 0;

            for(int i = 0; i<listaPutnika.Count; i++)
            {
                if(p.Ime == listaPutnika[i].Ime && p.Prezime == listaPutnika[i].Prezime)
                {
                    istiPutnik = true;
                    redniBrojNaListi = i;
                }

            }
            if(istiPutnik)
            {
                listaPutnika[redniBrojNaListi] = p;
            } else
            {
                listaPutnika.Add(p);
            }

        }
    }
}

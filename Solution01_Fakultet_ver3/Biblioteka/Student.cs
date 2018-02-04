using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public enum Status
    {
        Redovan, NaDaljinu, Diplomirao
    }

    public class Student : Osoba
    {
        private string brojIndeksa;
        private Status status;
        private List<Ispit> listaIspita;

        public Status Status
        {
            get { return status; }
            set { status = value; }
        }

        public List<Ispit> ListaIspita
        {
            get { return listaIspita; }
            set { listaIspita = value; }
        }


        public string BrojIndeksa
        {
            get { return brojIndeksa; }
            set { brojIndeksa = value; }
        }

        public Student(string ime, string prezime, string brojIndeksa, Status status) : base(ime, prezime)
        {
            this.brojIndeksa = brojIndeksa;
            this.status = status;
            this.listaIspita = new List<Ispit>();
        }

        public override string DajPodatke()
        {
            string ispis = "";
            List<Profesor> listaProfesora = new List<Profesor>();
            int zbirOcena = 0;
            int brojIspita = 0;
            double prosek = 0;

            foreach (Ispit i in listaIspita)
            {
                zbirOcena += i.Ocena;
                brojIspita++;
                if (!listaProfesora.Contains(i.Profesor))
                {
                    listaProfesora.Add(i.Profesor);
                }
            }
            prosek = (double)zbirOcena / brojIspita;

            ispis += string.Format("{0}, {1}, {2}", brojIndeksa, base.DajPodatke(), prosek);

            foreach (Profesor p in listaProfesora)
            {
                ispis += string.Format("\n\t{0}", p.DajPodatke());
                foreach (Ispit i in listaIspita)
                {
                    if (i.Profesor.BrojRadneKnjizice == p.BrojRadneKnjizice)
                    {
                        ispis += string.Format("\n\t\t{0}", i.DajPodatke());
                    }
                }
            }
            return ispis;
        }

        public void PolozenIspit(Ispit i)
        {
            if (i.Ocena <= 5)
            {
                return;
            }

            int brojac = 0;
            foreach (Ispit isp in ListaIspita)
            {
                if (isp.Profesor.Equals(i.Profesor))
                {
                    brojac++;
                }
            }
            if (brojac >= 2)
            {
                return;
            }

            int indeks = 0;
            bool nadjen = false;
            for (int j = 0; j<listaIspita.Count; j++)
            {
                if (listaIspita[j].Predmet==i.Predmet)
                {
                    nadjen = true;
                    indeks = j;
                }
            }
            if (nadjen)
            {
                listaIspita[indeks] = i;
            } else
            {
                listaIspita.Add(i);
            }
        }
    }
}

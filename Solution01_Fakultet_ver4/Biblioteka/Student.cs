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

    public delegate void Delegat();

    public class Student : Osoba
    {
        private string brojIndeksa;
        private List<Ispit> listaIspita;
        private Status status;
        public static event Delegat Obavesti;

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
            double zbirOcena = 0;
            double prosecnaOcena = 0;
            foreach (Ispit i in ListaIspita)
            {
                zbirOcena += i.Ocena;
            }
            prosecnaOcena = (double)(zbirOcena / listaIspita.Count);
            ispis += string.Format("{0}, {1}, {2}", brojIndeksa, base.DajPodatke(), prosecnaOcena);

            List<Profesor> listaProfesora = new List<Profesor>();
            foreach (Ispit i in listaIspita)
            {
                if (!listaProfesora.Contains(i.Profesor))
                {
                    listaProfesora.Add(i.Profesor);
                }
            }
            foreach (Profesor p in listaProfesora)
            {
                ispis += string.Format("\n\t{0}", p.DajPodatke());
                foreach (Ispit i in listaIspita)
                {
                    if (i.Profesor.Equals(p))
                    {
                        ispis += string.Format("\n\t\t{0}", i.DajPodatke());
                    }
                }
            }
            return ispis;
        }

        public void PolozenIspit(Ispit isp)
        {
            int brojPolozenihKodProfesora = 0;
            foreach (Ispit i in listaIspita)
            {
                if (i.Profesor.Equals(isp.Profesor))
                {
                    brojPolozenihKodProfesora++;
                }
            }
            if (brojPolozenihKodProfesora >= 2 || isp.Ocena <= 5)
            {
                return;
            }
            bool polozen = false;
            int indeks = 0;
            for (int i = 0; i < listaIspita.Count; i++)
            {
                if (listaIspita[i].Predmet.Equals(isp.Predmet))
                {
                    polozen = true;
                    indeks = i;
                }
            }
            if (polozen)
            {
                listaIspita[indeks] = isp;
            }
            else
            {
                listaIspita.Add(isp);
            }
            if (isp.Ocena == 10)
            {
                if (Obavesti != null)
                {
                    Obavesti();
                }
            }
        }
    }
}

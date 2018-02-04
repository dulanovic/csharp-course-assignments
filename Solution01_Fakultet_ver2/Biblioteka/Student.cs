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
        private List<Ispit> listaIspita;
        private Status status;


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
            this.listaIspita = new List<Ispit>();
            this.status = status;
        }

        public override string DajPodatke()
        {
            int zbirOcena = 0;
            foreach (Ispit i in listaIspita)
            {
                zbirOcena += i.Ocena;
            }
            double prosecnaOcena = Convert.ToDouble(zbirOcena) / Convert.ToDouble(listaIspita.Count);
            string ispis = string.Format("{0} {1}, {2}", brojIndeksa, base.DajPodatke(), prosecnaOcena);

            List<Profesor> listaProfesora = new List<Profesor>();
            foreach (Ispit i in listaIspita)
            {
                if (!(listaProfesora.Contains(i.Profesor)))
                {
                    listaProfesora.Add(i.Profesor);
                }
            }

            foreach (Profesor p in listaProfesora)
            {
                ispis += string.Format("\n\t{0}", p.DajPodatke());
                foreach (Ispit i in listaIspita)
                {
                    if (i.Profesor.BrojRadneKnjizice == p.BrojRadneKnjizice)
                    {
                        ispis += string.Format("\n\t\t{0}\n", i.DajPodatke());
                    }
                }
            }
            return ispis;
        }

        public void PolozenIspit(Ispit i)
        {
            int brojac = 0;
            foreach (Ispit isp in listaIspita)
            {
                if (isp.Profesor.BrojRadneKnjizice == i.Profesor.BrojRadneKnjizice)
                {
                    brojac++;
                }
            }

            if (i.Ocena > 5 && brojac <= 2)
            {
                for (int k = 0; k < listaIspita.Count; k++)
                {
                    if (listaIspita[k].Predmet == i.Predmet)
                    {
                        listaIspita[k] = i;
                    }
                }
                listaIspita.Add(i);
            }

        }

    }
}

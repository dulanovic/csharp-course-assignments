using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public enum Status
    {
        Redovan,
        NaDaljinu,
        Diplomirao
    }

    public delegate void Delegat();

    public class Student : Osoba
    {
        private string brojIndeksa;

        public string BrojIndeksa
        {
            get { return brojIndeksa; }
            set { brojIndeksa = value; }
        }

        private List<Ispit> listaIspita;

        public List<Ispit> ListaIspita
        {
            get { return listaIspita; }
            set { listaIspita = value; }
        }

        private Status status;

        public Status Status
        {
            get { return status; }
            set { status = value; }
        }

        public Student(string ime, string prezime, string brojIndeksa, Status status) : base(ime, prezime)
        {
            this.brojIndeksa = brojIndeksa;
            this.status = status;
            this.listaIspita = new List<Ispit>();
        }

        public override string DajPodatke()
        {
            double zbirOcena = 0;
            for (int i = 0; i < listaIspita.Count; i++)
            {
                zbirOcena += Convert.ToDouble(listaIspita[i].Ocena);
            }

            double prosecnaOcena = zbirOcena / listaIspita.Count;

            List<Profesor> listaProfesora = new List<Profesor>();

            for (int i = 0; i<listaIspita.Count; i++)
            {
                for (int j = 0; j<listaProfesora.Count; j++)
                {
                    if(!(listaIspita[i].Profesor.BrojRadneKnjizice == listaProfesora[j].BrojRadneKnjizice))
                    {
                        listaProfesora.Add(listaIspita[i].Profesor);
                    }
                }
            }

            string ispis = string.Format("{0}, {1}, {2}", brojIndeksa, base.DajPodatke(), prosecnaOcena);

            foreach (Profesor p in listaProfesora)
            {
                ispis += string.Format("\n\t{0}", p.DajPodatke());

                foreach (Ispit i in listaIspita)
                {
                    if (p.BrojRadneKnjizice == i.Profesor.BrojRadneKnjizice)
                    {
                        ispis += string.Format("\n\t\t{0}", i.DajPodatke());
                    }
                }
            }
            return ispis;
        }

        public void PolozenIspit(Ispit ispit)
        {
            int brojPolozenihKodProfesora = 0;

            for (int i = 0; i < listaIspita.Count; i++)
            {
                if (listaIspita[i].Profesor.BrojRadneKnjizice == ispit.Profesor.BrojRadneKnjizice)
                {
                    brojPolozenihKodProfesora++;
                }
            }

            if (ispit.Ocena > 5 && brojPolozenihKodProfesora <= 2)
            {
                for (int i = 0; i < listaIspita.Count; i++)
                {
                    if (listaIspita[i].Predmet == ispit.Predmet && listaIspita[i].Profesor == ispit.Profesor && listaIspita[i].Ocena == ispit.Ocena)
                    {
                        listaIspita[i] = ispit;
                    }
                    else
                    {
                        listaIspita.Add(ispit);
                    }
                }
            }
            if(ispit.Ocena == 10)
            {
                if(PovecajBrojDesetki != null)
                {
                    PovecajBrojDesetki();
                }
            }
        }

        public static event Delegat PovecajBrojDesetki;
    }
}

using Biblioteka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konzola
{
    public class Fakultet
    {
        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        private List<Student> listaStudenata;

        public List<Student> ListaStudenata
        {
            get { return listaStudenata; }
            set { listaStudenata = value; }
        }

        private int brojDesetki;

        public int BrojDesetki
        {
            get { return brojDesetki; }
            set { brojDesetki = value; }
        }

        public Fakultet(string naziv)
        {
            this.naziv = naziv;
            this.listaStudenata = new List<Student>();
            this.brojDesetki = 0;
        }

        public Ispit UcitajIspit()
        {
            Console.Write("Unesite naziv predmeta:");
            string predmet = Console.ReadLine();

            Console.Write("Unesite ime profesora:");
            String ime = Console.ReadLine();

            Console.Write("Unesite prezime profesora:");
            string prezime = Console.ReadLine();

            Console.Write("Unesite broj radne knjizice profesora:");
            string brojRadneKnjizice = Console.ReadLine();

            Console.Write("Unesite datum polaganja:");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            bool pogresanUnos = true;
            int ocena = 0;

            while (pogresanUnos)
            {
                Console.Write("Unesite ocenu:");
                string ocenaString = Console.ReadLine();

                try
                {
                    ocena = Int32.Parse(ocenaString);
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.Write("Unesite broj za ocenu!");
                }



            }
            Profesor profesor = new Profesor(ime, prezime, Int32.Parse(brojRadneKnjizice));
            Ispit ispit = new Ispit(predmet, profesor, datum, ocena);
            return ispit;
        }

        public Student UcitajStudenta()
        {
            Console.WriteLine("Unesite ime studenta:");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime studenta:");
            string prezime = Console.ReadLine();

            Console.WriteLine("Unesite broj indeksa:");
            string brojIndeksa = Console.ReadLine();

            Console.WriteLine("Unesite status studenta:");
            string status = Console.ReadLine();

            Status odabraniStatus = new Status();

            switch (status)
            {
                case "Redovan": odabraniStatus = Status.Redovan; break;
                case "NaDaljinu": odabraniStatus = Status.NaDaljinu; break;
                case "Diplomirao": odabraniStatus = Status.Diplomirao; break;
                default: odabraniStatus = Status.Redovan; break;
            }

            Student student = new Student(ime, prezime, brojIndeksa, odabraniStatus);

            Console.WriteLine("Unesite broj ispita:");
            int brojIspita = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < brojIspita; i++)
            {
                student.ListaIspita.Add(UcitajIspit());
            }

            return student;
        }

        public void PovecajBrojDesetkiFakulteta()
        {
            brojDesetki++;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Unesite naziv fakulteta:");
            string naziv = Console.ReadLine();

            Fakultet fakultet = new Fakultet(naziv);

            Student s1 = fakultet.UcitajStudenta();
            Student.PovecajBrojDesetki += fakultet.PovecajBrojDesetkiFakulteta();
            fakultet.listaStudenata.Add(s1);
            //fakultet.listaStudenata.Add(fakultet.UcitajStudenta());

            string ispis = string.Format("Naziv fakulteta: {0}\n", fakultet.Naziv);

            for (int i = 0; i < fakultet.listaStudenata.Count; i++)
            {
                ispis += string.Format("\n{0}", fakultet.ListaStudenata[i].DajPodatke());
            }
            ispis += string.Format("\nBroj desetki na fakultetu: {0}.", fakultet.BrojDesetki);

            Console.WriteLine(ispis);
            Console.ReadLine();
        }
    }
}

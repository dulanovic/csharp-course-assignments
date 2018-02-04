using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;

namespace Konzola
{
    public class Fakultet
    {
        private string naziv;
        private List<Student> listaStudenata;
        private int brojDesetki;

        public List<Student> ListaStudenata
        {
            get { return listaStudenata; }
            set { listaStudenata = value; }
        }

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public Fakultet(string naziv)
        {
            this.naziv = naziv;
            this.listaStudenata = new List<Student>();
            this.brojDesetki = 0;
        }

        public Ispit UcitajIspit()
        {
            Console.WriteLine("Unesite predmet:");
            string predmet = Console.ReadLine();

            Console.WriteLine("Unesite ime profesora:");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime profesora:");
            string prezime = Console.ReadLine();

            Console.WriteLine("Unesite broj radne knjizice profesora:");
            string brojRadneKnjizice = Console.ReadLine();

            Console.WriteLine("Unesite datum polaganja:");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            bool pogresanUnos = true;
            int ocena = 0;
            while (pogresanUnos)
            {
                Console.WriteLine("Unesite ocenu(samo cifre):");
                try
                {
                    ocena = Int32.Parse(Console.ReadLine());
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Pogresan unos! Ispravite gresku.");
                }
            }

            return new Ispit(predmet, new Profesor(ime, prezime, brojRadneKnjizice), datum, ocena);
        }

        public Student UcitajStudenta()
        {
            Console.WriteLine("Unesite ime studenta:");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime studenta:");
            string prezime = Console.ReadLine();

            Console.WriteLine("Unesite broj indeksa studenta:");
            string brojIndeksa = Console.ReadLine();

            Console.WriteLine("Unesite status studenta:");
            Status status = (Status)Enum.Parse(typeof(Status), Console.ReadLine());

            Console.WriteLine("Unesite broj ispita koje zelite uneti:");
            int brojIspita = Int32.Parse(Console.ReadLine());

            Student s = new Student(ime, prezime, brojIndeksa, status);
            for (int i = 0; i < brojIspita; i++)
            {
                s.PolozenIspit(UcitajIspit());
            }
            return s;
        }

        public void PovecajBrojDesetkiFakulteta()
        {
            brojDesetki++;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Unesite naziv fakulteta:");
            string naziv = Console.ReadLine();

            Fakultet f = new Fakultet(naziv);
            Student.Obavesti += f.PovecajBrojDesetkiFakulteta;
            f.listaStudenata.Add(f.UcitajStudenta());

            string ispis = string.Format("Naziv fakulteta: {0}", f.naziv);
            foreach (Student s in f.ListaStudenata)
            {
                ispis += string.Format("\n\n{0}", s.DajPodatke());
            }
            ispis += string.Format("\nBroj desetki na fakultetu: {0}", f.brojDesetki);

            Console.WriteLine(ispis);

            Console.ReadKey();
        }
    }
}

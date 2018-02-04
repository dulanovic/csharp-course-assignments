using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;

namespace Konzola
{
    public delegate void Delegat();

    public class Fakultet
    {
        private string naziv;
        private List<Student> listaStudenata;
        private int brojDesetki;
        public static event Delegat Obavesti;

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
            Console.WriteLine("Unesite naziv predmeta: ");
            string naziv = Console.ReadLine();

            Console.WriteLine("Unesite ime profesora: ");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime profesora: ");
            string prezime = Console.ReadLine();

            Console.WriteLine("Unesite broj radne knjizice profesora: ");
            int brojRadneKnjizice = Int32.Parse(Console.ReadLine());

            bool pogresanUnos = true;
            int ocena = 0;

            while (pogresanUnos)
            {
                Console.WriteLine("Unesite ocenu: ");
                try
                {
                    ocena = Int32.Parse(Console.ReadLine());
                    pogresanUnos = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Uneli ste tekst umesto broja, molimo unesite broj!!!");
                }
            }
            Console.WriteLine("Unesite datum polaganja: ");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            Profesor p = new Profesor(ime, prezime, brojRadneKnjizice);
            Ispit i = new Ispit(naziv, p, ocena, datum);
            if (i.Ocena == 10)
            {
                if (Obavesti != null)
                {
                    Obavesti();
                }
            }
            return i;
        }

        public Student UcitajStudenta()
        {
            Console.WriteLine("Unesite ime studenta: ");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime studenta: ");
            string prezime = Console.ReadLine();

            Console.WriteLine("Unesite broj indeksa: ");
            string brojIndeksa = Console.ReadLine();

            Console.WriteLine("Unesite status studenta: ");
            Status status = (Status)Enum.Parse(typeof(Status), Console.ReadLine());

            Console.WriteLine("Unesite broj ispita: ");
            int brojIspita = Int32.Parse(Console.ReadLine());

            Student s = new Student(ime, prezime, brojIndeksa, status);

            for (int i = 0; i < brojIspita; i++)
            {
                s.ListaIspita.Add(UcitajIspit());
            }

            return s;
        }

        public void PovecajBrojDesetkiNaFakultetu()
        {
            brojDesetki++;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Unesite naziv fakulteta: ");
            string naziv = Console.ReadLine();

            Fakultet f = new Fakultet(naziv);
            Fakultet.Obavesti += f.PovecajBrojDesetkiNaFakultetu;

            f.listaStudenata.Add(f.UcitajStudenta());
            //f.listaStudenata.Add(f.UcitajStudenta());

            string ispis = string.Format("Naziv fakulteta: {0}\n", f.naziv);
            foreach (Student s in f.listaStudenata)
            {
                ispis += string.Format("{0}", s.DajPodatke());
            }
            ispis += string.Format("\nBroj desetki na fakultetu: {0}", f.brojDesetki);

            Console.WriteLine(ispis);
            Console.ReadLine();
        }
    }
}

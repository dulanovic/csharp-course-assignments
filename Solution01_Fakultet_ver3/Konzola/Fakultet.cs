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
            string predmet = Console.ReadLine();

            Console.WriteLine("Unesite ime profesora: ");
            string ime = Console.ReadLine();

            Console.WriteLine("Unesite prezime profesora: ");
            string prezime = Console.ReadLine();

            Console.WriteLine("Unesite broj radne knjizice profesora: ");
            int brojRadneKnjizice = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Unesite datum polaganja ispita: ");
            DateTime datum = DateTime.Parse(Console.ReadLine());

            int ocena = 0;
            bool pogresanUnos = true;

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
                    Console.WriteLine("Uneli ste slovo umesto broja, molimo ispravite gresku!");
                }
            }

            Profesor p = new Profesor(ime, prezime, brojRadneKnjizice);
            Ispit i = new Ispit(predmet, p, datum, ocena);

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

            Console.WriteLine("Unesite broj ispita koje zelite uneti: ");
            int brojIspita = Int32.Parse(Console.ReadLine());

            Student s = new Student(ime, prezime, brojIndeksa, status);
            for (int i = 0; i < brojIspita; i++)
            {
                s.PolozenIspit(UcitajIspit());
            }

            return s;
        }

        public void PovecajBrojDesetkiNaFakultetu()
        {
            brojDesetki++;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Unesite naziv fakuteta: ");
            string naziv = Console.ReadLine();

            Fakultet f = new Fakultet(naziv);
            Fakultet.Obavesti += f.PovecajBrojDesetkiNaFakultetu;
            f.ListaStudenata.Add(f.UcitajStudenta());
            //f.ListaStudenata.Add(f.UcitajStudenta());

            string ispis = string.Format("Naziv fakulteta: {0}\n\n", f.naziv);
            foreach (Student s in f.ListaStudenata)
            {
                ispis += s.DajPodatke();
            }
            ispis += string.Format("\nBroj desetki na fakultetu: {0}", f.brojDesetki);

            Console.WriteLine(ispis);
            Console.ReadLine();
        }
    }
}

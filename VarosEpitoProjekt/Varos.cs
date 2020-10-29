using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarosEpitoProjekt
{
    class Varos
    {
        public string Nev { get; }

        private int lakosok;
        public int Lakosok
        {
            get { return lakosok; }
            set
            {
                if (lakosok > value)
                    Console.WriteLine("Hiba! A lakosok száma nem csökkenhet!");
                else if (lakosok > MaxLakosok)
                    Console.WriteLine("Hiba! A lakosok száma nem lépheti át a maximumot!");
                else
                    lakosok = value;
            }
        }
        private int hazak;
        public int Hazak
        {
            get => hazak;
            set
            {
                if (MaxLakosok < lakosok)
                    Console.WriteLine("Hiba! A maximum lakosok száma nem lehet kisebb, mint a lakosok!");
                else
                    hazak = value;
            }
        }
        public int Uzletek { get; set; }
        public int MaxLakosok => Hazak * 6;
        public double Alapterulet => Hazak * 110 + Uzletek * 85.5;

        public Varos(string nev, int meret)
        {
            Nev = nev;
            if (meret == 1)
            {
                Hazak = 150;
                Uzletek = 20;
            }
            else if (meret == 2)
            {
                Hazak = 300;
                Uzletek = 45;
            }
            else if (meret == 3)
            {
                Hazak = 450;
                Uzletek = 50;
            }
            Lakosok = MaxLakosok / 2;
        }

        public void uzletetEpit(int db)
        {
            if (db <= 0)
                throw new ArgumentException("Csak pozitív számú üzletet lehet építeni!");
            else
            {
                int maxUzlet = (int)Math.Floor((double)(Lakosok / 20));
                if (maxUzlet > (Uzletek + db))
                    Uzletek += db;
                else
                {
                    Uzletek = maxUzlet;
                    Console.WriteLine("Több üzletet már nem építhet!");
                }
            }
        }

        public int compareTo(Varos masik)
        {
            if (this.Alapterulet == masik.Alapterulet)
                return 0;
            else if (this.Alapterulet < masik.Alapterulet)
                return -1;
            else
                return 1;
        }

        public override string ToString()
        {
            return $"{Nev,-10} - Lakosok: {Lakosok,-4}/{MaxLakosok,4} - Üzletek: {Uzletek,2} - Alapterület:" +
                $" {Alapterulet,7} m²";
        }
    }
}

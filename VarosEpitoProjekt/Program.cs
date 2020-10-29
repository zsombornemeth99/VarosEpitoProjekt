using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VarosEpitoProjekt
{
    class Program
    {
        static int menuPont;
        static string jatekosVarosNev;
        static int jatekosVarosMeret;
        static Varos jatekosVarosa;
        static int menu()
        {

            do
            {
                Console.Clear();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\tMenü");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n\t1 -  Lakosokat betelepíteni");
                Console.WriteLine("\t2 - Házat építeni");
                Console.WriteLine("\t3 - Üzletet építeni");
                Console.WriteLine("\t4 - Kilépés");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\n\tKérem válasszon menüpontot: ");
                Console.ResetColor();
                try
                {
                    while (!int.TryParse(Console.ReadLine(), out menuPont) || menuPont < 1 || menuPont > 4)
                    {

                        MessageBox.Show("Hiba, nem létező menüpontot választott!");
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine();
            }
            while (menuPont < 1 || menuPont > 4);

            return menuPont;
        }

        static void Main(string[] args)
        {
            var varosLista = new List<Varos>();

            varosLista.Add(new Varos("Budapest", 2));
            varosLista.Add(new Varos("Debrecen", 1));
            varosLista.Add(new Varos("New York", 3));
            try
            {
                StreamReader r = new StreamReader("varosok.csv", Encoding.UTF8);
                while (!r.EndOfStream)
                {
                    string[] st = r.ReadLine().Split(';');
                    varosLista.Add(new Varos(st[0], int.Parse(st[1])));
                }
                varosLista.Sort((x, y) => x.Alapterulet.CompareTo(y.Alapterulet));
                //foreach (var item in varosLista)
                //{
                //    Console.WriteLine(item);
                //}
                r.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("A fájl nem található!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n\nKérem adja meg milyen néven szeretné létrehozni városát: ");
            Console.ResetColor();
            jatekosVarosNev = Console.ReadLine();
            jatekosVarosMeret = 0;
            bool bevitel;
            do
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n\nKérem adja meg mekkora mérettel szeretné létrehozni városát(1,2 v. 3): ");
                    Console.ResetColor();
                    bevitel = int.TryParse(Console.ReadLine(), out jatekosVarosMeret);
                    while (!bevitel)
                    {
                        MessageBox.Show("Hiba, érvénytelen bevitel!");
                        break;
                    }
                    if (jatekosVarosMeret != 1 && jatekosVarosMeret != 2 && jatekosVarosMeret != 3 && bevitel)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nHiba! Az érték 1, 2 vagy 3 lehet csak! Kérem adja meg újra!" +
                            "\n\nNyomjon egy ENTER-t a folytatáshoz!");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    Console.Clear();                 
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            while (jatekosVarosMeret != 1 && jatekosVarosMeret != 2 && jatekosVarosMeret != 3);

            jatekosVarosa = new Varos(jatekosVarosNev, jatekosVarosMeret);



            Console.ReadLine();
        }
    }
}

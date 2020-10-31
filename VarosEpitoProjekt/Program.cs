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
                Console.WriteLine("\n\t1 - Lakosokat betelepíteni");
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

            int korszamlalo = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("\nA többi város: \n");
                for (int i = 0; i < varosLista.Count; i++)
                {
                    Console.WriteLine("\t" + (i + 1) + ". " + varosLista[i]);
                    Console.WriteLine();
                }
                Console.WriteLine("Az Ön városa:");
                Console.WriteLine($"\n\t{varosLista.Count + 1}. {jatekosVarosa}");
                Console.WriteLine("\nNyomjon egy ENTER-t a menü megjelenítéséhez!");
                if (korszamlalo == 2)
                {
                    Console.Clear();
                    Console.WriteLine("\n\tA játék végéhez értünk!");
                    varosLista.Add(jatekosVarosa);
                    //terület egyenlőség esetén ABC sorrendben határozza meg a győztest
                    var eredmeny = varosLista.OrderBy(x => x.Alapterulet).ThenBy(y => y.Nev).ToList();
                    foreach (var item in eredmeny)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("A győztes(akinek a legnagyobb a területe): "
                        + eredmeny[eredmeny.Count-1]);

                    var yesNO = MessageBox.Show("Szeretne új játékot kezdeni?", "A játék vége!", MessageBoxButtons.YesNo);
                    if (yesNO == DialogResult.Yes)
                    {
                        Application.Restart();
                        Environment.Exit(0);
                    }
                    else
                        Environment.Exit(0);
                }
                Console.ReadKey();
                int menuPont;
                do
                {
                    menuPont = menu();

                    switch (menuPont)
                    {
                        case 1:
                            int lakos = 0;
                            bool beker = false;
                            do
                            {
                                try
                                {
                                    Console.Write("\tMennyi lakost szeretne betelepítemi?");
                                    beker = int.TryParse(Console.ReadLine(), out lakos);
                                    while (!beker)
                                    {
                                        MessageBox.Show("Hiba, csak számot adhat meg!");
                                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                                        Console.Write(new string(' ', Console.BufferWidth));
                                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                                        break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            } 
                            while (!beker);
                            jatekosVarosa.Lakosok += lakos;
                            break;
                        case 2:
                            Random r = new Random();

                            jatekosVarosa.Hazak += r.Next(10, 20);

                            break;
                        case 3:
                            jatekosVarosa.uzletetEpit(10);
                            break;
                        case 4:
                            MessageBox.Show("Köszönjük, hogy részt vett a játékban");
                            Environment.Exit(0); break;
                    }
                }
                while (menuPont == 4);
                korszamlalo++;
                Console.WriteLine("\n\tNyomjon egy ENTER-t a folytatáshoz!");
                Console.ReadKey();
                if (korszamlalo % 2 == 0)
                {
                    foreach (var item in varosLista)
                    {
                        item.Hazak += 15;
                        item.Lakosok += 10;
                    }
                }
                else
                {
                    foreach (var item in varosLista)
                    {
                        item.uzletetEpit(10);
                        item.Lakosok += 20;
                    }
                }               
            }
            while (menuPont != 4);

            Console.ReadLine();
        }
    }
}

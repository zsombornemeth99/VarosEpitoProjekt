using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarosEpitoProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            var varosLista = new List<Varos>();

            varosLista.Add(new Varos("Budapest", 2));
            varosLista.Add(new Varos("Debrecen", 1));
            varosLista.Add(new Varos("New York", 3));

            StreamReader r = new StreamReader("varosok.csv", Encoding.UTF8);
            while (!r.EndOfStream)
            {
                string[] st = r.ReadLine().Split(';');
                varosLista.Add(new Varos(st[0], int.Parse(st[1])));
            }
            varosLista.Sort((x,y) => x.Alapterulet.CompareTo(y.Alapterulet));
            foreach (var item in varosLista)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}

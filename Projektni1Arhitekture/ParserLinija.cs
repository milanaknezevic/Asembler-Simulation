using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projektni1Arhitekture
{
    internal class ParserLinija
    {
        private string linija;
        public Instrukcije komande = new Instrukcije();
        private string _fileName;
        private Dictionary<string, int> dLabela = new Dictionary<string, int>();
        private Dictionary<int, string> dLinija = new Dictionary<int, string>();
        private int linijaZaIzvrsiti;

        public ParserLinija()
        {
            linija = null;
            linijaZaIzvrsiti = 0;
        }

        public static bool isMemoryAdress(string argument)
        {
            bool correct = true;
            if (argument.Length < 3 || argument.Length > 10)
            {
                correct = false;
                return correct;
            }
            else
            {
                if (argument[0] == '0' && argument[1] == 'x')
                {
                    for (int i = 2; i < argument.Length; i++)
                    {
                        if (!((argument[i] >= '0' && argument[i] <= '9') || (argument[i] >= 'a' && argument[i] <= 'f')))
                        {
                            correct = false;
                            return correct;
                        }
                    }
                    return correct;
                }
                else
                {
                    correct = false;
                    return correct;
                }
            }
        }

        public static bool isNumber(string argument)
        {
            bool correct = true;

            foreach (char slovo in argument)
            {
                if (slovo > '9' || slovo < '0')
                    correct = false;
            }

            return correct;
        }

        public static bool isLabela(string rijec)
        {
            rijec = rijec.Trim();
            if (rijec[rijec.Length - 1] == ':')
            {
                return true;
            }
            else return false;
        }

        public string Write()
        {
            Console.WriteLine("Unesite naziv skripte (bez ekstenzije):");
            string filename = Console.ReadLine(); filename = filename + ".txt";
            string linija = null;
            using (StreamWriter file = new StreamWriter(Directory.GetCurrentDirectory() + @"\" + filename))
            {
                Console.WriteLine($"Pisanje u datoteku: {filename}");
                do
                {
                    linija = Console.ReadLine();
                    file.WriteLine(linija);
                }
                while (linija.Length > 0);
            }//KreirajDictionaryLinija(filename);
            this._fileName = filename;
            return filename;
        }

        private string[] CommandSplitter(string linija)
        {
            linija = linija.ToLower();
            string komanda = linija.Split(' ').First();
            komanda = komanda.Trim();
            List<string> tmp = new List<string>();
            tmp.Add(komanda);
            string argumenti = string.Join(" ", linija.Split(' ').Skip(1)); //izbacuje samo prvu rijec iz linije (komandu)
            argumenti = argumenti.Trim();
            string[] nizArgumenata = argumenti.Split(',');
            for (int i = 0; i < nizArgumenata.Length; i++)
            {
                nizArgumenata[i] = nizArgumenata[i].Trim();
                if (nizArgumenata[i].Length != 0)
                    tmp.Add(nizArgumenata[i]);
            }

            return tmp.ToArray();
        }

        private void Je(string labela)
        {
            if (komande.flag == 0)//skace ako je flag 1
            { //Console.Clear();//Console.WriteLine($"Broj linije: {linijaZaIzvrsiti}");//komande.IspisStanjaMemorije();//Console.ReadLine()//Console.Clear();
                linijaZaIzvrsiti = dLabela[labela];
            }
            else return;
        } //skace ako je jednako ako je flag 0

        private void Jne(string labela)//ako  nije ejdnako
        {
            if (komande.flag != 0)
            {
                linijaZaIzvrsiti = dLabela[labela];
            }
            else return;
        }

        private void Jge(string labela)//vece ili jednako
        {
            if (komande.flag >= 0)
            {
                linijaZaIzvrsiti = dLabela[labela];
            }
            else return;
        }

        private void Jl(string labela)
        {
            if (komande.flag < 0)
            {
                linijaZaIzvrsiti = dLabela[labela];
            }
            else return;
        }

        private void IspisiFajl(string dataFile)
        {
            try
            {
                string line;
                int brojac = 1;
                using (StreamReader sr = new StreamReader(dataFile))
                {
                    Console.WriteLine("============ ISPIS SKRIPTE ============");
                    while (!sr.EndOfStream)
                    {
                        Console.Write($"{brojac}:\t");
                        line = sr.ReadLine();
                        Console.WriteLine(line);
                        brojac++;
                    }
                    Console.WriteLine("=======================================");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void FindLabels(string dataFile)
        {
            Dictionary<string, int> tmp = new Dictionary<string, int>();
            using (StreamReader sr = new StreamReader(dataFile))
            {
                int brojac = 0;
                string line;
                while (!sr.EndOfStream)
                {
                    brojac++;
                    line = sr.ReadLine();
                    line = line.Trim();
                    if (line.Length > 0)
                    {
                        if (isLabela(line))
                        {
                            if (!tmp.ContainsKey(line))
                            {
                                line = line.Split(':').First();
                                tmp.Add(line, brojac);
                            }
                        }
                    }
                    continue;
                }
            }
            dLabela = tmp;
        }

        private void KreirajDictionaryLinija(string dataFile)
        {
            try
            {
                StreamReader sr = new StreamReader(dataFile);
                int brojac = 0;
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    dLinija.Add(brojac++, line);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
          
        }
        public void ReadBezSingleStep(string dataFile)
        {
            try
            {
                KreirajDictionaryLinija(dataFile);
                _fileName = dataFile;
                string line;
                IspisiFajl(dataFile);
                Console.WriteLine("Linije prekidnih tacaka (razdvajati sa space-om):");
                Console.Write("-> ");
                string breakpoints = Console.ReadLine();
                breakpoints = breakpoints.Trim();
                string[] tmp = breakpoints.Split(' ');
                List<int> breakpoint = new List<int>(); //lista int-ova koji predstavljaju broj linije sa breakpoint
                foreach (string clan in tmp)
                {
                    if (clan.Length > 0)
                    {
                        int n = 0;
                        if (int.TryParse(clan, out n))
                            breakpoint.Add(n);
                    }
                }
                //int brojac = 0;
                while (linijaZaIzvrsiti <= (dLinija.Count - 1))//citanje iz dictionary linija
                {
                    line = dLinija[linijaZaIzvrsiti];
                    line = line.Trim();
                    linijaZaIzvrsiti++;
                    if (line.Length == 0 && breakpoint.Contains(linijaZaIzvrsiti))//za prazne linije
                    {
                        Console.Clear();
                        Console.WriteLine($"Stanje do linije {linijaZaIzvrsiti}:\t");
                        komande.IspisStanjaMemorije();
                        Console.ReadLine();
                        continue;
                    }
                    else if (line.Length == 0)
                    { continue; }
                    try
                    {
                        if (breakpoint.Contains(linijaZaIzvrsiti))//za linije koje imaju komande
                        {
                            Console.Clear();
                            Console.WriteLine($"Stanje do linije {linijaZaIzvrsiti}:\t");
                            komande.IspisStanjaMemorije();
                            Console.ReadLine();
                            Console.Clear();
                        }
                        ProvjeriSintaksu(line);
                        Izvrsi(line);
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Greska u sintaksi u liniji: {linijaZaIzvrsiti}");
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                        break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Pregled krajnjeg stanja memorije:");
                Console.ResetColor();
                komande.IspisStanjaMemorije();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fajl nije moguce procitati!");
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        public void ReadSaSingleStep(string dataFile)
        {
            _fileName = dataFile;
            try
            {
                KreirajDictionaryLinija(dataFile);
                string line;
                //int brojac = 0;
                while (linijaZaIzvrsiti <= (dLinija.Count - 1))
                {
                    line = dLinija[linijaZaIzvrsiti];
                    line = line.Trim();
                    linijaZaIzvrsiti++;
                    if (line.Length == 0)
                        continue;
                    try
                    {
                        ProvjeriSintaksu(line);
                        Izvrsi(line);
                        Console.WriteLine($"Broj linije: {linijaZaIzvrsiti}\t");
                        komande.IspisStanjaMemorije();
                        Console.ReadLine();
                        Console.Clear();
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Greska u sintaksi u liniji: {linijaZaIzvrsiti}");
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                        break;//da prekine izvrsavanje cim naidje na jendu gresku
                    }
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Pregled krajnjeg stanja memorije:");
                Console.ResetColor();
                komande.IspisStanjaMemorije();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fajl nije moguce procitati!");
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        private void Izvrsi(string linija)
        {
            linija = linija.Trim();
            string[] nizArgumenata = CommandSplitter(linija);
            if (nizArgumenata[0] == "mov")
                komande.Mov(nizArgumenata[1], nizArgumenata[2]);
            else if (nizArgumenata[0] == "add")
                komande.Add(nizArgumenata[1], nizArgumenata[2]);
            else if (nizArgumenata[0] == "sub")
                komande.Sub(nizArgumenata[1], nizArgumenata[2]);
            else if (nizArgumenata[0] == "and")
                komande.And(nizArgumenata[1], nizArgumenata[2]);
            else if (nizArgumenata[0] == "or")
                komande.Or(nizArgumenata[1], nizArgumenata[2]);
            else if (nizArgumenata[0] == "not")
                komande.Not(nizArgumenata[1]);
            else if (nizArgumenata[0] == "load")
                komande.Load();
            else if (nizArgumenata[0] == "store")
                komande.Store();
            else if (nizArgumenata[0] == "cmp")
                komande.Cmp(nizArgumenata[1], nizArgumenata[2]);
            else if (nizArgumenata[0] == "je")
                Je(nizArgumenata[1]);
            else if (nizArgumenata[0] == "jne")
                Jne(nizArgumenata[1]);
            else if (nizArgumenata[0] == "jge")
                Jge(nizArgumenata[1]);
            else if (nizArgumenata[0] == "jl")
                Jl(nizArgumenata[1]);
            else return;
        }

        private void ProvjeriSintaksu(string linija)
        {
            linija = linija.Trim();
            string[] nizArgumenata = CommandSplitter(linija); // pretvara sve u lower i razdvaja sve argumente
            if (nizArgumenata.Length > 3)
                throw new Exception("Previse argumenata!");
            else if (nizArgumenata.Length == 3)
            {
                if (nizArgumenata[0] == "mov" || nizArgumenata[0] == "add" || nizArgumenata[0] == "sub" || nizArgumenata[0] == "and" || nizArgumenata[0] == "or" || nizArgumenata[0] == "cmp")
                {
                    if (komande.dReg.ContainsKey(nizArgumenata[1]) && komande.dReg.ContainsKey(nizArgumenata[2]))
                        return;
                    else if (komande.dReg.ContainsKey(nizArgumenata[1]) && isMemoryAdress(nizArgumenata[2]))
                    {
                        return;
                    }
                    else if (komande.dReg.ContainsKey(nizArgumenata[2]) && isMemoryAdress(nizArgumenata[1]))
                    {
                        return;
                    }
                    else if (komande.dReg.ContainsKey(nizArgumenata[1]) && isNumber(nizArgumenata[2]))
                    {
                        return;
                    }
                    else if (isMemoryAdress(nizArgumenata[1]) && isNumber(nizArgumenata[2]))
                    {
                        return;
                    }
                    else if (nizArgumenata[0]=="cmp")
                    {
                        if (isNumber(nizArgumenata[1]) && isMemoryAdress(nizArgumenata[2]))
                            return;
                        else if (isNumber(nizArgumenata[1]) && komande.dReg.ContainsKey(nizArgumenata[2]))
                            return;
                    }
                    else
                        throw new Exception($"Nemoguca kombinacija operanada {nizArgumenata[1]} i {nizArgumenata[2]}");
                }
                else
                {
                    if (!komande.instrukcija.Contains(nizArgumenata[0]))
                        throw new Exception($"Nepostojeca komanda: {nizArgumenata[0]}");
                    else if (nizArgumenata[0] == "jge" || nizArgumenata[0] == "jl" || nizArgumenata[0] == "jne" || nizArgumenata[0] == "je" || nizArgumenata[0] == "store" || nizArgumenata[0] == "load")
                        throw new Exception($"{nizArgumenata[0].ToUpper()} nije binarna instrukcija!");
                    else
                        throw new Exception($"Nepravilno iskoristena instrukcija: {nizArgumenata[0].ToUpper()}");
                }
            }
            else if (nizArgumenata.Length == 2)
            {
                if (nizArgumenata[0] == "je" || nizArgumenata[0] == "jne" || nizArgumenata[0] == "jge" || nizArgumenata[0] == "jl")
                {
                    FindLabels(_fileName);

                    if (dLabela.ContainsKey(nizArgumenata[1]))
                    {
                        return;
                    }
                    else throw new Exception($"Nepoznata labela: {nizArgumenata[1]}");
                }
                else if (nizArgumenata[0] != "not")
                    throw new Exception($"{nizArgumenata[0].ToUpper()} nije unarna instrukcija!");
                else if (!komande.dReg.ContainsKey(nizArgumenata[1]))
                {
                    throw new Exception($"NOT komanda nije moguca sa argumentom koji nije registar");
                }
                else
                    return;
            }
            else if (nizArgumenata.Length == 1)
            {
                if (nizArgumenata[0] == "store" || nizArgumenata[0] == "load")
                {
                    return;
                }
                else if (isLabela(nizArgumenata[0]))
                { return; }
                else
                    throw new Exception($"Neodgovarajuci broj argumenata za {nizArgumenata[0].ToUpper()}"); ;
            }
            else
            {
                throw new Exception($"Nepravilna linija");
            }
        }
    }
}
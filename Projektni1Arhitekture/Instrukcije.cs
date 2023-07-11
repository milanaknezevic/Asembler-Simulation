using System;
using System.Collections.Generic;

namespace Projektni1Arhitekture
{
    internal class Instrukcije
    {
        private const int velicinaAlokacije = 10; //velicina upotrebljive memorije
        public List<string> instrukcija = new List<string>();

        // public Registri registri = new Registri();
        public Memorija[] memorija = new Memorija[velicinaAlokacije];

        public Dictionary<string, long> dReg = new Dictionary<string, long>();
        public Dictionary<string, long> dMem = new Dictionary<string, long>();
        public long flag;

        public Instrukcije()
        {
            instrukcija.Add("mov");
            instrukcija.Add("add");
            instrukcija.Add("sub");
            instrukcija.Add("and");
            instrukcija.Add("or");
            instrukcija.Add("not");
            instrukcija.Add("load");
            instrukcija.Add("store");
            instrukcija.Add("cmp");
            instrukcija.Add("je");
            instrukcija.Add("jne");
            instrukcija.Add("jge");
            instrukcija.Add("jl");

            for (int i = 0; i < velicinaAlokacije; i++)
            {
                memorija[i] = new Memorija();
                dMem.Add(memorija[i].adresa, memorija[i].podaci);
            }

            dReg.Add("rax", 0);
            dReg.Add("rcx", 0);
            dReg.Add("rdi", 0);
            dReg.Add("rsi", 0);
        }

        public void IspisStanjaMemorije()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("============= MEMORIJA =============");
            Console.ResetColor();
            foreach (var item in dMem)
            {
                Console.WriteLine($"Adress: {item.Key}\tValue: {item.Value}\tSize: 64b");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("============= REGISTRI =============");
            Console.ResetColor();
            foreach (var item in dReg)
            {
                Console.WriteLine($"Adress: {item.Key.ToUpper()}\tValue: {item.Value}\tSize: 64b");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine($"Flag: {flag}");
        }

        public void Mov(string argument1, string argument2)
        {
            if (dReg.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                dReg[argument1] = dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && dMem.ContainsKey(argument2))
            {
                dReg[argument1] = dMem[argument2];
            }
            else if (dMem.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                dMem[argument1] = dReg[argument2];
            }
            else if (dMem.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                dMem[argument1] = long.Parse(argument2);//pretvara string u broj
            }
            else
            {
                dReg[argument1] = long.Parse(argument2);
            }
        }

        public void Add(string argument1, string argument2)
        {
            if (dReg.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                dReg[argument1] += dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && dMem.ContainsKey(argument2))
            {
                dReg[argument1] += dMem[argument2];
            }
            else if (dMem.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                dMem[argument1] += dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                dReg[argument1] += long.Parse(argument2);//pretvara string u broj
            }
            else if (dMem.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                dMem[argument1] += long.Parse(argument2);//pretvara string u broj
            }
            else return;
        }

        public void Sub(string argument1, string argument2)
        {
            if (dReg.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                dReg[argument1] -= dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && dMem.ContainsKey(argument2))
            {
                dReg[argument1] -= dMem[argument2];
            }
            else if (dMem.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                dMem[argument1] -= dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                dReg[argument1] -= long.Parse(argument2);//pretvara string u broj
            }
            else if (dMem.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                dMem[argument1] -= long.Parse(argument2);//pretvara string u broj
            }
            else return;
        }

        public void And(string argument1, string argument2)
        {
            if (dReg.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                dReg[argument1] &= dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && dMem.ContainsKey(argument2))
            {
                dReg[argument1] &= dMem[argument2];
            }
            else if (dMem.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                dMem[argument1] &= dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                dReg[argument1] &= long.Parse(argument2);//pretvara string u broj
            }
            else if (dMem.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                dMem[argument1] &= long.Parse(argument2);//pretvara string u broj
            }
            else return;
        }

        public void Or(string argument1, string argument2)
        {
            if (dReg.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                dReg[argument1] |= dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && dMem.ContainsKey(argument2))
            {
                dReg[argument1] |= dMem[argument2];
            }
            else if (dMem.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                dMem[argument1] |= dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                dReg[argument1] |= long.Parse(argument2);//pretvara string u broj
            }
            else if (dMem.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                dMem[argument1] |= long.Parse(argument2);//pretvara string u broj
            }
            else return;
        }

        public void Not(string argument)
        {
            if (dReg.ContainsKey(argument))
            {
                dReg[argument] = ~dReg[argument];
            }
            else return;
        }

        public void Load()
        {
            long i = dReg["rsi"];//uzima vrjednost iz rama na koju rsi pokazuje i smjesta u rax
            if (i >= 0 && i < velicinaAlokacije)
            {
                string tmp = "0x" + i.ToString();
                dReg["rax"] = dMem[tmp];
                dReg["rsi"]++;
            }
            else throw new OutOfMemoryException("RSI je van domena alocirane memorije!");
        }

        public void Store()//uzima vrjednost iz rax i smjesta na mjesto u mem na  koje pokazuje rdi
        {
            long i = dReg["rdi"];

            if (i >= 0 && i < velicinaAlokacije)
            {
                string tmp = "0x" + i.ToString();
                dMem[tmp] = dReg["rax"];
                dReg["rdi"]++;
            }
            else throw new OutOfMemoryException("RDI je van domena alocirane memorije!");
        }

        public void Cmp(string argument1, string argument2)
        {
            long tmp = 0;
            if (dReg.ContainsKey(argument1) && dReg.ContainsKey(argument2))//zero flag 1 ako je rez jednak 0
            {
                tmp = dReg[argument1] - dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && dMem.ContainsKey(argument2))
            {
                tmp = dReg[argument1] - dMem[argument2];
            }
            else if (dMem.ContainsKey(argument1) && dReg.ContainsKey(argument2))
            {
                tmp = dMem[argument1] - dReg[argument2];
            }
            else if (dReg.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                tmp = dReg[argument1] - long.Parse(argument2);//pretvara string u broj
            }
            else if (dMem.ContainsKey(argument1) && ParserLinija.isNumber(argument2))
            {
                tmp = dMem[argument1] - long.Parse(argument2);//pretvara string u broj
            }
            else if (ParserLinija.isNumber(argument1) && dMem.ContainsKey(argument2))
            {
                tmp = long.Parse(argument1)- dMem[argument2];//pretvara string u broj
            }
            else if (ParserLinija.isNumber(argument1) && dReg.ContainsKey(argument2))
            {
                tmp = long.Parse(argument1) - dReg[argument2];//pretvara string u broj
            }

            if (tmp > 0)
            {
                flag = 1;//prvi arg je veci
            }
            else if (tmp < 0)
            {
                flag = -1;
            }
            else { flag = 0; }

            return;
        }//postavlja flagove
    }
}
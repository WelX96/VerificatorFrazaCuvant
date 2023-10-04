using System.Text.RegularExpressions;
internal class Program
{
    private static void Main(string[] args)
    {
        RunTest();
      
        ConsoleKeyInfo cki;
        Console.TreatControlCAsInput = true;
        Console.WriteLine("Apasa orice tasta pentru a repeta testul cu alti parametrii");
        Console.WriteLine("Apasa ESCPAE ( ESC) pentru a inchide aplicatia \n");
        cki = Console.ReadKey();
        while (cki.Key != ConsoleKey.Escape)
        {
            Console.Clear();
            RunTest();
            Console.WriteLine("Apasa orice tasta pentru a repeta testul cu alti parametrii");
            Console.WriteLine("Apasa ESCAPE ( ESC) pentru a inchide aplicatia \n");
            cki = Console.ReadKey();
        }

        Environment.Exit(0);
           
    }

    static (bool, string) VerifInput(string input,int tipInput)  //return un true daca e cf conditiilor, si apoi un string conditiile, sa stim afisa
    {
        string conditii = "doar litere, fara diacritice";

        if (tipInput == 1)
        {
            if (Regex.Matches(input, @"[a-zA-Z\s]").Count == input.Length)
            {
                return (true, conditii);
            }
            else
            {
                return (false, conditii);
            }
        }
        else
        {
            if (Regex.Matches(input, @"[a-zA-Z]").Count == input.Length)
            {
                return (true, conditii);
            }
            else
            {
                return (false, conditii);
            }
        }
        
    }
    public static (string, string) InputParametri()  //return (Item1 cuvant,Item2 fraza)
    {
         string  fraza = "";
         string  cuvant = "";
        Console.WriteLine("Introduce-ti fraza: ");
        fraza = Console.ReadLine();
        while (!VerifInput(fraza,1).Item1)
        {
            Console.WriteLine("Va rog introduceti o fraza care contine " + VerifInput(fraza,1).Item2 + " si care nu contine spatii!");
            fraza = Console.ReadLine();
        }

        Console.WriteLine("Introduce-ti cuvantul:");
        cuvant = Console.ReadLine();
        while (!VerifInput(cuvant,2).Item1)
        {
            Console.WriteLine("Va rog introduceti un cuvant care contine " + VerifInput(cuvant,2).Item2 + "!");
            cuvant = Console.ReadLine();
        }

        return (cuvant.ToUpper(), fraza.ToUpper());
    }

    public static string TipTest()  //return ('a' sau 'b')
    {
        string TipTest(string tipTest) => Regex.Match(tipTest, "[abAB]").Success==true ? tipTest:"error"  ;
        string tipTest;
        Console.WriteLine("Tipuri de test:");
        Console.WriteLine("a) Determina daca un cuvant se poate forma luand prima litera din fiecare cuvant din fraza in ordine");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Exemplu:");
        Console.ResetColor();
        Console.WriteLine("\t" + @"pentru fraza: ”Programarea este interesanta.” și cuvantul ”pei”");
        Console.WriteLine("\t" + @"rezultatul este da (”pei” este format din prima litera a fiecarui cuvant din fraza)");

        Console.WriteLine("a) Determina daca cuvantul dat se poate forma luand inceputul (una, doua sau trei litere) de la fiecare cuvant din fraza in ordine");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Exemplu:");
        Console.ResetColor();
        Console.WriteLine("\t" + @" pentru fraza: ”Cocosul cantă cucurigu dimineata.” și cuvântul ”coccudim”");
        Console.WriteLine("\t" + @" raspunsul este da ( [CO]cosul [C]anta [CU]curigu [DIM]ineata)" + "\n");
        Console.WriteLine("\nIntroduceti litera aferenta tipului de test: ");
        tipTest=Console.ReadLine();

        while (TipTest(tipTest) =="error")
        {
            Console.WriteLine("Introduceti doar a sau b pentru a putea continua: ");
            tipTest = Console.ReadLine();
        }
        

        return tipTest.ToLower();

    }

    public static string TestA ((string, string) parametrii)  //return string mesaj pozitiv sau negativ al testului
    {
        int contorCuvant = 0;
        string[] fraza = parametrii.Item2.Split(' ');
        for (int i = 0; i < parametrii.Item1.Length; i++)
        {
            if (parametrii.Item1[i] == fraza[i][0])
            {
                contorCuvant++;
            } ;
        }
        if (contorCuvant== parametrii.Item1.Length) 
        {
            return "DA";
        }
        else
        {
            return "NU";
        }
        
    }

    public static string TestB((string, string) parametrii)  //return string mesaj pozitiv sau negativ al testului
    {
        string[] fraza = parametrii.Item2.Split(' ');
        int esteCorect = 1;
        int i = 0;
        int nrCuvantDinFraza = 0;
        int nrLiteraDinCuvantFraza = 0;
        int ultimulCuvantBun = -1;
        while (esteCorect==1 && i< parametrii.Item1.Length)
        {
            if (nrLiteraDinCuvantFraza == 3)
            {
                nrLiteraDinCuvantFraza = 0;
            }

            if (parametrii.Item1[i] == fraza[nrCuvantDinFraza][nrLiteraDinCuvantFraza])
            {
                nrLiteraDinCuvantFraza++;
            }
            else if (nrCuvantDinFraza == 0 || nrCuvantDinFraza==fraza.Length-1)
                {
                    esteCorect = 0;
                }
            else if (ultimulCuvantBun==nrCuvantDinFraza)
                {
                ultimulCuvantBun++;
                nrLiteraDinCuvantFraza = 0;
                }
            else
            {
                esteCorect = 0;
            }
            i++;
                    }

        if (esteCorect == 1)
        {
            return "DA";
        }
        else
        {
            return "NU";
        }
    }

    public static void RunTest()
    {
        (string, string) parametrii = InputParametri();
        if (TipTest() == "a")
        {
            Console.WriteLine(TestA(parametrii));
        }
        else
        {
            Console.WriteLine(TestB(parametrii));
        }
    }
}

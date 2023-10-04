using System.Text.RegularExpressions;
internal class Program
{
    private static void Main(string[] args)
    {
        RunTest();  //RunTest=>InputParametrii=>VerifInput->TipTest-if>TestA/TestB
      
        ConsoleKeyInfo cki;
        Console.TreatControlCAsInput = true;
        Console.WriteLine("Apasa ESCAPE ( ESC) pentru a inchide aplicatia \n");
        cki = Console.ReadKey(true);
        while (cki.Key != ConsoleKey.Escape)
        {
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("Apasa ESCAPE ( ESC) pentru a inchide aplicatia \n");
            cki = Console.ReadKey();
        }

        Environment.Exit(0);
           
    }

    static (bool, string) VerifInput(string input,int tipInput)  //return un true daca e cf conditiilor, si apoi un string conditiile, sa stim afisa ce conditii avem in caz ca userul greseste
    {
        string conditii = "doar litere, fara diacritice";
        //tipInput =1 pt fraza, =2 pt cuvant
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
    public static (string, string) InputParametri()  //return (Item1-fraza,Item2-cuvant)
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
        Console.WriteLine();
        Console.WriteLine("Introduce-ti cuvantul:");
        cuvant = Console.ReadLine();
        while (!VerifInput(cuvant,2).Item1)
        {
            Console.WriteLine("Va rog introduceti un cuvant care contine " + VerifInput(cuvant,2).Item2 + "!");
            cuvant = Console.ReadLine();
        }

        return (fraza.ToUpper(), cuvant.ToUpper());
    }

    public static string TipTest()  //return ('a' sau 'b'), reprezentand tipul de test
    {
        string TipTest(string tipTest) => Regex.Match(tipTest, "[abAB]").Success==true ? tipTest:"error"  ;
        string tipTest;
        Console.WriteLine("Tipuri de test:");
        Console.WriteLine("a) Determina daca un cuvant se poate forma luand prima litera din fiecare cuvant din fraza in ordine");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Exemplu:");
        Console.ResetColor();
        Console.WriteLine("\t" + @"pentru fraza: ”Programarea este interesanta.” si cuvantul ”pei”");
        Console.WriteLine("\t" + @"rezultatul este da (”pei” este format din prima litera a fiecarui cuvant din fraza)");

        Console.WriteLine("b) Determina daca cuvantul dat se poate forma luand inceputul (una, doua sau trei litere) de la fiecare cuvant din fraza in ordine");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Exemplu:");
        Console.ResetColor();
        Console.WriteLine("\t" + @" pentru fraza: ”Cocosul canta cucurigu dimineata.” si cuvântul ”coccudim”");
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
        string[] fraza = parametrii.Item1.Split(' ');
        for (int i = 0; i < parametrii.Item2.Length; i++)
        {
            if (parametrii.Item2[i] == fraza[i][0])
            {
                contorCuvant++;
            } ;
        }
        if (contorCuvant== parametrii.Item2.Length) 
        {
            return "Rezultat pozitiv pentru parametrii introdusi in testul A";
        }
        else
        {
            return "Rezultat negativ pentru parametrii introdusi in testul A";
        }
        
    }

    public static string TestB((string, string) parametrii)  //return string mesaj pozitiv sau negativ al testului
    {
        string[] fraza = parametrii.Item1.Split(' ');
        int esteCorect = 1;
        int i = 0;
        int nrCuvantDinFraza = 0;
        int nrLiteraDinCuvantFraza = 0;
        int ultimulCuvantBun = -1;
        while (esteCorect==1 && i< parametrii.Item2.Length)
        {
            if (nrLiteraDinCuvantFraza == 3)
            {
                nrLiteraDinCuvantFraza = 0;
            }

            if (parametrii.Item2[i] == fraza[nrCuvantDinFraza][nrLiteraDinCuvantFraza])
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
            return "Rezultat pozitiv pentru parametrii introdusi in testul B";
        }
        else
        {
            return "Rezultat negativ pentru parametrii introdusi in testul B";
        }
    }

    public static void RunTest()  //cere tipul de test, apoi afiseaza rezultatele acestuia
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

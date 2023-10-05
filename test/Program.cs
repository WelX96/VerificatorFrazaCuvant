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
        string conditii = "doar litere, fara diacritice"; //conditiile pentru afisare, in caz de adaugare/modificare a acestora, sa nu se mai modifice in mesajul de input eronat
        //tipInput =1 pt fraza, =2 pt cuvant
        if (tipInput == 1)
        {
            if (Regex.Matches(input, @"[a-zA-Z\s]").Count == input.Length && !string.IsNullOrEmpty(input)) // verifica sa contina doar litere, indiferent de case + daca e empty
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
            if (Regex.Matches(input, @"[a-zA-Z]").Count == input.Length && !string.IsNullOrEmpty(input)) //pentru cuvant verifica si sa nu aiba spatiu + daca e empty
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
         string  fraza ;
         string  cuvant ;
        Console.WriteLine("Introduce-ti fraza: ");
        fraza = Console.ReadLine();
        while (!VerifInput(fraza,1).Item1)
        {
            Console.WriteLine("Va rog introduceti o fraza care contine " + VerifInput(fraza,1).Item2 + " !");
            fraza = Console.ReadLine();
        }
        Console.WriteLine();
        Console.WriteLine("Introduce-ti cuvantul:");
        cuvant = Console.ReadLine();
        while (!VerifInput(cuvant,2).Item1)
        {
            Console.WriteLine("Va rog introduceti un cuvant care contine " + VerifInput(cuvant,2).Item2 + " si care nu contine spatii!");
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
            Console.WriteLine("Introduceti doar a(A) sau b(B) pentru a putea continua: ");
            tipTest = Console.ReadLine();
        }
        

        return tipTest.ToLower();

    }

    public static string TestA ((string, string) parametrii)  //return string mesaj pozitiv sau negativ al testului
    {
        string cuvantDinFraze = "";
        string[] fraze = parametrii.Item1.Split(' ');
        string cuvant = parametrii.Item2;
        foreach (string fraza in fraze) //construim cuvantul rezultat din preluarea primei litere din fiecare cuvant din fraza
        {
            cuvantDinFraze += fraza[0];
        }
        if (cuvantDinFraze==cuvant) //verificam cuvantul construit cu cuvantul dat
        {
            return "Rezultat pozitiv pentru parametrii introdusi in testul A";
        }
        else
        {
            return "Rezultat negativ pentru parametrii introdusi in testul A";
        }
        
    }


    public static (int, string, string[],int,int,int) VP(int corect, string cuvant, string[] fraze, int i, int c, int l) //VP = Verifica Posibilitate
    {
        

        switch(corect) //switch care incorporeaza recursivitatea metodei, case 1 imi cere sa continui verificarea pe bratele posibilitatilor, 2 imi intoarce faptul ca a ajuns la ultima litera corecta, 3 ca a ajuns la dead end si nu e bun.
        {
            case 1: 
                if (c == fraze.Length)  //daca indexul cuvantului a depasit lungimea frazei, inseamna ca e dead end
                {
                    return (3, cuvant, fraze, i, c, l);
                }
                else if (l == fraze[c].Length) //daca indexul literei din cuvantul frazei a depasit lungimea cuvantului, inseamna ca e dead end
                {
                    return (3, cuvant, fraze, i, c, l);
                }
                else {
                    if (cuvant[i] == fraze[c][l]) //daca literele sunt egale =>
                    {
                        if (i == cuvant.Length - 1 && c == fraze.Length - 1) //daca suntem la ultima litera returnam case 2, care spune ca e rezultat bun, si recursiv vine la call ul initial ca e bun
                        {
                            return (2, cuvant, fraze, i, c, l);
                        }
                        else  //aici se incep cele 2 posibilitati de localizare a urmatoarei litere. in cazul in care se depaseste indexul, if ul initial si else if ul de dupa prind eroare si returneaza case 3
                        {
                            if (VP(1, cuvant, fraze, i + 1, c, l + 1).Item1 == 2) //daca nu suntem la ultima, apelez metoda din nou sa mearga pe lantul primei posibilitati, care este ca litera urmatoare din cuvantul dat poate fi gasit la litera urmatoare a aceluiasi cuvant din fraza sau =>
                            {
                                return (2, cuvant, fraze, i, c, l);
                            }
                            else if (VP(1,cuvant,fraze,i+1,c+1,0).Item1 == 2)// sau poate fi la prima litera din cuvantul urmatori din fraza. Daca urmatoarea litera revine cu case 2, return case 2 la call ul de dinainte( si tot asa)
                            {
                                return (2, cuvant, fraze, i, c, l);
                            }
                            else
                            {
                                return (3, cuvant, fraze, i, c, l); //daca nici una din cele 2 lanturi de posibilitati nu imi returneaza un raspuns positiv, inseamna ca nu este posibila o mapare, returneaza case 3 inapoi
                            }
                        }
                    }

                    else
                    {
                        return (3, cuvant, fraze, i, c, l); //eventual daca nu sunt egale literele, returneaza case 3 inapoi

                    };
                }
            case 2:
                return (2, cuvant, fraze, i, c, l); //suntem la ultima litera, este buna, ne intoarcem pe lant inapoi cu raspuns pozitiv
            case 3:
                return (3, cuvant, fraze, i, c, l); //la un moment dat lantul a gasit o inegalitate, ne intoarcem inapoi cu raspuns negativ
            default:
                return (3, cuvant, fraze, i, c, l); //poate fi folosit aici si un case 4, in care sa returnam o eroare in lant, in cazul in care algoritmul este mai complicat si poate avea erori neprevazute.

        }

    }

    public static string TestB((string, string) parametrii)  //return string mesaj pozitiv sau negativ al testului
    {
        string[] fraze = parametrii.Item1.Split(' ');//prelucram fraza intr-un string de cuvinte
       
        for (int i = 0; i < fraze.Length; i++)
        {
            fraze[i] = fraze[i].Substring(0, 3);// preluam doar primele 3 litere ale cuvintelor, pentru ca doar acolo avem posibilitate, poate fi modificat daca se modifica problema
        }                                       // *Nota: exista posibilitatea ca un cuvant sa fie sub 3 litere, insa posibila eroare este prinsa in VP, deoarece noi verificam indexul literei cu lungimea cuvantului din fraza

        string cuvant = parametrii.Item2;     
        int corect = 1; // 1 verifica ,2 final bun(doar la ultima litera din cuvantul dat) 3 final negativ ( de fiecare data cand nu gaseste egalitate pe pozitia posibila)
        corect = VP(corect, cuvant, fraze, 0, 0, 0).Item1; // se incepe verificarea de la prima litera din cuvant, cu prima litera din primul cuvant din fraza, singura posibilitate initiala, iar de acolo incep lanturile de posibilitati.
                                                           // *Nota: nu se va sari peste un cuvant din fraza, deoarece a doua posibilitate este intotdeauna urmatorul cuvant, iar daca avem case 3, returnam incompatibilitate

        if (corect==2) 
        {
            return "Rezultat pozitiv pentru parametrii introdusi in testul B";
        }
        else
        {
            return "Rezultat negativ pentru parametrii introdusi in testul B";
        }
    }

    public static void RunTest()  //inregistreaza parametrii, cere tipul de test, apoi afiseaza rezultatele acestuia. Metoda poate sa fie direct introdusa in Main, insa lasata separat poate fi chemata din nou mai simplu, daca se doreste repetarea testului
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

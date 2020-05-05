using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandrelli_Magnani_Nadifi_4J_ContoCorrente
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creazione di una banca
            Banca DB = new Banca("Deutsche Bank", "viale Principe Amedeo");

            // Creazione del primo intestatario e del suo relativo conto corrente (tradizionale)
            Intestatario A = new Intestatario("Mario", "Rossi", new DateTime(1990 , 06 , 19), "weudu8763", "via Roma", "3874636784");
            ContoCorrente AC = new ContoCorrente("IT00000001", 5000);
            // Aggiungiamo il conto alla lista di conti
            DB.AddConto(AC);
            // Stampa info intestatario e del relativo conto 
            Console.WriteLine("Intestatario: " + A.getNome() + " " + A.getCognome() + "\nData di nascita: " + A.getDataNascita() + "\nAbita in via: " + A.getIndirizzo() + "\nNumero di telefono: " + A.getTelefono());
            Console.WriteLine("Stampa del saldo di " + A.getNome() + " " + A.getCognome() + ": " + AC.getSaldo());
            // Vengono effettuati un versamento e un prelievo con la relativa stampa aggiornata
            AC.AddMovimenti(new Versamento(100, new DateTime(2020, 03, 19), AC, null));
            AC.AddMovimenti(new Prelievo(300, new DateTime(2020, 04, 10), AC, null));
            Console.WriteLine("MOVIMENTI:");
            foreach (Movimento movimento in AC.getMovimenti())
            {               
                movimento.Sommare();
                Console.Write(movimento.getImporto() + " euro" + " fatto nel: " + movimento.getDataOra() + " - saldo attuale: " + AC.getSaldo() + "\n");
            }
            Console.WriteLine("__________________________________________________________________________________________________\n");


            // Creazione del secondo intestatario
            Intestatario B = new Intestatario("Luigi", "Verdi", new DateTime(1990, 04, 26), "asdaf5678", "via Roma", "3981848734");
            // Creazione del suo conto corrente (tradizionale)
            ContoCorrente BC = new ContoCorrente("IT00000002", 2500);
            // Aggiunta del conto corrente alla lista di conti
            DB.AddConto(BC);
            // Stampa delle info del proprietario e del relativo conto
            Console.WriteLine("Intestatario: " + B.getNome() + " " + B.getCognome() + "\nData di nascita: " + B.getDataNascita() + "\nAbita in via: " + B.getIndirizzo() + "\nNumero di telefono: " + B.getTelefono());
            Console.WriteLine("Stampa del saldo di " + B.getNome() + " " + B.getCognome() + ": " + BC.getSaldo());
            // Vengono effettutati dei movimenti di prova come il versamento, il prelievo e il bonifico
            BC.AddMovimenti(new Versamento(300, new DateTime(2020, 05, 30), BC, null));
            BC.AddMovimenti(new Prelievo(100, new DateTime(2020, 07, 21), BC, null));
            BC.AddMovimenti(new Bonifico(AC, 50, -200, new DateTime(2020, 06, 06), BC, null));
            // Stampa dei movimenti effettuati
            Console.WriteLine("MOVIMENTI:");
            foreach (Movimento movimento in BC.getMovimenti())
            {
                movimento.Sommare();               
                Console.Write(movimento.getImporto() + " euro " + "fatto nel: " + movimento.getDataOra() + " - saldo attuale: " + BC.getSaldo() + "\n");
            }
            Console.WriteLine("__________________________________________________________________________________________________\n");

            //Creazione di un nuovo intestatario online e del suo relativo conto online, aggiungendolo alla lista dei conti
            Intestatario C = new Intestatario("Antonio", "Peroni", new DateTime(1968, 09, 09), "udhwoiud83", "via Lazio", "38648374683");
            ContoOnline CC = new ContoOnline("Anto384", "vero4666", "IT00000003", 10000);
            DB.AddConto(CC);
            Console.WriteLine("Intestatario: " + C.getNome() + " " + C.getCognome() + "\nData di nascita: " + C.getDataNascita() + "\nAbita in via: " + C.getIndirizzo() + "\nNumero di telefono: " + C.getTelefono());
            Console.WriteLine("Stampa del saldo di " + C.getNome() + " " + C.getCognome() + ": " + CC.getSaldo());
            //Vengono effettuati dei movimenti, aggiunti poi alla lista dei movimenti
            CC.AddMovimenti(new Versamento(1000, new DateTime(2020, 01, 07), null, CC));
            CC.AddMovimenti(new Prelievo(600, new DateTime(2020, 09, 25), null, CC));
            // Stampa dei movimenti effettuati
            Console.WriteLine("MOVIMENTI:");
            foreach (Movimento movimento in CC.getMovimenti())
            {              
                movimento.SommareOnline();
                Console.Write(movimento.getImporto() + " euro " + "fatto nel: " + movimento.getDataOra() + " - saldo: " + CC.getSaldo() + "\n");
            }
            Console.WriteLine("__________________________________________________________________________________________________\n");

            Console.ReadLine();
        }
    }
}

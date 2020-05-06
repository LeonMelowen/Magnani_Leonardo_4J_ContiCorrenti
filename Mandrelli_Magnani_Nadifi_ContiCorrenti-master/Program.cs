using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandrelli_Magnani_Nadifi_ContiCorrenti_master
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creazione di una banca
            string nomeBanca;
            string indirizzoBanca;
            string nome;
            string cognome;
            DateTime dataNascita;
            DateTime dataMovimento;
            string codiceFiscale;
            string indirizzo;
            string telefono;
            string iban;
            string iban2;
            double saldo;
            string username;
            string password;
            double quantita;
            string scelta="";
            string scelta2="";
            int maxMovimenti;
            int nMovimenti;

            Console.Write("Nome Banca: ");
            nomeBanca=Console.ReadLine();
            Console.Write("Indirizzo Banca: ");
            indirizzoBanca = Console.ReadLine();
            Banca DB = new Banca(nomeBanca, indirizzoBanca);

            do {

                Console.Write("Nome Intestatario: ");
                nome = Console.ReadLine();
                Console.Write("Cognome Intestatario: ");
                cognome = Console.ReadLine();
                Console.Write("Data di nascita Intestatario: ");
                dataNascita = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Codice fiscale Intestatario: ");
                codiceFiscale = Console.ReadLine();
                Console.Write("Indirizzo Intestatario: ");
                indirizzo = Console.ReadLine();
                Console.Write("Numero telefono Intestatario: ");
                telefono = Console.ReadLine();

                // Creazione del primo intestatario e del suo relativo conto corrente (tradizionale)
                Intestatario cliente = new Intestatario(nome, cognome, dataNascita, codiceFiscale, indirizzo, telefono);
                DB.AddIntestatario(cliente);
                
                //Creazione conti
                do {

                    do {
                        Console.Write("Vuole creare un nuove conto? (Y/N)");
                        scelta = Console.ReadLine();
                    } while (scelta != "y" && scelta != "Y" && scelta != "n" && scelta != "N");

                    if (scelta == "y" || scelta == "Y")
                    {
                        do
                        {
                            Console.Write("Che cosa vuole fare? \n 1.ContoCorrente \n 2.ContoOnline \n 0.BACK \n\n Scelta:");
                            scelta2 = Console.ReadLine();
                        } while (scelta2 != "1" && scelta2 != "2" && scelta2 != "0");

                        switch (scelta2)
                        {
                            case "1":
                                Console.Write("Iban del conto:");
                                iban = Console.ReadLine();
                                Console.Write("Saldo del conto:");
                                saldo = Convert.ToDouble(Console.ReadLine());
                                DB.AddConto(new ContoCorrente(iban, saldo, cliente));
                               break;
                            case "2":
                                Console.Write("Username:");
                                username = Console.ReadLine();
                                Console.Write("Password:");
                                password = Console.ReadLine();
                                Console.Write("Iban del conto:");
                                iban = Console.ReadLine();
                                Console.Write("Saldo del conto:");
                                saldo = Convert.ToDouble(Console.ReadLine());
                                DB.AddConto(new ContoOnline(username, password, iban, saldo, cliente));
                                break;
                        }
                 
                        do
                        {
                            Console.Write("Vuole aggiungere un altro conto?: (Y/N)");
                            scelta = Console.ReadLine();
                        } while (scelta != "y" && scelta != "Y" && scelta != "n" && scelta != "N");
                    }
                
                } while (scelta2=="0" || (scelta=="Y" || scelta=="y"));


                //Creazione movimenti
                do
                {

                    do
                    {
                        Console.Write("Vuole fare dei movimenti?: (Y/N)");
                        scelta = Console.ReadLine();
                    } while (scelta != "y" && scelta != "Y" && scelta != "n" && scelta != "N");

                    if (scelta == "y" || scelta == "Y")
                    {
                        do
                        {
                            Console.Write("Che cosa vuole fare? \n 1.Prelievo \n 2.Versamento \n 3.Bonifico \n 0.BACK \n");
                            scelta2 = Console.ReadLine();
                        } while (scelta2 != "1" && scelta2 != "2" && scelta2 != "3" && scelta2!="0");

                        switch (scelta2)
                        {
                            case "1":
                                Console.Write("Iban del conto in cui vuole prelevare: ");
                                iban=Console.ReadLine();
                                Console.Write("Quanto vuole prelevare?: ");
                                quantita = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Data prelievo?: ");
                                dataMovimento = Convert.ToDateTime(Console.ReadLine());

                                bool controllo = false;
                                foreach (ContoCorrente conto in DB.getConti())
                                {
                                    if (iban == conto.getIban())
                                    {
                                        maxMovimenti = conto.getMaxMovimenti();
                                        nMovimenti = conto.getNumeroMovimenti();

                                        if (nMovimenti < maxMovimenti)
                                        {
                                            if (conto is ContoCorrente)
                                            {
                                                if (conto.getSaldo() >= quantita)
                                                {
                                                    Prelievo p = new Prelievo(quantita, dataMovimento);
                                                    conto.AddMovimenti(p);
                                                    controllo = true;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Non puo prelevare di piu del saldo nel conto = " + conto.getSaldo());
                                                    controllo = true;
                                                }
                                            }
                                            else
                                            {
                                                if (conto.getSaldo() >= quantita)
                                                {
                                                    if (quantita <= 3000)
                                                    {
                                                        Prelievo p = new Prelievo(quantita, dataMovimento);
                                                        conto.AddMovimenti(p);
                                                        controllo = true;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Puo prelevare al massimo 3000 ad ogni prelevazione");
                                                        controllo = true;
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Non puo prelevare di piu del saldo nel conto = " + conto.getSaldo());
                                                    controllo = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Superato il limite dei movimenti = " + conto.getMaxMovimenti());
                                            controllo = true;
                                        }                                    
                                    }
                                }
                                if (controllo == false)
                                {
                                    Console.WriteLine("Non è stato trovato un conto con questo iban..");
                                }

                                do
                                {
                                    Console.Write("Vuole fare un altro movimento?: (Y/N)");
                                    scelta = Console.ReadLine();
                                } while (scelta != "y" && scelta != "Y" && scelta != "n" && scelta != "N");

                                break;

                            case "2":
                                Console.Write("Iban del conto in cui vuole versare: ");
                                iban = Console.ReadLine();
                                Console.Write("Quanto vuole versare?: ");
                                quantita = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Data varsamento?: ");
                                dataMovimento = Convert.ToDateTime(Console.ReadLine());

                                bool controllo2 = false;
                                foreach (ContoCorrente conto in DB.getConti())
                                {
                                    if (iban == conto.getIban())
                                    {
                                         maxMovimenti = conto.getMaxMovimenti();
                                         nMovimenti = conto.getNumeroMovimenti();
                                        if (nMovimenti < maxMovimenti)
                                        {
                                            Versamento v = new Versamento(quantita, dataMovimento);
                                            conto.AddMovimenti(v);
                                            controllo2 = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Superato il limite dei movimenti = " + conto.getMaxMovimenti());
                                            controllo = true;
                                        }                                                                 
                                    }
                                }
                                if (controllo2 == false)
                                {
                                    Console.WriteLine("Non è stato trovato un conto con questo iban..");
                                }

                                do
                                {
                                    Console.Write("Vuole fare un altro movimento?: (Y/N)");
                                    scelta = Console.ReadLine();
                                } while (scelta != "y" && scelta != "Y" && scelta != "n" && scelta != "N");

                                break;

                            case "3":
                                Console.Write("Iban del conto in cui vuole prelevare: ");
                                iban2 = Console.ReadLine();
                                Console.Write("Iban del conto in cui vuole versare: ");
                                iban = Console.ReadLine();
                                Console.Write("Quanto vuole versare/prelevare?: ");
                                quantita = Convert.ToDouble(Console.ReadLine());
                                Console.Write("Data bonifico?: ");
                                dataMovimento = Convert.ToDateTime(Console.ReadLine());

                                bool controllo3 = false;
                                ContoCorrente posto = null;

                                Bonifico b = new Bonifico(quantita, dataMovimento);

                                foreach (ContoCorrente conto in DB.getConti())
                                {
                                    if (iban == conto.getIban())
                                    {
                                        posto = conto;                                                                                                            
                                    }
                                }

                                if (posto != null)
                                {
                                    foreach (ContoCorrente conto in DB.getConti())
                                    {
                                        if (iban2 == conto.getIban())
                                        {
                                            b.EseguiBonifico(posto, conto);
                                            controllo3 = true;
                                        }
                                    }
                                }

                                if (controllo3 == false)
                                {
                                    Console.WriteLine("Non è stato trovato uno dei 2 conti con questi iban..");
                                }

                                do
                                {
                                    Console.Write("Vuole fare un altro movimento?: (Y/N)");
                                    scelta = Console.ReadLine();
                                } while (scelta != "y" && scelta != "Y" && scelta != "n" && scelta != "N");

                                break;
                        }
                    }

                } while (scelta2 == "0" || (scelta == "Y" || scelta == "y"));

                do
                {
                    Console.Write("Vuole creare un nuovo intestatario?: (Y/N)");
                    scelta = Console.ReadLine();
                } while (scelta != "y" && scelta != "Y" && scelta != "n" && scelta != "N");

            } while (scelta=="Y" || scelta=="y");

            Console.WriteLine("\n");
            do {
                // Stampa info intestatario e del relativo conto 
                foreach (Intestatario intestatario in DB.getIntestatari())
                {
                    Console.WriteLine("\nIntestatario: " + intestatario.getNome() + " " + intestatario.getCognome() + "\nData di nascita: " + intestatario.getDataNascita() + "\nAbita in via: " + intestatario.getIndirizzo() + "\nNumero di telefono: " + intestatario.getTelefono());
                    foreach (ContoCorrente conto in DB.getConti())
                    {
                        if (conto.getCodiceFiscaleintestatario() == intestatario.getCodiceFiscale())
                        {
                            Console.WriteLine("\nCONTO: ");
                            Console.WriteLine("IBAN: " + conto.getIban());
                            Console.WriteLine("Stampa del saldo di " + intestatario.getNome() + " " + intestatario.getCognome() + ": " + conto.getSaldo());
                            // Vengono effettuati un versamento e un prelievo con la relativa stampa aggiornata
                            Console.WriteLine("MOVIMENTI:");
                            foreach (Movimento movimento in conto.getMovimenti())
                            {
                                movimento.Sommare(conto);
                                Console.Write(movimento.getImporto() + " euro" + " fatto nel: " + movimento.getDataOra() + " - saldo attuale: " + conto.getSaldo() + "\n");
                            }
                            Console.WriteLine("\n");
                        }
                    }
                    Console.WriteLine("__________________________________________________________________________________________________");
                }
                bool controllou = false;
                do {
                    do
                    {
                        Console.Write("\nVuole eliminare un conto?: (Y/N)");
                        scelta = Console.ReadLine();
                    } while (scelta != "y" && scelta != "Y" && scelta != "n" && scelta != "N");
                    if (scelta == "y" || scelta == "Y")
                    {

                        Console.Write("Inserire Iban del conto che vuole eliminare?: ");
                        iban = Console.ReadLine();
                        foreach (ContoCorrente conto in DB.getConti())
                        {
                            if (iban == conto.getIban())
                            {
                                DB.RemoveConto(conto);
                                controllou = true;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Iban non trovato");
                                controllou = false;
                            }
                        }
                    }
                    else
                    {
                        controllou = true;
                    }
                } while (controllou == false);
            } while (scelta!="n" && scelta!="N");
            Console.ReadLine();
        }
    }
}

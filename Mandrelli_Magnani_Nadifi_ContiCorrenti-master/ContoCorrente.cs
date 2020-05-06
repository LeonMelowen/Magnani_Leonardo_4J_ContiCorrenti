using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandrelli_Magnani_Nadifi_ContiCorrenti_master
{
    class ContoCorrente//Dichiarazione classe Conto Corrente
    {
        protected string iban;
        protected double saldo;
        string username = null;
        string password=null;
        protected Intestatario intestatario;
        protected List<Movimento> movimenti = new List<Movimento>(); //Lista di movimenti
        int maxMovimenti;

        public ContoCorrente(string iban, double saldo, Intestatario intestatario)//Costruttore classe
        {
            this.iban = iban;
            this.saldo = saldo;
            this.intestatario = intestatario;

            maxMovimenti = 50;
        }

        public virtual string getUsername()
        {
            return username;
        }

        public virtual string getPassword()
        {
            return password;
        }

        public virtual string getNomeintestatario()//Metodo che restituisce nome intestatario
        {
            return intestatario.getNome();
        }

        public virtual string getCodiceFiscaleintestatario()
        {
            return intestatario.getCodiceFiscale();
        }

        public virtual string getIban()//Metodo che restituisce Iban
        {
            return iban;
        }

        public virtual double getSaldo()//Metodo che restituisce saldo
        {
            return saldo;
        }

        public virtual void setSaldo(double importo) //Aggiorna il saldo del conto
        {
            saldo = saldo + importo;
        }

        public virtual void AddMovimenti(Movimento movimento)//Viene aggiunto un movimento alla lista dei movimenti
        {
            movimenti.Add(movimento);
        }

        public virtual List<Movimento> getMovimenti()
        {
            return movimenti;
        }

        public virtual int getNumeroMovimenti()
        {
            return movimenti.Count();
        }

        public virtual int getMaxMovimenti()
        {
            return maxMovimenti;
        }

    }

    class ContoOnline : ContoCorrente
    {
        double maxPrelievo;
        string username;
        string password;
        int maxMovimenti;

        public ContoOnline(string username, string password, string iban, double saldo, Intestatario intestatario) : base(iban, saldo, intestatario)
        {
            maxPrelievo = 3000;
            this.username = username;
            this.password = password;
            maxMovimenti = 50;
        }

        public override void setSaldo(double importo)
        {
            if (importo > 0 || (importo < 0 && importo >= -maxPrelievo))
            {
                saldo = saldo + importo;
            }
        }

        public override string getUsername()
        {
            return username;
        }

        public override string getPassword()
        {
            return password;
        }

        public override double getSaldo()
        {
            return saldo;
        }

        public override string getIban()
        {
            return iban;
        }

        public override string getNomeintestatario()
        {
            return intestatario.getNome();
        }

        public override string getCodiceFiscaleintestatario()
        {
            return intestatario.getCodiceFiscale();
        }

        public override void AddMovimenti(Movimento movimento)
        {
            movimenti.Add(movimento);
        }

        public override List<Movimento> getMovimenti()
        {
            return movimenti;
        }

        public override int getNumeroMovimenti()
        {
            return movimenti.Count();
        }

        public override int getMaxMovimenti()
        {
            return maxMovimenti;
        }

    }

    class Intestatario
    {
        string nome;
        string cognome;
        DateTime dataNascita;
        string codiceFiscale;
        string indirizzo;
        string telefono;

        public Intestatario(string nome, string cognome, DateTime dataNascita, string codiceFiscale, string indirizzo, string telefono)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.dataNascita = dataNascita;
            this.codiceFiscale = codiceFiscale;
            this.indirizzo = indirizzo;
            this.telefono = telefono;
        }

        public string getNome()
        {
            return nome;
        }

        public string getCognome()
        {
            return cognome;
        }

        public DateTime getDataNascita()
        {
            return dataNascita;
        }

        public string getCodiceFiscale()
        {
            return codiceFiscale;
        }

        public string getIndirizzo()
        {
            return indirizzo;
        }

        public string getTelefono()
        {
            return telefono;
        }
    }
    

    class Banca
    {
        string nome;
        string indirizzo;
        List<Intestatario> intestatari;
        List<ContoCorrente> conti; 

        public Banca(string nome, string indirizzo)
        {
            this.nome = nome;
            this.indirizzo = indirizzo;
            conti = new List<ContoCorrente>(); //Lista di conti
            intestatari= new List<Intestatario>();
        }

        public string getNome()
        {
            return nome;
        }

        public string getIndirizzo()
        {
            return indirizzo;
        }

        public void AddIntestatario(Intestatario i)
        {
            intestatari.Add(i);
        }

        public void AddConto(ContoCorrente c)//Viene aggiunto un conto alla lista dei conti
        {
            conti.Add(c);
        }

        public void RemoveConto(ContoCorrente c)
        {
            conti.Remove(c);
        }

        public List<ContoCorrente> getConti()
        {
            return conti;
        }

        public List<Intestatario> getIntestatari()
        {
            return intestatari;
        }

    }

    class Movimento
    {
        protected double importo;
        protected DateTime dataOra;

        public Movimento(double importo, DateTime dataOra)
        {
            this.importo = importo;
            this.dataOra = dataOra;           
        }

        public virtual void Sommare(ContoCorrente conto)
        {
            conto.setSaldo(importo);//Importo movimento nel conto corrente
        }

        public virtual double getImporto()//Restituisce l'importo del movimento
        {
            return importo;
        }

        public virtual DateTime getDataOra()//Restituisce l'orario del movimento
        {
            return dataOra;
        }
    }

    class Prelievo : Movimento
    {
        public Prelievo(double importo, DateTime dataOra) : base(importo, dataOra)
        {

        }

        public override void Sommare(ContoCorrente conto)//Effettua un prelievo dal conto corrente
        {
            importo = -importo;
            conto.setSaldo(importo);
        }

        public override double getImporto()//Restituisce l'importo del prelieve
        {
            return importo;
        }

        public override DateTime getDataOra()//Restituisce l'orario del versamento
        {
            return dataOra;
        }
    }

    class Versamento : Movimento
    {
        public Versamento(double importo, DateTime dataOra) : base(importo, dataOra)
        {

        }

        public override void Sommare(ContoCorrente conto)//Importo versamento nel conto corrente
        {
            conto.setSaldo(importo);
        }

        public override double getImporto()//Restituisce l'importo del versamento
        {
            return importo;
        }

        public override DateTime getDataOra()//Restituisce l'orario del versamento
        {
            return dataOra;
        }
    }

    class Bonifico : Movimento
    {
        double commissione;

        public Bonifico(double importo, DateTime dataOra) : base(importo, dataOra)
        {
            commissione=importo*0.015;
        }

        public void EseguiBonifico(ContoCorrente versa, ContoCorrente preleva)//Metodo che ci permette di eseguire un bonifico, andando a prelevare i soldi da un conto per versarli in quello del destinatario
        {
            versa.setSaldo(importo-commissione);
            importo = -importo;
            preleva.setSaldo(importo);

        }

        public string getMittente(ContoCorrente versa)//Restituisce il mittente del bonifico
        {
            return versa.getNomeintestatario();
        }

        public double getCommissione()
        {
            return commissione;
        }
    }

}

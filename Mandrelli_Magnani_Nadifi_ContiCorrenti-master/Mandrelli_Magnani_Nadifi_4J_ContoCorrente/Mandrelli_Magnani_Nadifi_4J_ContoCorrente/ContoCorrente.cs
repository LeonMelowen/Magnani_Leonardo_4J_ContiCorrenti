using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mandrelli_Magnani_Nadifi_4J_ContoCorrente
{
    class ContoCorrente//Dichiarazione classe Conto Corrente
    {
        string iban;
        protected float saldo;
        Banca banca;
        Intestatario intestatario;
        ContoOnline contoOnline;
        Bonifico bonifico;
        protected List<Movimento> movimenti = new List<Movimento>(); //Lista di movimenti

        public ContoCorrente( string iban, float saldo)//Costruttore classe
        {
            this.iban = iban;
            this.saldo = saldo;
        }

        public string getNomeintestatario()//Metodo che restituisce nome intestatario
        {
            return intestatario.getNome();
        }

        public string getIban()//Metodo che restituisce Iban
        {
            return iban;
        }

        public float getSaldo()//Metodo che restituisce saldo
        {
            return saldo;
        }

        public virtual void setSaldo(float importo) //Aggiorna il saldo del conto
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
    }

    class Intestatario
    {
        string nome;
        string cognome;
        DateTime dataNascita;
        string codiceFiscale;
        string indirizzo;
        string telefono;
        ContoCorrente contoCorrente;

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

    class Bonifico : Movimento
    {
        string mittente;
        float commissione;

        ContoCorrente contoCorrente1;

        public Bonifico(ContoCorrente contoCorrente1, float commissione, float importo, DateTime dataOra, ContoCorrente contoCorrente, ContoOnline contoOnline) : base (importo, dataOra, contoCorrente, contoOnline)
        {
            this.contoCorrente1 = contoCorrente1;
            this.commissione = commissione;
        }

        public void EseguiBonifico()//Metodo che ci permette di eseguire un bonifico, andando a prelevare i soldi da un conto per versarli in quello del destinatario
        {
            contoCorrente1.setSaldo(importo);
            importo = -importo;
            contoCorrente.setSaldo(importo);

        }

        public string getMittente()//Restituisce il mittente del bonifico
        {
            return contoCorrente1.getNomeintestatario();
        }

        public float getCommissione()
        {
            return commissione;
        }
    }

    class ContoOnline : ContoCorrente
    {
        float maxPrelievo;
        string username;
        string password;
        
        public ContoOnline(string username, string password, string iban, float saldo) : base(iban, saldo)
        {
            maxPrelievo = 3000;
            this.username = username;
            this.password = password;
        }

        public override void setSaldo(float importo)
        {
            if (importo > 0 || (importo < 0 && importo >= -maxPrelievo))
            {
                saldo = saldo + importo;
            }
        }

        public string getUsername()
        {
            return username;
        }

        public string getPassword()
        {
            return password;
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

    }

    class Banca
    {
        string nome;
        string indirizzo;
        ContoCorrente ContoCorrente;
        List<ContoCorrente> conti = new List<ContoCorrente>(); //Lista di conti 

        public Banca(string nome, string indirizzo)
        {
            this.nome = nome;
            this.indirizzo = indirizzo;
        }

        public string getNome()
        {
            return nome;
        }

        public string getIndirizzo()
        {
            return indirizzo;
        }

        public void AddConto (ContoCorrente c)//Viene aggiunto un conto alla lista dei conti
        {
            conti.Add(c);
        }
    }

    class Movimento
    {
        protected ContoCorrente contoCorrente;
        protected ContoOnline contoOnline;
        int nMovimenti;
        int maxMovimenti;
        protected float importo;
        protected DateTime dataOra;

        public Movimento(float importo, DateTime dataOra, ContoCorrente contoCorrente, ContoOnline contoOnline)
        {
            this.importo = importo;
            this.dataOra = dataOra;
            this.contoCorrente = contoCorrente;
            this.contoOnline = contoOnline;
            if (contoCorrente == null && contoOnline != null)
            {
                nMovimenti = contoOnline.getNumeroMovimenti(); //Restituisce il numero di movimenti del conto online
            }
            else
            {
                if (contoOnline == null && contoCorrente != null)
                {
                    nMovimenti = contoCorrente.getNumeroMovimenti(); //Restituisce il numero di movimenti del conto corrente
                }
                else
                {
                    nMovimenti = 0;
                }
            }
      
            maxMovimenti = 50;
        }

        public virtual void Sommare()
        {
            contoCorrente.setSaldo(importo);//Importo movimento nel conto corrente
        }

        public virtual void SommareOnline()
        {
            contoOnline.setSaldo(importo);//Importo movimento nel conto online
        }

        public virtual float getImporto()//Restituisce l'importo del movimento
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
        public Prelievo(float importo, DateTime dataOra, ContoCorrente contoCorrente, ContoOnline contoOnline) : base(importo, dataOra, contoCorrente, contoOnline)
        {
            
        }

        public override void Sommare()//Effettua un prelievo dal conto corrente
        {
            importo = -importo;
            contoCorrente.setSaldo(importo);
        }

        public override void SommareOnline()//Effettua un prelievo dal conto online
        {
            importo = -importo;
            contoOnline.setSaldo(importo);
        }

        public override float getImporto()//Restituisce l'importo del prelieve
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
        public Versamento(float importo, DateTime dataOra, ContoCorrente contoCorrente, ContoOnline contoOnline) : base(importo, dataOra, contoCorrente, contoOnline)
        {
            
        }

        public override void Sommare()//Importo versamento nel conto corrente
        {
            contoCorrente.setSaldo(importo);
        }

        public override void SommareOnline()//Importo versamento nel conto online
        {
            contoOnline.setSaldo(importo);
        }

        public override float getImporto()//Restituisce l'importo del versamento
        {
            return importo;
        }

        public override DateTime getDataOra()//Restituisce l'orario del versamento
        {
            return dataOra;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Palestra.model;

namespace Palestra.Persistence
{
    public class MainPersistanceManager : IAllenamentoPersistenceManager, IEsercizioPersistanceManager, IPianoAllenamentoPersistenceManager, IUtentePersistenceManager
    {
        private List<Esercizio> _esercizi;
        private SqlConnection _conn; 

        public MainPersistanceManager()
        {
            //connessione al database che rimarrà connesso per tutta la durata dell'applicazione
            Conn = new SqlConnection();
            Conn.ConnectionString = "Data Source=NOMESERVER;Initial Catalog=NOMEDB;Integrated Security=True";
            Conn.Open();

            _esercizi = new List<Esercizio>();
            _esercizi.Add(new Esercizio("Panca piana", FasciaMuscolare.Pettorali, "", Risorsa.SalaPesi ));
            //mettere tutti gli esercizi nella lista
        }

        public SqlConnection Conn { get => _conn; set => _conn = value; }


        public Utente Autentica(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllenamento(Allenamento allenamento)
        {
            throw new NotImplementedException();
        }

        public bool DeletePianoAllenamento(PianoAllenamento pianoAllenamento)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUtente(Utente utente)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Allenamento> GetAllAllenamenti()
        {
            List<Allenamento> allenamenti = new List<Allenamento>();
            try
            { 
                SqlCommand myCommand = new SqlCommand("select * from allenamenti",   Conn);
                SqlDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    allenamenti.Add(new Allenamento((int)myReader["DurataInMinuti"], (DateTime)myReader["Data"]));
                }
            }
            catch (SqlException e)
            {
                //eccezione da gestire
                throw;
            }
            return allenamenti;
        }

        public IEnumerable<Esercizio> GetAllEsercizi()
        {
            return _esercizi;
        }

        public PianoAllenamento GetPianoAllenamento()
        {
            throw new NotImplementedException();
        }

        public Utente GetUtente()
        {
            throw new NotImplementedException();
        }

        public bool SaveAllenamento(Allenamento allenamento)
        {
            throw new NotImplementedException();
        }

        public bool SavePianoAllenamento(PianoAllenamento pianoAllenamento)
        {
            throw new NotImplementedException();
        }

        public bool SaveUtente(Utente utente)
        {
            try
            {
                //uso di SqlParameter per garantire sicurezza e evitare Sql Injection
                SqlParameter myParam = new SqlParameter("@Param1", SqlDbType.VarChar, 11);
                myParam.Value = utente.Nome;

                SqlParameter myParam2 = new SqlParameter("@Param2", SqlDbType.VarChar, 11);
                myParam2.Value = utente.Cognome;

                SqlParameter myParam3 = new SqlParameter("@Param3", SqlDbType.Date);
                myParam3.Value = utente.DataDiNascita;

                SqlParameter myParam4 = new SqlParameter("@Param4", SqlDbType.Bit);
                myParam4.Value = utente.Sesso;

                SqlParameter myParam5 = new SqlParameter("@Param5", SqlDbType.Int, 2);
                myParam5.Value = utente.AltezzaInCm;

                SqlParameter myParam6 = new SqlParameter("@Param6", SqlDbType.Int, 2);
                myParam6.Value = utente.PesoInKg;

                SqlCommand myCommand = new SqlCommand("INSERT INTO Utenti (Nome, Cognome, DataDiNascita, Sesso, AltezzaInCm, PesoInKg)" +
                    " Values (@Param1, @Param2, @Param3, @Param4, @Param5, @Param6)", Conn);
                myCommand.Parameters.Add(myParam);
                myCommand.Parameters.Add(myParam2);
                myCommand.Parameters.Add(myParam3);
                myCommand.Parameters.Add(myParam4);
                myCommand.Parameters.Add(myParam5);
                myCommand.Parameters.Add(myParam6);

                myCommand.ExecuteNonQuery();

                return true;
            }
            catch(SqlException e)
            {
                //eccezione da gestire
                throw;
            }
        }

        //da invocare sempre alla chiusura dell'applicazione
        public void CloseConnection()
        {
            _conn.Close();
        }
    }
}

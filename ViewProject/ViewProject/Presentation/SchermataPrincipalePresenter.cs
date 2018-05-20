﻿using ViewProject.model;
using ViewProject.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ViewProject.Presentation
{
    public class SchermataPrincipalePresenter
    {
        MainPersistanceManager _mpm;
        SchermataPrincipaleView _schermataPrincipaleView;
        Utente _utente;

        public SchermataPrincipalePresenter(MainPersistanceManager mpm, SchermataPrincipaleView schermataPrincipaleView, Utente utente)
        {
            _mpm = mpm;
            _schermataPrincipaleView = schermataPrincipaleView;
            _utente = utente;

            _schermataPrincipaleView.Load += OnLoad_SchermataPrincipale;
            _schermataPrincipaleView.buttonReset.Click += Click_ButtonReset;
        }

        private void Click_ButtonReset(object sender, EventArgs e)
        { 
            _mpm.Reset(_utente);
        }

        private void OnLoad_SchermataPrincipale(object sender, EventArgs e)
        {

            PianoAllenamento piano = null;
            try
            {
               piano = _mpm.LoadPianoAllenamento(_utente);
            }
            catch (SqlException)
            {
                MessageBox.Show("Errore nel database: verificare la procedura d'installazione", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _schermataPrincipaleView.dataGridViewScheda.DataSource = null;
            BindingSource bindingSource = new BindingSource();
            foreach (GiornoAllenamento giorno in piano.GiorniAllenamento)
            {
                foreach (EsecuzioneEsercizio esercizio in giorno.ListaEsecuzioniEsercizi)
                {
                    bindingSource.Add(esercizio);
                }
            }
            _schermataPrincipaleView.dataGridViewScheda.DataSource = bindingSource;
        
            
    
        }
    }
}
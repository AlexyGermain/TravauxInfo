using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class ExpediteurCuisineSimulacre : IExpediteurCuisine
    {

        public ExpediteurCuisineSimulacre(
            string numeroCommandeAttendu,
            int nombreArticlesAttendu,
            decimal montantTotalAttendu,
            int nbAppelAttendu)
        {
            m_montantTotalAttendu = montantTotalAttendu;
            m_nbAppelAttendu = nbAppelAttendu;
            m_nbArticlesAttendu = nombreArticlesAttendu;
            m_numeroCommandeAttendu = numeroCommandeAttendu;
            m_compteurAppel = 0;
        }

        public string m_numeroCommandeAttendu { get; }
        public int m_nbArticlesAttendu { get; }
        public decimal m_montantTotalAttendu { get; }
        public int m_nbAppelAttendu { get; }
        public int m_compteurAppel { get; set; }

        public void Envoyer(
            string numeroCommande,
            int nombreArticles,
            decimal montantTotal)
        {
            if(m_nbArticlesAttendu != nombreArticles)
            {
                return;
            }
            if (m_numeroCommandeAttendu != numeroCommande)
            {
                return;
            }
            if (m_montantTotalAttendu != montantTotal)
            {
                return;
            }

            m_compteurAppel++;
        }

        public bool VerifierAttentes()
        {
            return m_compteurAppel == m_nbAppelAttendu;
        }

    }
}

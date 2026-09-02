using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Tests
{
    public class ServiceTransmissionCuisineTests
    {
        //Pour valider les infos dans chaque test.
        private void VerifierAttentesEtParams(Commande commande, ExpediteurCuisineSimulacre expediteur)
        {
            Assert.True(expediteur.VerifierAttentes());

            if (commande == null || expediteur == null)
            {
                return;
            }
            Assert.Equal(commande.Numero, expediteur.m_numeroCommandeAttendu);
            Assert.Equal(commande.NombreArticles, expediteur.m_nbArticlesAttendu);
            Assert.Equal(commande.SousTotal, expediteur.m_montantTotalAttendu);
        }


        [Fact]
        public void Transmettre_CommandeValide_CodeNBArticlesMontantUnAppel()
        {
            Commande commande = new("C-1042");
            commande.AjouterLigne(new LigneCommande("POUT3", "Poutine", 14.50m, 2, 0m));
            commande.AjouterLigne(new LigneCommande("SOUP1", "Soupe miso", 6.00m, 1, 0m));

            ExpediteurCuisineSimulacre expediteur = new ExpediteurCuisineSimulacre("C-1042", 3, 35.00m, 1);
            ServiceTransmissionCuisine transmission = new(expediteur);

            transmission.Transmettre(commande);

            VerifierAttentesEtParams(commande, expediteur);
        }

        [Fact]
        public void Transmettre_CommandeVide_Erreur()
        {
            //Arrange
            Commande commande = new("C-1042");

            ExpediteurCuisineSimulacre expediteur = new ExpediteurCuisineSimulacre("C-1042", 0, 0m, 0);
            ServiceTransmissionCuisine transmission = new(expediteur);

            //Act
            Action action = () => transmission.Transmettre(commande);

            //Assert
            Assert.Throws<InvalidOperationException>(action);
            VerifierAttentesEtParams(commande, expediteur);
        }

        [Fact]
        public void Transmettre_CommandeNull_Erreur()
        {
            //Arrange
            Commande commande = null;

            ExpediteurCuisineSimulacre expediteur = new ExpediteurCuisineSimulacre("", 0, 0m, 0);
            ServiceTransmissionCuisine transmission = new(expediteur);

            //Act
            Action action = () => transmission.Transmettre(commande);

            //Assert
            Assert.Throws<ArgumentNullException>(action);
            VerifierAttentesEtParams(commande, expediteur);
        }

    }
}

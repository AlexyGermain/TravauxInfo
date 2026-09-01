using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Tests
{
    public class CommandeTests
    {
        [Fact]
        public void Commande_CodeVide_Erreur()
        {
            Action value = () => new Commande("");

            Assert.Throws<ArgumentException>(value);
        }
        
        [Fact]
        public void EstVide_CommandeVide_Vrai()
        {
            Commande commande = new("1234");

            Assert.True(commande.EstVide && commande.SousTotal == 0.0m && commande.NombreArticles == 0);
        }

        [Fact]
        public void AjouterLigne_LigneVide_Erreur()
        {
            Commande commande = new("1234");
            Action ajouterLigne = () => commande.AjouterLigne(null);

            Assert.Throws<ArgumentNullException>(ajouterLigne);
        }

        [Fact]
        public void SousTotal_TroisArticlesUnAvecRabais_RetourneSousTotalEtNbArticles()
        {
            //Arrange
            Commande commande = new("1234");
            decimal sousTotalAttendu = 32.10m;
            int quantiteAttendu = 3;

            //Act
            commande.AjouterLigne(new LigneCommande("A1", "Bon repas", 4.20m, 1, 50));
            commande.AjouterLigne(new LigneCommande("A2", "Bon repas", 15.00m, 2, 0));

            //Assert
            //Assert.True(commande.SousTotal == sousTotalAttendu && commande.NombreArticles == quantiteAttendu);
            Assert.Equal(sousTotalAttendu, commande.SousTotal);
            Assert.Equal(quantiteAttendu, commande.NombreArticles);
        }

    }
}

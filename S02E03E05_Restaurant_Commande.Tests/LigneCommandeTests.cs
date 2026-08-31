using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Tests
{
    public class LigneCommandeTests
    {
        [Fact]
        public void CalculerTotal_LigneSansRabais_Calcul29()
        {
            //Arrange
            decimal prixAttendu = 29.00m;
            LigneCommande ligneCommande = new("POUT23", "Poutine Délice", 14.50m, 2, 0m);

            //Act
            decimal total = ligneCommande.CalculerTotal();

            //Assert
            Assert.Equal(prixAttendu, total);
        }

        [Theory]
        [InlineData(10.00f, 1, 0f, 10.00f)]
        [InlineData(12.50f, 2, 10f, 22.50f)]
        [InlineData(3.25f, 4, 20f, 10.40f)]
        [InlineData(10.00f, 2, 100f, 0.00f)]
        public void CalculerTotal_DonneesValides_CalculMontantTotal(
            double prixUnitaire,
            int quantite,
            double rabais,
            double prixAttendu)
        {
            //Arrange
            LigneCommande ligneCommande = new("ERR0R", "Repas non-dit", (decimal)prixUnitaire, quantite, (decimal)rabais);

            //Act
            decimal total = ligneCommande.CalculerTotal();

            //Assert
            Assert.Equal((decimal)prixAttendu, total, 1);
        }
    }
}

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

        [Fact]
        public void LigneCommande_CodePlatVide_Erreur()
        {
            //Arrange
            Action value = () => new LigneCommande("", "description", 0.0m, 0, 0m);

            //Act / Assert
            Assert.Throws<ArgumentException>(value);
        }

        [Theory]
        //Pas de code
        [InlineData("", "descriptionValide")]
        //Pas de description
        [InlineData("CodeBon", "")]
        public void LigneCommande_ParamConstructeurStringInvalides_Erreur(
            string code,
            string description)
        {
            //Arrange
            Action value = () => new LigneCommande(code, description, 2.0m, 1, 0.0m);

            //Act / Assert
            Assert.Throws<ArgumentException>(value);
        }

        [Theory]
        //Prix unit négatif
        [InlineData( -2.0, 1, 0.0)]
        //Quantité nulle
        [InlineData( 2.0, 0, 0.0)]
        //Rabais
        [InlineData( 2.0, 1, 101.0)]
        [InlineData( 2.0, 1, -1.0)]
        public void LigneCommande_ParamConstructeurNumeriqueInvalides_Erreur(
            double prixUnit,
            int quantite,
            double rabais)
        {
            //Arrange
            Action value = () => new LigneCommande("CodeBon", "DescriptionValide", (decimal)prixUnit, quantite, (decimal)rabais);

            //Act / Assert
            Assert.Throws<ArgumentOutOfRangeException>(value);
        }
    }
}

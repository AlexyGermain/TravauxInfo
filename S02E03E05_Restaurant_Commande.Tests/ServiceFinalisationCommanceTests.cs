using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace Restaurant.Tests
{
    public class ServiceFinalisationCommanceTests
    {
        [Fact]
        public void Finaliser_CommandeValide_CommandeFinaliser()
        {
            //Arrange
            Commande commande = new("C-1042");

            Mock<IPasserellePaiement> mockPasserellePaiement = new Mock<IPasserellePaiement>();
            Mock<IDisponibilitePlats> mockDisponibilitePlat = new Mock<IDisponibilitePlats>();
            Mock<IExpediteurCuisine>  mockExpediteurCuisine = new Mock<IExpediteurCuisine>();

            //Act
            ServiceFinalisationCommande serviceFinalisation = new(
                mockDisponibilitePlat.Object,
                mockPasserellePaiement.Object,
                mockExpediteurCuisine.Object);

            commande.AjouterLigne(new LigneCommande("POU-01", "POUTINE", 12.00m, 2, 0m));
            commande.AjouterLigne(new LigneCommande("SOU-01", "SOUPE", 6.00m, 1, 0m));

            mockPasserellePaiement
                .Setup(d => d.Autoriser("C-1042", It.Is<decimal>(montant => montant == 30.00m)))
                .Returns(true);

            mockDisponibilitePlat
                .Setup(d => d.EstDisponible("POU-01", 2))
                .Returns(true);

            mockDisponibilitePlat
                .Setup(d => d.EstDisponible("SOU-01", 1))
                .Returns(true);

            ResultatFinalisationCommande resultatFinalisation = serviceFinalisation.Finaliser(commande);

            //Assert
            Assert.Equal(ResultatFinalisationCommande.CommandeConfirmee, resultatFinalisation);

            mockDisponibilitePlat.Verify(disponibilite => disponibilite.EstDisponible("POU-01", 2), Times.Once);
            mockDisponibilitePlat.Verify(disponibilite => disponibilite.EstDisponible("SOU-01", 1), Times.Once);
            mockDisponibilitePlat.VerifyNoOtherCalls();

            mockPasserellePaiement.Verify(paiement => paiement.Autoriser("C-1042", It.Is<decimal>(montant => montant == 30.00m)), Times.Once);
            mockPasserellePaiement.VerifyNoOtherCalls();

            mockExpediteurCuisine.Verify(expediteur => expediteur.Envoyer("C-1042", 3, 30.00m));
            mockExpediteurCuisine.VerifyNoOtherCalls();
        }

        [Fact]
        public void Finaliser_PremierPlatNonDispo_CommandePasFinaliser()
        {
            //Arrange
            Commande commande = new("C-1042");

            Mock<IPasserellePaiement> mockPasserellePaiement = new Mock<IPasserellePaiement>();
            Mock<IDisponibilitePlats> mockDisponibilitePlat = new Mock<IDisponibilitePlats>();
            Mock<IExpediteurCuisine> mockExpediteurCuisine = new Mock<IExpediteurCuisine>();

            //Act
            ServiceFinalisationCommande serviceFinalisation = new(
                mockDisponibilitePlat.Object,
                mockPasserellePaiement.Object,
                mockExpediteurCuisine.Object);

            commande.AjouterLigne(new LigneCommande("POU-01", "POUTINE", 12.00m, 2, 0m));
            commande.AjouterLigne(new LigneCommande("SOU-01", "SOUPE", 6.00m, 1, 0m));

            mockPasserellePaiement
                .Setup(d => d.Autoriser("C-1042", 30.00m))
                .Returns(true);

            mockDisponibilitePlat
                .Setup(d => d.EstDisponible("POU-01", 2))
                .Returns(false);

            mockDisponibilitePlat
                .Setup(d => d.EstDisponible("SOU-01", 1))
                .Returns(true);

            ResultatFinalisationCommande resultatFinalisation = serviceFinalisation.Finaliser(commande);

            //Assert
            Assert.Equal(ResultatFinalisationCommande.PlatIndisponible, resultatFinalisation);

            mockDisponibilitePlat.Verify(disponibilite => disponibilite.EstDisponible("POU-01", 2), Times.Once);
            mockDisponibilitePlat.Verify(disponibilite => disponibilite.EstDisponible("SOU-01", 1), Times.Never);
            mockDisponibilitePlat.VerifyNoOtherCalls();

            mockPasserellePaiement.Verify(paiement => paiement.Autoriser("C-1042", 30.00m), Times.Never);
            mockPasserellePaiement.VerifyNoOtherCalls();

            mockExpediteurCuisine.Verify(expediteur => expediteur.Envoyer("C-1042", 3, 30.00m), Times.Never);
            mockExpediteurCuisine.VerifyNoOtherCalls();
        }

        [Fact]
        public void Finaliser_PaiementRefuse_CommandePasFinaliser()
        {
            //Arrange
            Commande commande = new("C-1042");

            Mock<IPasserellePaiement> mockPasserellePaiement = new Mock<IPasserellePaiement>();
            Mock<IDisponibilitePlats> mockDisponibilitePlat = new Mock<IDisponibilitePlats>();
            Mock<IExpediteurCuisine> mockExpediteurCuisine = new Mock<IExpediteurCuisine>();

            //Act
            ServiceFinalisationCommande serviceFinalisation = new(
                mockDisponibilitePlat.Object,
                mockPasserellePaiement.Object,
                mockExpediteurCuisine.Object);

            commande.AjouterLigne(new LigneCommande("POU-01", "POUTINE", 12.00m, 2, 0m));
            commande.AjouterLigne(new LigneCommande("SOU-01", "SOUPE", 6.00m, 1, 0m));

            mockPasserellePaiement
                .Setup(d => d.Autoriser("C-1042", 30.00m))
                .Returns(false);

            mockDisponibilitePlat
                .Setup(d => d.EstDisponible("POU-01", 2))
                .Returns(true);

            mockDisponibilitePlat
                .Setup(d => d.EstDisponible("SOU-01", 1))
                .Returns(true);

            ResultatFinalisationCommande resultatFinalisation = serviceFinalisation.Finaliser(commande);

            //Assert
            Assert.Equal(ResultatFinalisationCommande.PaiementRefuse, resultatFinalisation);

            mockDisponibilitePlat.Verify(disponibilite => disponibilite.EstDisponible("POU-01", 2), Times.Once);
            mockDisponibilitePlat.Verify(disponibilite => disponibilite.EstDisponible("SOU-01", 1), Times.Once);
            mockDisponibilitePlat.VerifyNoOtherCalls();

            mockPasserellePaiement.Verify(paiement => paiement.Autoriser("C-1042", It.Is<decimal>(montant => montant == 30.00m)), Times.Once);
            mockPasserellePaiement.VerifyNoOtherCalls();

            mockExpediteurCuisine.Verify(expediteur => expediteur.Envoyer("C-1042", 3, 30.00m), Times.Never);
            mockExpediteurCuisine.VerifyNoOtherCalls();
        }

        [Fact]
        public void Finaliser_CommandeVide_CommandePasFinaliser()
        {
            //Arrange
            Commande commande = new("C-1042");

            Mock<IPasserellePaiement> mockPasserellePaiement = new Mock<IPasserellePaiement>();
            Mock<IDisponibilitePlats> mockDisponibilitePlat = new Mock<IDisponibilitePlats>();
            Mock<IExpediteurCuisine> mockExpediteurCuisine = new Mock<IExpediteurCuisine>();

            //Act
            ServiceFinalisationCommande serviceFinalisation = new(
                mockDisponibilitePlat.Object,
                mockPasserellePaiement.Object,
                mockExpediteurCuisine.Object);

            Action action = () => serviceFinalisation.Finaliser(commande);

            //Assert
            Assert.Throws<InvalidOperationException>(action);

            mockDisponibilitePlat.Verify(disponibilite => disponibilite.EstDisponible("POU-01", 2), Times.Never);
            mockDisponibilitePlat.Verify(disponibilite => disponibilite.EstDisponible("SOU-01", 1), Times.Never);
            mockDisponibilitePlat.VerifyNoOtherCalls();

            mockPasserellePaiement.Verify(paiement => paiement.Autoriser("C-1042", 30.00m), Times.Never);
            mockPasserellePaiement.VerifyNoOtherCalls();

            mockExpediteurCuisine.Verify(expediteur => expediteur.Envoyer("C-1042", 3, 30.00m), Times.Never);
            mockExpediteurCuisine.VerifyNoOtherCalls();
        }


    }
}

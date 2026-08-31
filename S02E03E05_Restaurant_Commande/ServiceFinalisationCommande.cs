namespace Restaurant;

public sealed class ServiceFinalisationCommande
{
    private readonly IDisponibilitePlats m_disponibilitePlats;
    private readonly IPasserellePaiement m_passerellePaiement;
    private readonly IExpediteurCuisine m_expediteurCuisine;

    public ServiceFinalisationCommande(
        IDisponibilitePlats disponibilitePlats,
        IPasserellePaiement passerellePaiement,
        IExpediteurCuisine expediteurCuisine)
    {
        ArgumentNullException.ThrowIfNull(disponibilitePlats);
        ArgumentNullException.ThrowIfNull(passerellePaiement);
        ArgumentNullException.ThrowIfNull(expediteurCuisine);

        m_disponibilitePlats = disponibilitePlats;
        m_passerellePaiement = passerellePaiement;
        m_expediteurCuisine = expediteurCuisine;
    }

    public ResultatFinalisationCommande Finaliser(Commande commande)
    {
        ArgumentNullException.ThrowIfNull(commande);

        if (commande.EstVide)
        {
            throw new InvalidOperationException(
                "Une commande vide ne peut pas être finalisée.");
        }

        foreach (LigneCommande ligne in commande.Lignes)
        {
            bool estDisponible = m_disponibilitePlats.EstDisponible(
                ligne.CodePlat,
                ligne.Quantite);

            if (!estDisponible)
            {
                return ResultatFinalisationCommande.PlatIndisponible;
            }
        }

        decimal sousTotal = commande.SousTotal;
        bool paiementAccepte = m_passerellePaiement.Autoriser(
            commande.Numero,
            sousTotal);

        if (!paiementAccepte)
        {
            return ResultatFinalisationCommande.PaiementRefuse;
        }

        m_expediteurCuisine.Envoyer(
            commande.Numero,
            commande.NombreArticles,
            sousTotal);

        return ResultatFinalisationCommande.CommandeConfirmee;
    }
}

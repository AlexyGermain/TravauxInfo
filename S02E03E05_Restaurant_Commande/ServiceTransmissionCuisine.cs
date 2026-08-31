namespace Restaurant;

public sealed class ServiceTransmissionCuisine
{
    private readonly IExpediteurCuisine m_expediteurCuisine;

    public ServiceTransmissionCuisine(IExpediteurCuisine expediteurCuisine)
    {
        ArgumentNullException.ThrowIfNull(expediteurCuisine);
        m_expediteurCuisine = expediteurCuisine;
    }

    public void Transmettre(Commande commande)
    {
        ArgumentNullException.ThrowIfNull(commande);

        if (commande.EstVide)
        {
            throw new InvalidOperationException(
                "Une commande vide ne peut pas être transmise.");
        }

        m_expediteurCuisine.Envoyer(
            commande.Numero,
            commande.NombreArticles,
            commande.SousTotal);
    }
}

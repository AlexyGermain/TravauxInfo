namespace Restaurant;

public interface IExpediteurCuisine
{
    void Envoyer(
        string numeroCommande,
        int nombreArticles,
        decimal montantTotal);
}

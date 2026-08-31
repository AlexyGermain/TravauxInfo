namespace Restaurant;

public sealed class LigneCommande
{
    public LigneCommande(
        string codePlat,
        string description,
        decimal prixUnitaire,
        int quantite,
        decimal pourcentageRabais)
    {
        if (string.IsNullOrWhiteSpace(codePlat))
        {
            throw new ArgumentException(
                "Le code du plat est obligatoire.",
                nameof(codePlat));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException(
                "La description est obligatoire.",
                nameof(description));
        }

        if (prixUnitaire < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(prixUnitaire));
        }

        if (quantite <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantite));
        }

        if (pourcentageRabais < 0m || pourcentageRabais > 100m)
        {
            throw new ArgumentOutOfRangeException(nameof(pourcentageRabais));
        }

        CodePlat = codePlat;
        Description = description;
        PrixUnitaire = prixUnitaire;
        Quantite = quantite;
        PourcentageRabais = pourcentageRabais;
    }

    public string CodePlat { get; }

    public string Description { get; }

    public decimal PrixUnitaire { get; }

    public int Quantite { get; }

    public decimal PourcentageRabais { get; }

    public decimal CalculerTotal()
    {
        decimal montantBrut = PrixUnitaire * Quantite;
        decimal montantRabais = montantBrut * PourcentageRabais / 100m;
        return montantBrut - montantRabais;
    }
}

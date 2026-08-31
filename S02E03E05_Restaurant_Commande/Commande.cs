namespace Restaurant;

public sealed class Commande
{
    private readonly List<LigneCommande> m_lignes = new List<LigneCommande>();

    public Commande(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero))
        {
            throw new ArgumentException(
                "Le numéro de commande est obligatoire.",
                nameof(numero));
        }

        Numero = numero;
    }

    public string Numero { get; }

    public IReadOnlyList<LigneCommande> Lignes
    {
        get
        {
            return m_lignes.AsReadOnly();
        }
    }

    public bool EstVide
    {
        get
        {
            return m_lignes.Count == 0;
        }
    }

    public void AjouterLigne(LigneCommande ligne)
    {
        ArgumentNullException.ThrowIfNull(ligne);
        m_lignes.Add(ligne);
    }

    public decimal SousTotal
    {
        get
        {
            return m_lignes.Sum(ligne => ligne.CalculerTotal());
        }
    }

    public int NombreArticles
    {
        get {
            return m_lignes.Sum(ligne => ligne.Quantite);
        }
    }
}

namespace Restaurant;

public interface IPasserellePaiement
{
    bool Autoriser(string numeroCommande, decimal montant);
}

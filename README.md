# Projet de départ — Tests du restaurant

Cette solution sert aux trois exercices de tests du restaurant :

- E03 : tests sans dépendance;
- E04 : simulacre manuel de l'expéditeur cuisine;
- E05 : cas intégrateur avec Moq.

Le projet `S02E03E05_Restaurant_Commande` contient tout le code de production à
tester. `S02E03E05_Restaurant_Commande.Terminal` permet de valider rapidement
la référence vers ce projet. `S02E03E05_Restaurant_Commande.Tests` référence
xUnit, mais ne contient volontairement aucun test. Les étudiants ajoutent leurs
tests progressivement sans modifier le code de production.

## Démarrage

### À partir de Visual Studio

- Compilez la solution : allez dans le menu `Générer` puis `Générer la solution`.
- Exécutez les tests de la solutions : allez dans le menu `Tests` puis `Exécuter tous les tests`

### Ou en ligne de commandes

Depuis ce répertoire :

```bash
dotnet restore
dotnet build
dotnet test
```

L'énoncé complet et les contraintes de chaque étape se trouvent dans le
[`README.md` de la semaine 2](../README.md).

L'exercice E02 utilise une mini-solution indépendante pour découvrir Moq.
N'ajoutez toutefois le paquet Moq à
`S02E03E05_Restaurant_Commande.Tests` qu'au début de E05.

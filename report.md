# Rapport du TP Caisse Automatique

## Partie 1 : Mise en place de l'automate

*(Ce fichier sera complété au fil des questions du TP.)*

### Travail réalisé sur l'automate
- ajout des évènements **SCAN**, **PAYE** et **RESET** dans l'automate
- création des états `EtatAttenteArticle` et `EtatFin`
- modification de `EtatAttenteClient` pour passer sur `EtatAttenteArticle` après un premier scan
- mise en place d'un timer dans `EtatFin` pour déclencher l'évènement `RESET` automatiquement
- ajout de l'accès à la `Caisse` depuis l'automate afin que les états puissent appeler les méthodes métier

### Gestion de la balance
- ajout de l'évènement **DEPOSE** puis **RETIRE** pour signaler les interactions avec la balance
- création des états `EtatAttenteDepot` et `EtatProblemePoids`
- mise à jour des transitions pour vérifier le poids après chaque action sur la balance

### Travail réalisé sur la classe `Caisse`
- ajout des méthodes `PoserArticleSurBalance` et `EnleverArticleDeLaBalance` qui mettent à jour `PoidsBalance`
- ajout de `EnregistrerArticle`, `Payer` et `Reset` pour gérer respectivement l'enregistrement d'un article, le paiement complet et la remise à zéro

### Travail réalisé sur le `VueModel`
- lors du scan d'un article ou du paiement, appel des nouvelles méthodes du métier puis activation des évènements de l'automate
- prise en compte du reset pour remettre la vue dans son état initial

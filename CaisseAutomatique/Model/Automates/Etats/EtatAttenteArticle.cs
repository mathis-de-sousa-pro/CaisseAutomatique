using CaisseAutomatique.Model.Automates;

namespace CaisseAutomatique.Model.Automates.Etats
{
    /// <summary>
    /// Etat après le premier scan, en attente des scans suivants ou du paiement
    /// </summary>
    public class EtatAttenteArticle : Etat
    {
        public EtatAttenteArticle(Automate automate) : base(automate)
        {
        }

        public override void Action(Evenement evt)
        {
            // aucune action spécifique
        }

        public override Etat Transition(Evenement evt)
        {
            return evt switch
            {
                Evenement.SCAN => new EtatAttenteDepot(this.automate),
                Evenement.PAYE => new EtatFin(this.automate),
                Evenement.DEPOSE or Evenement.RETIRE =>
                    this.automate.Caisse.PoidsBalance != this.automate.Caisse.PoidsAttendu
                        ? new EtatProblemePoids(this.automate, this)
                        : this,
                _ => this
            };
        }

        public override string Message => "Scannez le produit suivant !";
    }
}

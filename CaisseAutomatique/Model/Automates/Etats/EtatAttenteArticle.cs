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
            if (evt == Evenement.SCAN && this.automate.Caisse.DernierArticleScanne != null)
            {
                if (!this.automate.Caisse.DernierArticleScanne.IsDenombrable)
                {
                    this.automate.Caisse.EnregistrerArticle(this.automate.Caisse.DernierArticleScanne);
                }
                else
                {
                    NotifyPropertyChanged("ScanArticleDenombrable");
                }
            }
        }

        public override Etat Transition(Evenement evt)
        {
            if (evt == Evenement.SCAN)
            {
                if (this.automate.Caisse.DernierArticleScanne != null && this.automate.Caisse.DernierArticleScanne.IsDenombrable)
                {
                    return new EtatSaisieQuantite(this.automate);
                }
                return new EtatAttenteDepot(this.automate);
            }
            else if (evt == Evenement.PAYE)
            {
                return new EtatFin(this.automate);
            }
            else if (evt == Evenement.DEPOSE || evt == Evenement.RETIRE)
            {
                if (this.automate.Caisse.PoidsBalance != this.automate.Caisse.PoidsAttendu)
                {
                    return new EtatProblemePoids(this.automate, this);
                }
            }
            return this;
        }

        public override string Message => "Scannez le produit suivant !";
    }
}

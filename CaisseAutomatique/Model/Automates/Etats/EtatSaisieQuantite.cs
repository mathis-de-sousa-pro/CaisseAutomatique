using CaisseAutomatique.Model.Automates;

namespace CaisseAutomatique.Model.Automates.Etats
{
    /// <summary>
    /// Etat de saisie de la quantité pour un article dénombrable
    /// </summary>
    public class EtatSaisieQuantite : Etat
    {
        public EtatSaisieQuantite(Automate automate) : base(automate)
        {
        }

        public override void Action(Evenement evt)
        {
            NotifierAdministration(evt);
            if (evt == Evenement.SAISIEQUANTITE && this.automate.Caisse.DernierArticleScanne != null)
            {
                this.automate.Caisse.EnregistrerArticle(this.automate.Caisse.DernierArticleScanne, this.automate.Caisse.QuantiteSaise);
            }
        }

        public override Etat Transition(Evenement evt)
        {
            if (evt == Evenement.SAISIEQUANTITE)
            {
                return new EtatAttenteDepot(this.automate);
            }
            else if (evt == Evenement.DEPOSE || evt == Evenement.RETIRE)
            {
                if (this.automate.Caisse.PoidsBalance != this.automate.Caisse.PoidsAttendu)
                {
                    return new EtatProblemePoids(this.automate, this);
                }
            }
            else if (evt == Evenement.ADMIN)
            {
                return this;
            }
            return this;
        }

        public override string Message => "Saisissez la quantité";
    }
}

using CaisseAutomatique.Model.Automates;

namespace CaisseAutomatique.Model.Automates.Etats
{
    /// <summary>
    /// Etat attendant que le client depose l'article scanne sur la balance
    /// </summary>
    public class EtatAttenteDepot : Etat
    {
        public EtatAttenteDepot(Automate automate) : base(automate)
        {
        }

        public override void Action(Evenement evt)
        {
            NotifierAdministration(evt);
        }

        public override Etat Transition(Evenement evt)
        {
            if (evt == Evenement.DEPOSE || evt == Evenement.RETIRE)
            {
                if (this.automate.Caisse.PoidsBalance == this.automate.Caisse.PoidsAttendu)
                {
                    return new EtatAttenteArticle(this.automate);
                }
                return new EtatProblemePoids(this.automate, this);
            }
            else if (evt == Evenement.PAYE)
            {
                return new EtatFin(this.automate);
            }
            else if (evt == Evenement.SCAN)
            {
                return this;
            }
            else if (evt == Evenement.ADMIN)
            {
                return this;
            }
            return this;
        }

        public override string Message => "Déposez l'article sur la balance";
    }
}

using CaisseAutomatique.Model.Automates;

namespace CaisseAutomatique.Model.Automates.Etats
{
    /// <summary>
    /// Etat signalant un probleme de poids sur la balance
    /// </summary>
    public class EtatProblemePoids : Etat
    {
        private Etat retour;

        public EtatProblemePoids(Automate automate, Etat retour) : base(automate)
        {
            this.retour = retour;
        }

        public override void Action(Evenement evt)
        {
            // aucune action
        }

        public override Etat Transition(Evenement evt)
        {
            if (evt == Evenement.DEPOSE || evt == Evenement.RETIRE)
            {
                if (this.automate.Caisse.PoidsBalance == this.automate.Caisse.PoidsAttendu)
                {
                    return retour;
                }
            }
            else if (evt == Evenement.SCAN || evt == Evenement.PAYE)
            {
                // ignore events while problem persists
            }
            return this;
        }

        public override string Message => "Problème poids";
    }
}

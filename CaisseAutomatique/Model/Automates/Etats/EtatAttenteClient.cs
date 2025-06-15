namespace CaisseAutomatique.Model.Automates.Etats
{
    /// <summary>
    /// Etat initial : attente du premier client
    /// </summary>
    public class EtatAttenteClient : Etat
    {
        public EtatAttenteClient(Automate automate) : base(automate)
        {
        }

        public override void Action(Evenement evt)
        {
            // Pas d'action pour le moment
        }

        public override Etat Transition(Evenement evt)
        {
            if (evt == Evenement.SCAN)
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
            return this;
        }

        public override string Message => "Bonjour, scannez votre premier article !";
    }
}

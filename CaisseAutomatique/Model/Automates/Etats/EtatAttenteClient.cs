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
            // Pour l'instant, la transition revient sur le même état
            return new EtatAttenteClient(this.automate);
        }

        public override string Message => "Bonjour, scannez votre premier article !";
    }
}

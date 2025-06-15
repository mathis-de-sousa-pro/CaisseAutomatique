using System.Timers;
using System.Windows;

namespace CaisseAutomatique.Model.Automates.Etats
{
    /// <summary>
    /// Etat final affichant au revoir puis déclenchant un reset automatique
    /// </summary>
    public class EtatFin : Etat
    {
        private static Timer? timer = null;

        public EtatFin(Automate automate) : base(automate)
        {
            if (timer == null)
            {
                timer = new Timer(2000);
                timer.Elapsed += Timer_Elapsed;
                timer.AutoReset = false;
                timer.Start();
            }
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            timer?.Stop();
            timer = null;
            Application.Current.Dispatcher.Invoke(() => this.automate.Activer(Evenement.RESET));
        }

        public override void Action(Evenement evt)
        {
            if (evt == Evenement.RESET)
            {
                this.automate.Caisse.Reset();
            }
        }

        public override Etat Transition(Evenement evt)
        {
            if (evt == Evenement.RESET)
            {
                return new EtatAttenteClient(this.automate);
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

        public override string Message => "Au revoir";
    }
}

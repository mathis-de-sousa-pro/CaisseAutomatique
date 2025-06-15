using System;
using CaisseAutomatique.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CaisseAutomatique.Model.Automates
{
    /// <summary>
    /// Automate gérant les états de la caisse automatique
    /// </summary>
    public class Automate : INotifyPropertyChanged
    {
        private Etat etatCourant;
        public Etat EtatCourant { get => etatCourant; private set => etatCourant = value; }

        private Caisse caisse;
        public Caisse Caisse => caisse;

        /// <summary>
        /// Message actuel de la caisse (donné par l'état courant)
        /// </summary>
        public string Message => this.etatCourant.Message;

        public Automate(Caisse caisse)
        {
            this.caisse = caisse;
            this.etatCourant = new Etats.EtatAttenteClient(this);
        }

        /// <summary>
        /// Active l'automate avec un évènement
        /// </summary>
        /// <param name="evt">évènement reçu</param>
        public void Activer(Evenement evt)
        {
            this.etatCourant.Action(evt);
            this.etatCourant = this.etatCourant.Transition(evt);
            NotifyPropertyChanged(nameof(Message));
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

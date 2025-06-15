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
            this.etatCourant.PropertyChanged += EtatCourant_PropertyChanged;
        }

        /// <summary>
        /// Active l'automate avec un évènement
        /// </summary>
        /// <param name="evt">évènement reçu</param>
        public void Activer(Evenement evt)
        {
            this.etatCourant.Action(evt);
            Etat nouvelEtat = this.etatCourant.Transition(evt);
            if (nouvelEtat != this.etatCourant)
            {
                this.etatCourant.PropertyChanged -= EtatCourant_PropertyChanged;
                nouvelEtat.PropertyChanged += EtatCourant_PropertyChanged;
            }
            this.etatCourant = nouvelEtat;
            NotifyPropertyChanged(nameof(Message));
        }

        private void EtatCourant_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ScanArticleDenombrable")
                NotifyPropertyChanged("ScanArticleDenombrable");
            else if (e.PropertyName == "DemandeAdministration")
                NotifyPropertyChanged("DemandeAdministration");
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

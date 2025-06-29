using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CaisseAutomatique.Model.Automates
{
    /// <summary>
    /// Etat abstrait de l'automate
    /// </summary>
    public abstract class Etat : INotifyPropertyChanged
    {
        /// <summary>
        /// Automate possédant l'état
        /// </summary>
        protected Automate automate;

        protected Etat(Automate automate)
        {
            this.automate = automate;
        }

        /// <summary>
        /// Action réalisée à la réception d'un évènement
        /// </summary>
        /// <param name="evt">Evenement reçu</param>
        public abstract void Action(Evenement evt);

        /// <summary>
        /// Etat suivant à la réception d'un évènement
        /// </summary>
        /// <param name="evt">Evenement reçu</param>
        /// <returns>Etat suivant</returns>
        public abstract Etat Transition(Evenement evt);

        /// <summary>
        /// Message associé à l'état
        /// </summary>

        public abstract string Message { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notification d'ouverture du mode administrateur
        /// </summary>
        /// <param name="evt">Evènement reçu</param>
        protected void NotifierAdministration(Evenement evt)
        {
            if (evt == Evenement.ADMIN)
            {
                NotifyPropertyChanged("DemandeAdministration");
            }
        }
    }
}

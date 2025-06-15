using System;

namespace CaisseAutomatique.Model.Automates
{
    /// <summary>
    /// Etat abstrait de l'automate
    /// </summary>
    public abstract class Etat
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
    }
}

namespace CaisseAutomatique.Model.Automates
{
    /// <summary>
    /// Evenements pouvant être traités par l'automate.
    /// Pour le moment, aucun évènement n'est défini.
    /// </summary>
    public enum Evenement
    {
        SCAN,
        PAYE,
        RESET,
        DEPOSE,
        RETIRE,
        SAISIEQUANTITE
    }
}

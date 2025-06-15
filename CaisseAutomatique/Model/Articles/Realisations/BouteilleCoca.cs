using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Articles.Realisations
{
    /// <summary>
    /// Une bouteille de Coca 2L
    /// </summary>
    public class BouteilleCoca : Article
    {
        public string Nom => "Bouteille de Coca";

        public double Prix => 1.95;

        public string Reference => "512345319871";

        public double Poids => 2.00;

        public bool IsDenombrable => true;

        public string NomImage => "Coca";

        public int Hauteur => 110;
    }
}

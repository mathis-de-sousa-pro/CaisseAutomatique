using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Articles.Realisations
{
    /// <summary>
    /// Une bouteille de Lait 1L
    /// </summary>
    public class BouteilleLait : Article
    {
        public string Nom => "Bouteille de lait";

        public double Prix => 1.35;

        public string Reference => "435876124998";

        public double Poids => 1;

        public bool IsDenombrable => true;

        public string NomImage => "Lait";

        public int Hauteur => 110;
    }
}

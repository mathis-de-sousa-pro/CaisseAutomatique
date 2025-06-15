using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Articles.Realisations
{
    /// <summary>
    /// Une boite de pâtes 
    /// </summary>
    public class BoitePates : Article
    {
        public string Nom => "Penne Rigate";

        public double Prix => 1.44;

        public string Reference => "2135498451102";

        public double Poids => 0.5;

        public bool IsDenombrable => false;

        public string NomImage => "Pates";

        public int Hauteur => 90;
    }
}

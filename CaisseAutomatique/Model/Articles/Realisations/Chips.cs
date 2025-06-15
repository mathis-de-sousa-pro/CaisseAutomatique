using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Articles.Realisations
{
    /// <summary>
    /// Paquet de chips nature
    /// </summary>
    public class Chips : Article
    {
        public string Nom => "Paquet de chips";

        public double Prix => 1.55;

        public string Reference => "345618739462";

        public double Poids => 0.150;

        public bool IsDenombrable => false;

        public string NomImage => "Chips";

        public int Hauteur => 70;
    }
}

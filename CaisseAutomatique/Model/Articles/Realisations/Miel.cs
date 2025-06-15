using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Articles.Realisations
{
    /// <summary>
    /// Pot de miel
    /// </summary>
    public class Miel : Article
    {
        public string Nom => "Pot de miel";

        public double Prix => 8.75;

        public string Reference => "123184196724";

        public double Poids => 0.5;

        public bool IsDenombrable => false;

        public string NomImage => "Miel";

        public int Hauteur => 70;
    }
}

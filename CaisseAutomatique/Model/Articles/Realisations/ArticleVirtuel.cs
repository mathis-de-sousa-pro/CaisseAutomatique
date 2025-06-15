using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Articles.Realisations
{
    /// <summary>
    /// Article virtuel pouvant servir pour de l'IHM, ne pas modifier
    /// </summary>
    public class ArticleVirtuel : Article
    {
        private string nom;
        private double prix;

        public ArticleVirtuel(String nom, double prix)
        {
            this.nom = nom;
            this.prix = prix;
        }

        public string Nom => nom;

        public double Prix => prix;

        public string Reference => "-";

        public double Poids => 0;

        public bool IsDenombrable => false;

        public string NomImage => "";

        public int Hauteur => 0;
    }
}

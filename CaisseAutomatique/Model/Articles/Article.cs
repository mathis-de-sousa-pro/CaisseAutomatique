using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Articles
{
    /// <summary>
    /// Un article scannable par la caisse
    /// </summary>
    public interface Article
    {
        /// <summary>
        /// Nom de l'article
        /// </summary>
        string Nom { get; }

        /// <summary>
        /// Prix de l'article en euro
        /// </summary>
        double Prix { get; }

        /// <summary>
        /// Référence de l'article (obtenue au scan)
        /// </summary>
        string Reference { get; }

        /// <summary>
        /// Poids de l'article en kg
        /// </summary>
        double Poids { get; }
        
        /// <summary>
        /// L'article est-il dénombrable
        /// </summary>
        Boolean IsDenombrable { get; }

        /// <summary>
        /// Nom de l'image (sans extension)
        /// </summary>
        string NomImage { get; }

        /// <summary>
        /// Hauteur de l'image
        /// </summary>
        int Hauteur { get; }
    }
}

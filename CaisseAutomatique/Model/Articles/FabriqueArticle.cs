using CaisseAutomatique.Model.Articles.Realisations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Articles
{
    /// <summary>
    /// Fabrique pour les articles
    /// </summary>
    public class FabriqueArticle
    {
        /// <summary>
        /// Crée un article de type donné
        /// </summary>
        /// <param name="type">Type de l'article désiré</param>
        /// <returns>Artice du type demandé</returns>
        public static Article Creer(TypeArticle type)
        {
            Article article = null;
            switch (type)
            {
                case TypeArticle.COCA: article = new BouteilleCoca(); break;
                case TypeArticle.PATES: article = new BoitePates(); break;
                case TypeArticle.LAIT: article = new BouteilleLait(); break;
                case TypeArticle.CHIPS: article = new Chips(); break;
                case TypeArticle.MIEL: article = new Miel(); break;
            }
            return article;
        }
    }
}

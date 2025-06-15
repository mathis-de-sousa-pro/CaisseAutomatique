using CaisseAutomatique.Model.Articles;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CaisseAutomatique.Vue
{
    /// <summary>
    /// Vue d'un article
    /// </summary>
    public class VueArticle : Image
    {
        /// <summary>
        /// Ecran principal
        /// </summary>
        private MainWindow window;

        /// <summary>
        /// L'article (Métier)
        /// </summary>
        public Article Article => article;
        private Article article;

        /// <summary>
        /// La vue est-elle active (réceptive au clic)
        /// </summary>
        private bool isActif;

        /// <summary>
        /// La vue de l'article est actuellement sur la balance
        /// </summary>
        public bool EstSurBalance { get => estSurBalance; set => estSurBalance = value; }
        private bool estSurBalance;
        public VueArticle(MainWindow window, Article article)
        {
            this.article = article;
            Source = new BitmapImage(new Uri(@"Ressources/"+article.NomImage+".png", UriKind.RelativeOrAbsolute));
            Height = article.Hauteur;
            this.window = window;
            this.isActif = true;
            this.MouseDown += VueArticle_MouseDown;
            this.estSurBalance = false;
        }

        /// <summary>
        /// Rend la vue réactive au clic
        /// </summary>
        public void Active()
        {
            this.isActif = true;
            this.IsHitTestVisible = true;
        }

        /// <summary>
        /// Désactive la vue au clic
        /// </summary>
        public void Desactive()
        {
            this.isActif = false;
            this.IsHitTestVisible = false;
        }

        /// <summary>
        /// Constructeur par copie
        /// </summary>
        /// <param name="model">Modèle pour la copie</param>
        public VueArticle(VueArticle model) : this(model.window,model.article)
        {
            this.isActif = false;
            this.IsHitTestVisible = false;
        }

        /// <summary>
        /// Clic sur la vue
        /// </summary>
        /// <param name="sender">La vue</param>
        /// <param name="e">Le clic</param>
        private void VueArticle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.isActif)
            {
                e.Handled = true;
                window.SelectionnerArticle(this);
            }
        }
    }
}

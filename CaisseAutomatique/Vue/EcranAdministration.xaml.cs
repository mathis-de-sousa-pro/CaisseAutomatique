using CaisseAutomatique.VueModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CaisseAutomatique.Vue
{
    /// <summary>
    /// Logique d'interaction pour EcranAdministration.xaml
    /// </summary>
    public partial class EcranAdministration : Window
    {
        //VueModel
        private VMCaisse vueModel;
        public EcranAdministration(VMCaisse vueModel)
        {
            this.vueModel = vueModel;
            this.Closed += (s, e) => Quitter();
            InitializeComponent();
        }

        /// <summary>
        /// Indique au vueModel de la fin du mode admin
        /// </summary>
        private void Quitter()
        {
            this.vueModel.FinModeAdministration();
        }

        /// <summary>
        /// Clic sur le bouton quitter
        /// </summary>
        /// <param name="sender">Le bouton</param>
        /// <param name="e">Le clic</param>
        private void Quitter_Click(object sender, RoutedEventArgs e)
        {
            this.Quitter();
            this.Close();
        }

        /// <summary>
        /// Clic sur le bouton AnnulerUnArticle
        /// </summary>
        /// <param name="sender">Le bouton</param>
        /// <param name="e">Le clic</param>
        private void AnnulerUnArticle_Click(object sender, RoutedEventArgs e)
        {
            this.vueModel.AnnuleDernierArticle();
        }

        /// <summary>
        /// Clic sur le bouton AnnulerTousLesArticles
        /// </summary>
        /// <param name="sender">Le bouton</param>
        /// <param name="e">Le clic</param>
        private void AnnulerTousLesArticles_Click(object sender, RoutedEventArgs e)
        {
            this.vueModel.AnnuleTousLesArticles();
        }
    }
}

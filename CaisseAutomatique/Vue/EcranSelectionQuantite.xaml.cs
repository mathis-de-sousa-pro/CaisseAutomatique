using CaisseAutomatique.VueModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour EcranSelectionQuantite.xaml
    /// </summary>
    public partial class EcranSelectionQuantite : Window
    {
        //VueModel
        private VMCaisse vueModel;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="vueModel">vueModel</param>
        public EcranSelectionQuantite(VMCaisse vueModel)
        {
            this.vueModel = vueModel;
            this.Closed += (s, e) => Valider();
            InitializeComponent();
        }

        /// <summary>
        /// Saisie dans la zone de texte pour la quantité
        /// </summary>
        /// <param name="sender">Zone</param>
        /// <param name="e">Saisie</param>
        private void ZoneSaisie_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string newText = this.ZoneSaisie.Text + e.Text;
            e.Handled = !IsEntreeValide(newText); 
        }

        /// <summary>
        /// La saisie est-elle valide (numérique non vide)
        /// </summary>
        /// <param name="text">La saisie</param>
        /// <returns>Validité de la saisie</returns>
        private bool IsEntreeValide(string text)
        {
            return new Regex("^([0-9]?){3}$").IsMatch(text);
        }

        /// <summary>
        /// L'utilisateur passe sa souris sur le bouton valider
        /// </summary>
        /// <param name="sender">Le bouton</param>
        /// <param name="e">Le survol</param>
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.ZoneSaisie.Text == "" || Convert.ToInt32(this.ZoneSaisie.Text) == 0) this.ZoneSaisie.Text = "1";
            FocusManager.SetFocusedElement(this, BoutonValider);
        }

        /// <summary>
        /// Valide auprès du vueModel
        /// </summary>
        private void Valider()
        {
            this.vueModel.SaisirNombreArticle(Convert.ToInt32(this.ZoneSaisie.Text));
        }

        /// <summary>
        /// Clic sur le bouton valider
        /// </summary>
        /// <param name="sender">Le bouton</param>
        /// <param name="e">Le clic</param>
        private void BoutonValider_Click(object sender, RoutedEventArgs e)
        {
            this.Valider();
            this.Close();
        }
    }
}

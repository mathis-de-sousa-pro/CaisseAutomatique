using CaisseAutomatique.Model.Articles;
using CaisseAutomatique.Model.Articles.Realisations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaisseAutomatique.Vue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// vueArticle pour le drag and drop
        /// </summary>
        private VueArticle vueArticleSelectionne;

        /// <summary>
        /// Vue model
        /// </summary>
        private VMCaisse vueModel;

        /// <summary>
        /// Liste des vues des articles
        /// </summary>
        private List<VueArticle> vueArticles;

        /// <summary>
        /// Constructeur
        /// </summary>
        public MainWindow()
        {
            //VueModel
            this.vueModel = new VMCaisse();
            this.DataContext = this.vueModel;

            //Liste des vueArticles
            this.vueArticles = new List<VueArticle>();

            //Initialisation des composants
            InitializeComponent();

            //Apparition du fond gris des zones
            GridSelectionZoneArticlesAVenir.MouseEnter += (s, e) => MouseInZoneSelection(GridSelectionZoneArticlesAVenirBackground,true);
            GridSelectionZoneArticlesAVenir.MouseLeave += (s, e) => MouseOutZoneSelection(GridSelectionZoneArticlesAVenirBackground, true);
            GridSelectionZoneArticlesEnregistres.MouseEnter += (s, e) => MouseInZoneSelection(GridSelectionZoneArticlesEnregistresBackground, true);
            GridSelectionZoneArticlesEnregistres.MouseLeave += (s, e) => MouseOutZoneSelection(GridSelectionZoneArticlesEnregistresBackground, true);
            GridSelectionZonePaiement.MouseEnter += (s, e) => MouseInZoneSelection(GridSelectionZonePaiement);
            GridSelectionZonePaiement.MouseLeave += (s, e) => MouseOutZoneSelection(GridSelectionZonePaiement);
            GridSelectionZoneScan.MouseEnter += (s, e) => MouseInZoneSelection(GridSelectionZoneScan);
            GridSelectionZoneScan.MouseLeave += (s, e) => MouseOutZoneSelection(GridSelectionZoneScan);

            //Drag and Drop
            GridSelectionZoneArticlesEnregistres.MouseDown += GridSelectionZoneArticlesEnregistres_MouseDown;
            GridSelectionZoneArticlesAVenir.MouseDown += GridSelectionZoneArticlesAVenir_MouseDown;
            this.vueArticleSelectionne = null;

            //Clics
            GridSelectionZonePaiement.MouseDown += GridSelectionZonePaiement_MouseDown;
            GridSelectionZoneScan.MouseDown += GridSelectionZoneScan_MouseDown;
        }

        /// <summary>
        /// Clic sur la zone de scan => début du mode débug
        /// </summary>
        /// <param name="sender">La zone</param>
        /// <param name="e">Le clic</param>
        private void GridSelectionZoneScan_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.vueModel.DebutModeAdministration();
        }

        /// <summary>
        /// Clic sur la zone des articles en attente de paiement
        /// </summary>
        /// <param name="sender">La zone</param>
        /// <param name="e">Clic</param>
        private void GridSelectionZoneArticlesAVenir_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.vueArticleSelectionne != null)
            {
                //Création du sprite
                Point positionSouris = Mouse.GetPosition(this.ZoneArticlesAVenirCanvas);
                this.CanvasDrag.Children.Remove(this.vueArticleSelectionne);
                this.ZoneArticlesAVenirCanvas.Children.Add(this.vueArticleSelectionne);
                Canvas.SetLeft(this.vueArticleSelectionne, positionSouris.X - this.vueArticleSelectionne.ActualWidth / 2);
                Canvas.SetTop(this.vueArticleSelectionne, positionSouris.Y - this.vueArticleSelectionne.ActualHeight / 2);
                //On pose l'article sur la zone
                this.vueArticleSelectionne = null;
                //Réactive les vues
                foreach (VueArticle vA in vueArticles) vA.Active();
                this.Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Clic sur la zone des articles enregistres
        /// </summary>
        /// <param name="sender">La zone</param>
        /// <param name="e">Le clic</param>
        private void GridSelectionZoneArticlesEnregistres_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(this.vueArticleSelectionne != null)
            {
                //Création du sprite
                Point positionSouris = Mouse.GetPosition(this.ZoneArticlesEnregistresCanvas);
                this.CanvasDrag.Children.Remove(this.vueArticleSelectionne);
                this.ZoneArticlesEnregistresCanvas.Children.Add(this.vueArticleSelectionne);
                Canvas.SetLeft(this.vueArticleSelectionne, positionSouris.X- this.vueArticleSelectionne.ActualWidth/2);
                Canvas.SetTop(this.vueArticleSelectionne, positionSouris.Y- this.vueArticleSelectionne.ActualHeight / 2);
                //Pose l'article sur la blanace
                this.vueModel.PoseUnArticleSurLaBalance(this.vueArticleSelectionne);
                this.vueArticleSelectionne.EstSurBalance = true;
                //Plus d'article sélectionné
                this.vueArticleSelectionne = null;
                //Réactive les vues
                foreach (VueArticle vA in vueArticles) vA.Active();
            }
        }

        /// <summary>
        /// Clic sur la zone de paiement
        /// </summary>
        /// <param name="sender">La zone</param>
        /// <param name="e">Le clic</param>
        private void GridSelectionZonePaiement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.vueModel.Paye();
        }

        /// <summary>
        /// Ajout d'un objet dans la zone des articles non payés
        /// </summary>
        private void AjouterObjet(TypeArticle type)
        {
            Article article = FabriqueArticle.Creer(type);
            VueArticle vueArticle = new VueArticle(this, article);
            ZoneArticlesAVenirCanvas.Children.Add(vueArticle);
            vueArticle.Loaded += delegate
            {
                Random rand = new Random();
                Canvas.SetLeft(vueArticle, rand.Next((int)(GridSelectionZoneArticlesAVenir.ActualWidth - vueArticle.ActualWidth)));
                Canvas.SetTop(vueArticle, rand.Next((int)(GridSelectionZoneArticlesAVenir.ActualHeight - vueArticle.ActualHeight)));
            };
            this.vueArticles.Add(vueArticle);
        } 

        //Mise en surbrillance d'une zone de sélection
        private void MouseInZoneSelection(Grid zone,bool dropable=false)
        {
            zone.Background = Brushes.LightGray;
            if (this.vueArticleSelectionne != null && dropable) this.Cursor = Cursors.Hand;
        }

        //Fin de la mise en surbrillance d'une zone de sélection
        private void MouseOutZoneSelection(Grid zone, bool dropable = false)
        {
            zone.Background = Brushes.Transparent;
            if (this.vueArticleSelectionne != null && dropable) this.Cursor = Cursors.No;
        }

        /// <summary>
        /// Clic sur un article pour drag and drop
        /// </summary>
        /// <param name="vueArticle">La vueArticle sélectionnée par l'utilisateur</param>
        public void SelectionnerArticle(VueArticle vueArticle)
        {
            if(this.vueArticleSelectionne == null)
            {
                this.vueArticleSelectionne = new VueArticle(vueArticle);
                Point positionSouris = Mouse.GetPosition(this);

                //L'article est enlevé de la balance
                if(vueArticle.EstSurBalance)
                {
                    this.vueModel.EnleveUnArticleDeLaBalance(this.vueArticleSelectionne);
                    this.ZoneArticlesEnregistresCanvas.Children.Remove(vueArticle);
                }
                //L'article vient de la zone des articles à venir
                else
                {
                    this.ZoneArticlesAVenirCanvas.Children.Remove(vueArticle);
                }

                Canvas.SetLeft(vueArticleSelectionne, positionSouris.X-vueArticle.ActualWidth/2);
                Canvas.SetTop(vueArticleSelectionne, positionSouris.Y-vueArticle.ActualHeight/2);
                this.CanvasDrag.Children.Add(vueArticleSelectionne);
                this.Cursor = Cursors.Hand;

                this.vueArticles.Remove(vueArticle);
                this.vueArticles.Add(vueArticleSelectionne);
                foreach(VueArticle vA in vueArticles) vA.Desactive();
            }
        }

        /// <summary>
        /// Drag and drop : suivi de la souris par la vueArticle
        /// </summary>
        /// <param name="e">Mouvement de la souris</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.vueArticleSelectionne != null)
            {
                Point positionSouris = Mouse.GetPosition(this);
                Canvas.SetLeft(vueArticleSelectionne, positionSouris.X - vueArticleSelectionne.ActualWidth / 2);
                Canvas.SetTop(vueArticleSelectionne, positionSouris.Y - vueArticleSelectionne.ActualHeight / 2);
            }
        }

        /// <summary>
        /// Entrée dans la zone de scan
        /// </summary>
        /// <param name="sender">La zone</param>
        /// <param name="e">l'évènement</param>
        private void GridSelectionZoneScan_MouseEnter(object sender, MouseEventArgs e)
        {
            if(this.vueArticleSelectionne != null)
            {
                this.vueModel.PasseUnArticleDevantLeScannair(this.vueArticleSelectionne);
            }
        }

        /// <summary>
        /// Supprimes tous les articles visibles
        /// </summary>
        private void SupprimerArticles()
        {
            this.ZoneArticlesAVenirCanvas.Children.Clear();
            this.ZoneArticlesEnregistresCanvas.Children.Clear();
            this.CanvasDrag.Children.Clear();
            this.vueArticleSelectionne = null;
            this.vueArticles.Clear();
        }

        /// <summary>
        /// Clic sur le bouton : Etudiant
        /// </summary>
        /// <param name="sender">Le bouton</param>
        /// <param name="e">Le clic</param>
        private void ClientEtudiant_Click(object sender, RoutedEventArgs e)
        {
            this.SupprimerArticles();
            this.PanelClient.IsEnabled = false;
            this.AjouterObjet(TypeArticle.PATES);
        }

        /// <summary>
        /// Clic sur le bouton : Groupe
        /// </summary>
        /// <param name="sender">Le bouton</param>
        /// <param name="e">Le clic</param>
        private void ClientGroupe_Click(object sender, RoutedEventArgs e)
        {
            this.SupprimerArticles();
            this.PanelClient.IsEnabled = false;
            this.AjouterObjet(TypeArticle.CHIPS);
            this.AjouterObjet(TypeArticle.CHIPS);
            this.AjouterObjet(TypeArticle.COCA);
            this.AjouterObjet(TypeArticle.COCA);
            this.AjouterObjet(TypeArticle.COCA);
        }

        /// <summary>
        /// Clic sur le bouton : Aléatoire
        /// </summary>
        /// <param name="sender">Le bouton</param>
        /// <param name="e">Le clic</param>
        private void ClientAleatoire_Click(object sender, RoutedEventArgs e)
        {
            this.SupprimerArticles();
            this.PanelClient.IsEnabled = false;
            Array values = Enum.GetValues(typeof(TypeArticle));
            Random random = new Random();
            for (int i=0;i < 10;i ++)
            {
                this.AjouterObjet((TypeArticle)values.GetValue(random.Next(values.Length)));
            }
        }
    }
}

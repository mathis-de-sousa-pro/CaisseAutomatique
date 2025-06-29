﻿using CaisseAutomatique.Model.Articles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model 
{
    /// <summary>
    /// Caisse automatique (couche métier)
    /// </summary>
    public class Caisse : INotifyPropertyChanged
    {
        /// <summary>
        /// Liste des articles enregistrés
        /// </summary>
        private List<Article> articles;
        public List<Article> Articles => articles;

        /// <summary>
        /// Dernier article scanné (dans un vrai modèle, seule la référence est connue et on utiliserait une Bdd pour retrouver l'article)
        /// </summary>
        public Article DernierArticleScanne => dernierArticleScanne;
        private Article dernierArticleScanne;

        /// <summary>
        /// Quantité saisie pour les articles dénombrables
        /// </summary>
        public int QuantiteSaise => quantiteSaisie;
        private int quantiteSaisie;
        

        /// <summary>
        /// Poids des objets sur la balance
        /// </summary>
        public double PoidsBalance => poidsBalance;
        private double poidsBalance;

        /// <summary>
        /// Somme déjà payée par l'utilisateur
        /// </summary>
        public double SommePayee => sommePayee;
        private double sommePayee;

        /// <summary>
        /// Poids attendu dans la balance
        /// </summary>
        public double PoidsAttendu
        {
            get
            {
                double res = 0;
                foreach (Article article in articles) res += article.Poids;
                return res;
            }
        }

        /// <summary>
        /// Prix total
        /// </summary>
        public double PrixTotal
        {
            get
            {
                double res = 0;
                foreach (Article article in articles) res += article.Prix;
                return res;
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Caisse()
        {
            this.articles = new List<Article>();
            this.dernierArticleScanne = null;
            this.poidsBalance = 0;
            this.sommePayee = 0;
        }

        /// <summary>
        /// Ajoute le poids d'un article sur la balance
        /// </summary>
        /// <param name="article">Article posé</param>
        public void PoserArticleSurBalance(Article article)
        {
            this.poidsBalance += article.Poids;
            this.NotifyPropertyChanged(nameof(PoidsBalance));
        }

        /// <summary>
        /// Enlève le poids d'un article de la balance
        /// </summary>
        /// <param name="article">Article retiré</param>
        public void EnleverArticleDeLaBalance(Article article)
        {
            this.poidsBalance -= article.Poids;
            if (this.poidsBalance < 0) this.poidsBalance = 0;
            this.NotifyPropertyChanged(nameof(PoidsBalance));
        }

        /// <summary>
        /// Saisie d'une quantité pour un article dénombrable
        /// </summary>
        /// <param name="valeur">Valeur de la quantité</param>

        public void SaisieQuantite(int valeur)
        {
            this.quantiteSaisie = valeur;
        }

        /// <summary>
        /// Enregistre un article scanné par le client
        /// </summary>
        /// <param name="article">Article scanné</param>
        /// <param name="quantite">Quantité d'articles à enregistrer</param>
        public void EnregistrerArticle(Article article, int quantite = 1)
        {
            for (int i = 0; i < quantite; i++)
            {
                this.articles.Add(article);
            }
            this.dernierArticleScanne = article;
            this.NotifyPropertyChanged(nameof(Articles));
        }

        /// <summary>
        /// Encaisse le paiement complet du client
        /// </summary>
        public void Payer()
        {
            this.sommePayee = this.PrixTotal;
            this.NotifyPropertyChanged(nameof(SommePayee));
        }

        /// <summary>
        /// Annule le dernier article enregistré
        /// </summary>
        public void AnnulerDernierArticle()
        {
            if (this.articles.Count > 0)
            {
                this.articles.RemoveAt(this.articles.Count - 1);
                this.dernierArticleScanne = this.articles.Count > 0 ? this.articles[^1] : null;
                this.NotifyPropertyChanged(nameof(Articles));
            }
        }

        /// <summary>
        /// Annule tous les articles de la commande
        /// </summary>
        public void AnnulerTousLesArticles()
        {
            if (this.articles.Count > 0)
            {
                this.articles.Clear();
                this.dernierArticleScanne = null;
                this.NotifyPropertyChanged(nameof(Articles));
            }
        }

        /// <summary>
        /// Remise à zéro de la caisse pour un nouveau client
        /// </summary>
        public void Reset()
        {
            this.articles.Clear();
            this.dernierArticleScanne = null;
            this.quantiteSaisie = 0;
            this.poidsBalance = 0;
            this.sommePayee = 0;
            this.NotifyPropertyChanged(nameof(Articles));
            this.NotifyPropertyChanged(nameof(SommePayee));
            this.NotifyPropertyChanged("Reset");
        }

        /// <summary>
        /// Pattern d'observable
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

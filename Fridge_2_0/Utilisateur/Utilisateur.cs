using Fridge_2_0.Facture;
using System;
using System.Collections.Generic;

namespace Utilisateur
{
    public class Utilisateur
    {
        
        public Utilisateur(int id, string nom, string surnom, string email, string motDePasse,double dette, List<Facture> historique)
        {
            this.id_ = id;
            this.nom_ = nom;
            this.surnom_ = surnom;
            this.email_ = email;
            this.motDePasse_ = motDePasse;
            this.dette_ = dette;
            historique_ = historique;
        }

        public Utilisateur(string nom, string surnom, string email, string motDePasse)
        {
            this.id_ = 0;
            this.nom_ = nom;
            this.surnom_ = surnom;
            this.email_ = email;
            this.motDePasse_ = motDePasse;
            this.dette_ = 0.00;
            historique_ = new List<Facture>();
        }


        private int id_;
        private string nom_;
        private string surnom_;
        private string email_;
        private string motDePasse_;
        private double dette_;
        private List<Facture> historique_;

        public int getId() { return id_; }
        public string getNom() { return nom_; }
        public string getSurnom() { return surnom_; }
        public string getEmail() { return email_; }
        public string getMotDePasse() { return motDePasse_; }
        public double getDette() { return dette_; }
        public List<Facture> getHistorique() { return historique_; }

        //No setId() function, as they can never be changed under any circumstances
        public void setNom(string value) { nom_ = value; }
        public void setSurnom(string value) { surnom_ = value; }
        public void setEmail(string value) { email_ = value; }
        public void setMotDePasse(string value) { motDePasse_ = value; }
        public void setDette(double value) { dette_ = value; }

        
        /*
        public void afficher()
        {
            Console.Write(getId() +":");
            Console.Write(getSurnom() + " (" + getNom() + ").");
            Console.Write("Email: "+ getEmail() + ".");
            Console.WriteLine("Dette: " + String.Format("{0:0.00}", getDette()) + "$");

        }
        */
    };
}

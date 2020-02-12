using System;

namespace Utilisateur
{
    public class Utilisateur
    {
        public Utilisateur(string nom, string surnom, string email, string motDePasse)
        {
            this.nom_ = nom;
            this.surnom_ = surnom;
            this.email_ = email;
            this.motDePasse_ = motDePasse;
            this.dette_ = 0.00;
            //this.historique_ = Historique();
        }



        //GETTERS
        public string getNom()
        {
            return nom_;
        }
        public string getSurnom() 
        {
            return surnom_;
        }
        public string getEmail()
        {
            return email_;
        }
        public string getMotDePasse()
        {
            return motDePasse_;
        }
        public double getDette()
        {
            return dette_;
        }



        //SETTERS
        public void setDette(double dette)
        {
            dette_ = dette;
        }
        public void setNom(string nom)
        {
            nom_ = nom;
        }
        public void setSurnom(string surnom)
        {
            surnom_ = surnom;
        }
        public void setEmail(string email)
        {
            email_ = email;
        }
        public void setMotDePasse(string motDePasse)
        {
            motDePasse_ = motDePasse;
        }




        /*
        Historique getHistorique()
        */

        private string nom_;
        private string surnom_;
        private string email_;
        private string motDePasse_;
        private double dette_;
        /*
        Historique historique_;
        */
    };
}

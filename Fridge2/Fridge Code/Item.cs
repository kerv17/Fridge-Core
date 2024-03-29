using System;
using System.Collections.Generic;

namespace Fridge_2_0
{
    public class Item
    {
        int id_;
        private string nom_;
        private double prix_;
        int stock_;
        List<Facture> historique_;

        double TempsHorsStockH = 0;
        DateTime DernierFinDeStock = DateTime.Now;

        public Item(int id, string nom, double prix, int stock, List<Facture> factures)
        {
            id_ = id;
            nom_ = nom;
            prix_ = prix;
            stock_ = stock;
            historique_ = factures;
        }

        public Item(int id, string nom, double prix, int stock, DateTime date, double temps)
        {
            id_ = id;
            nom_ = nom;
            prix_ = prix;
            stock_ = stock;
            historique_ = new List<Facture>();
            DernierFinDeStock = date;
            TempsHorsStockH = temps;
        }

        public Item(string nom, double prix, int stock = 0 )
        {
            nom_ = nom;
            prix_ = prix;
            stock_ = stock;
            historique_ = new List<Facture>();
        }

        public void setStock(int quantite)
        {
            stock_ = quantite;
        }


        public void addStock(int quantite)
        {
            stock_ += quantite;
        }

        public void removeStock(int quantite)
        {
            addStock(-quantite);
            if (stock_ < 0)
                stock_ = 0;
        }

        

        public int getId() { return id_; }
        public string getNom() { return nom_; }
        public double getPrix() { return prix_; }
        public int getStock() { return stock_; }
        public double getTempsHorsStock(){ return TempsHorsStockH;}
        public DateTime getDernierHorsStock(){return DernierFinDeStock;}

        public void ajouterFacture(Facture f)
        {
            historique_.Add(f);
        }

        public void setNom(string nom)
        {
            nom_ = nom;
        }
        public void setPrix(double prix)
        {
            prix_ = prix;
        }

        public void mettreAJourTempsHorsStock(){
        
            TimeSpan difference = DateTime.Now-DernierFinDeStock;
            TempsHorsStockH += difference.TotalHours;
            

        }

        public void mettreAJourDernierMomentHorsStock(){
            DernierFinDeStock = DateTime.Now;
        }

        public List<Facture> getHistorique(){
            return historique_;
        }
    }

}

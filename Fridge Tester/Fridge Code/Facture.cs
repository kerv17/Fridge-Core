using System;
using System.Collections.Generic;
using System.Text;

namespace Fridge_2_0
{
    public class Facture
    {
        private int id_;
        private Utilisateur utilisateur_;
        private Item item_;
        private DateTime horodate_;
        private double prix_;

        public int getId() { return id_; }
        public Utilisateur getUtilisateur() { return utilisateur_; }
        public Item getItem() { return item_; }
        public DateTime getHorodate() { return horodate_; }
        public double getPrix() { return prix_; }

        public Facture(int id, Utilisateur utilisateur, Item item, double prix, DateTime horodate)
        {
            id_ = id;
            utilisateur_ = utilisateur;
            item_ = item;
            prix_ = prix;
            horodate_ = horodate;
        }

        public Facture(Utilisateur utilisateur, Item item)
        {
            id_ = 0;
            utilisateur_ = utilisateur;
            item_ = item;
            prix_ = item_.getPrix();
            horodate_ = DateTime.Now;
        }
    }
}


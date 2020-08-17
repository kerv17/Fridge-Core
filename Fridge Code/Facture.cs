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

        public int getId() { return id_; }
        public Utilisateur getUtilisateur() { return utilisateur_; }
        public Item getItem() { return item_; }
        public DateTime getHorodate() { return horodate_; }

        public Facture(int id, Utilisateur utilisateur, Item item, DateTime horodate)
        {
            id_ = id;
            utilisateur_ = utilisateur;
            item_ = item;
            horodate_ = horodate;
        }

        public Facture(Utilisateur utilisateur, Item item)
        {
            id_ = 0;
            utilisateur_ = utilisateur;
            item_ = item;
            horodate_ = DateTime.Now;
        }
    }
}


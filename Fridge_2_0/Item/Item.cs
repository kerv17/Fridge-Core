using System;

namespace Fridge_2_0.Item
{
    public class Item
    {
        String nom_;
        Double prix_;
        uint quantite_;


        public Item(String nom = "Undefined", Double prix = 0, uint quantite = 0)
        {
            nom_ = nom;
            prix_ = prix;
            quantite_ = quantite;
        }

        public void setQuantity(uint quantite)
        {
            quantite_ = quantite;
        }


        public void addQuantity(int quantite)
        {
            quantite += quantite;
        }

        public void removeQuantity(int quantite)
        {
            addQuantity(-quantite);
        }

    }

}

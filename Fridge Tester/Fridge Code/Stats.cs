using Fridge_2_0;

namespace Fridge_2_0
{
    class Stats{
        private BD database;


        public Stats(BD db){
            database = db;
        }



        public Utilisateur utilisateurLePlusEndette(){
            List<Utilisateur> utilisateurs = database.GetUtilisateurs();
            Utilisateur endette = new Utilisateur("","","","");
            foreach (Utilisateur utilisateur in utilisateur)
            {
                if utilisateur.getDette() > endette.getDette()
                    endette=utilisateur;
            }
        }

        public Utilisateur utilisateurLePlusAcheteur(){
            List<Utilisateur> utilisateurs = database.GetUtilisateurs();
            Utilisateur acheteur = new Utilisateur("","","","");
            foreach (Utilisateur utilisateur in utilisateur)
            {
                if utilisateur.getHistorique().Length > acheteur.getHistorique().Length
                    acheteur=utilisateur;
            }
        }

        public Item itemLePlusAchete(){
            List<Item> items = database.GetItems();
            Item vendeur = new Item("",0);
        
            foreach (Item i in items)
            {
                if i.getHistorique().Length > vendeur.getHistorique().Length
                    vendeur=i;
            }
        }


        public Item itemLeMoinsSouventEnStock(){
            List<Item> items = database.GetItems();
            Item vendeur = new Item("",0);
        
            foreach (Item i in items)
            {
                if i.getTempsHorsStock() > vendeur.getTempsHorsStock()
                    vendeur=i;
            }
        }
    }
}
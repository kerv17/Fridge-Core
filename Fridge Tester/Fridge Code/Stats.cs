using Fridge_2_0;
using System.Collections.Generic;
using System;

namespace Fridge_2_0
{
    public class Stats{
        private BD database;


        public Stats(BD db){
            database = db;
        }



        public Utilisateur utilisateurLePlusEndette(){
            List<Utilisateur> utilisateurs = database.listeU;
            Utilisateur endette = new Utilisateur("","","","");
            foreach (Utilisateur utilisateur in utilisateurs)
            {
                if (utilisateur != null){
                    if (utilisateur.getDette() > endette.getDette() && utilisateur != null)
                        endette=utilisateur;
                }
            }
            return endette;
        }

        public Utilisateur utilisateurLePlusAcheteur(){
            List<Utilisateur> utilisateurs = database.listeU;
            Utilisateur acheteur = new Utilisateur("","","","");
            foreach (Utilisateur utilisateur in utilisateurs)
            {
                if (utilisateur != null){
                    if (utilisateur.getHistorique().Count > acheteur.getHistorique().Count)
                        acheteur=utilisateur;
                }
            }
            return acheteur;
        }

        public Item itemLePlusAchete(){
            List<Item> items = database.listeI;
            Item vendeur = new Item("",0);
        
            foreach (Item i in items)
            {
                if (i != null){
                    if (i.getHistorique().Count > vendeur.getHistorique().Count)
                      vendeur=i;
                }
            }
            return vendeur;
        }


        public Item itemLeMoinsSouventEnStock(){
            List<Item> items = database.listeI;
            Item vendeur = new Item("",0);
        
            foreach (Item i in items)
            {
                if (i != null){
                    if (i.getTempsHorsStock() > vendeur.getTempsHorsStock())
                        vendeur=i;
                }
            }
            return vendeur;
        }

        public double detteTotale(){
            List<Utilisateur> utilisateurs = database.listeU;
            double somme = 0;
            foreach (Utilisateur u in utilisateurs){
                if (u!=null){
                somme += u.getDette();
                }
            }
            return somme;
        }

        public List<string> listeDeStats(){
            database.refresh();
            List<string> stats = new List<string>();
            Item i;
            Utilisateur u;
            //Item Le Moins Souvent En Stock
            i = itemLeMoinsSouventEnStock();
            stats.Add(
                String.Format(
                    "Item le plus souvent en rupture de stock: {0,10}  avec {1,0}",
                    i.getNom(),hoursToDHM(i.getTempsHorsStock())
                )
            );

            //Item Le Plus Acheté
            i = itemLePlusAchete();
            stats.Add(
                String.Format(
                    "Item le plus souvent acheté: {0,10} avec {1,0} achats",
                    i.getNom(),i.getHistorique().Count
                )
            );

            //Dette totale
            stats.Add(
                String.Format(
                    "Dette totale: {0,5}$", 
                    detteTotale()
                )
            );


            //Membre le plus endetté
            u = utilisateurLePlusEndette();
            stats.Add(
                String.Format(
                    "Utilisateur le plus endetté: {0,20} avec {1,0}$",
                    u.getSurnom(),u.getDette()
                )
            );

            //utilisateur avec le plus d'achats
            stats.Add(
                String.Format(
                    "Utilisateur avec le plus d'achats: {0,20} avec {1,3} achats",
                    u.getSurnom(),u.getHistorique().Count
                )
            );


            return stats;

        }

        public string printStats(){
            string listeComplete = "";
            foreach(string s in listeDeStats()){
                listeComplete += s + "\n\n";
            }

            return listeComplete;
        }

        public string hoursToDHM(double hours){
            string s;
            int day = (int)hours/24;
            int hour = (int)hours%24;
            int min = (int)(hours*60)%60;
            s=String.Format("{0,3}j {1,2}h {2,2}m", day, hour,min);
            return s;
        }
    }
}
/*
 * Modele.cs
 * 
 * @date 12 Fév 2020
 * 
 * Fonction de démarrage de l'application
 * 
 */

using Fridge_2_0;
using System.Collections.Generic;
using System;
class Fridge
{
    static int Main()
    {
        //Vue vue = new Vue();
        //vue.afficher();
        BD database = new BD();
        database.GetUtilisateurs();
        List<Utilisateur.Utilisateur> liste = database.GetUtilisateurs();
        Console.WriteLine("Utilisateurs:");
        for (int i = 0; i< liste.Count; i++)
        {
            liste[i].afficher();
        }
        
       return 0;
    }
}

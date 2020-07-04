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
        Vue vue = new Vue();
        BD database = new BD();
        vue.afficher();
        
        
        
       return 0;
    }
}

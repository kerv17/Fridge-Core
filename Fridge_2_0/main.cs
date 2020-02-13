/*
 * Modele.cs
 * 
 * @date 12 Fév 2020
 * 
 * Fonction de démarrage de l'application
 * 
 */

using Fridge_2_0;

class Fridge
{
    static int Main()
    {
        Vue vue = new Vue();
        vue.afficher();

        return 0;
    }
}

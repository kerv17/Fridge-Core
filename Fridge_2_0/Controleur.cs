/*
 * Controleur.cs
 * 
 * @date 12 Fév 2020
 * 
 * Composante "Contrôleur" du modèle MVC.
 * Implémente les callbacks des entrées du GUI.
 * Reflète les changements au modèle dans les sorties du GUI.
 * 
 */

using Terminal.Gui;

namespace Fridge_2_0
{
    public class Controleur
    {
        public Controleur()
        {
        }

        public static Dialog AfficherBienvenue()
        {
            var alerteTest = new Dialog("Bienvenue sur le Fridge!", 40, 10, new Button("OK", true) { Clicked = () => {; } });
            return alerteTest;
        }
    }
}

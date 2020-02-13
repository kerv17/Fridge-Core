/*
 * Vue.cs
 * 
 * @date 12 Fév 2020
 * 
 * Composante "Vue" du modèle MVC. Crée et affiche les éléments visuels
 * de l'IU. Les callbacks du GUI sont implémentées sous Controleur.cs
 * 
 */

using Terminal.Gui;
using Fridge_2_0;

namespace Fridge_2_0
{
    class Vue
    {
        protected Controleur controleur;

        public bool afficher()
        {
            Application.Init();
            var top = Application.Top;
            controleur = new Controleur();

            // Creates the top-level window to show
            var win = new Window("MyApp")
            {
                X = 0,
                Y = 0,
                // By using Dim.Fill(), it will automatically resize without manual intervention
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            top.Add(win);

            // Creates a menubar, the item "New" has a help menu.
            var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
                new MenuItem ("_Quit", "", () => { top.Running = false; })
            }),
        });

            var boutonLabel = new Button(40, 10, "Afficher le message de bienvenue", true)
            {
                Clicked = (() => { Application.Run(Controleur.AfficherBienvenue()); })
            };

            top.Add(menu);
            top.Add(boutonLabel);
            Application.Run();

            return true;
        }
    }
}

using Terminal.Gui;

class Demo
{

    static Dialog AfficherBienvenue()
    {
        var alerteTest = new Dialog("Bienvenue sur le Fridge!", 40, 10, new Button("OK", true) { Clicked = () => {; } } );
        return alerteTest;
    }

    static void Main()
    {
        Application.Init();
        var top = Application.Top;

        // Creates the top-level window to show
        var win = new Window("MyApp")
        {
            X = 0,
            Y = 1, // Leave one row for the toplevel menu

            // By using Dim.Fill(), it will automatically resize without manual intervention
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        top.Add(win);
        //top.ColorScheme = new ColorScheme();

        // Creates a menubar, the item "New" has a help menu.
        var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
                //                new MenuItem ("_New", "Creates new file", NewFile),
                //new MenuItem ("_Close", "", () => Close ()),
                //new MenuItem ("_Quit", "", () => { if (Quit ()) top.Running = false; })
                new MenuItem ("_New", "Creates new file", null),
                new MenuItem ("_Close", "", null),
                new MenuItem ("_Quit", "", () => { top.Running = false; })
            }),
            new MenuBarItem ("_Edit", new MenuItem [] {
                new MenuItem ("_Copy", "", null),
                new MenuItem ("C_ut", "", null),
                new MenuItem ("_Paste", "", null)
            }),
        });
        var boutonLabel = new Button(40,10,"Afficher le message de bienvenue", false)
        {
            Clicked = ( () => { Application.Run(AfficherBienvenue()); } )
        };

        top.Add(menu);

        var login = new Label("Login: ") { X = 3, Y = 2 };
        var password = new Label("Password: ")
        {
            X = Pos.Left(login),
            Y = Pos.Top(login) + 1
        };
        var loginText = new TextField("")
        {
            X = Pos.Right(password),
            Y = Pos.Top(login),
            Width = 40
        };
        var passText = new TextField("")
        {
            Secret = true,
            X = Pos.Left(loginText),
            Y = Pos.Top(password),
            Width = Dim.Width(loginText)
        };

        // Add some controls, 
        win.Add(
            // The ones with my favorite layout system
            login, password, loginText, passText, boutonLabel,

                // The ones laid out like an australopithecus, with absolute positions:
                new CheckBox(3, 6, "Remember me"),
                new RadioGroup(3, 8, new[] { "_Personal", "_Company" }),
                new Button(3, 14, "Ok"),
                new Button(10, 14, "Cancel"),
                new Label(3, 18, "Press F9 or ESC plus 9 to activate the menubar"));

        Application.Run();
    }
}
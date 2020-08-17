using Godot;
using System;
using Fridge_2_0;
public class Connections : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("Hello");
		BD database = new BD();
        Helper.dropTableaux(database.GetConnection());
        Helper.creerTableaux(database.GetConnection());
        Utilisateur newUser = new Utilisateur(
			"Kervin", "Unity", "kerv1234@live.fr", "kervin17"
			);
        database.creerUtilisateur(newUser);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	
	static public bool verifierCompte(string username, string password)
	{
		BD database = new BD();
        Utilisateur compte = database.GetUtilisateur(username);
        return (compte.getMotDePasse() == password);
	}

}

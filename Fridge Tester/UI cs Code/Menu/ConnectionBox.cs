using Godot;
using System;
using Fridge_2_0;
using System.Data.SqlClient;

public class ConnectionBox : WindowDialog
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	// Called when the node enters the scene tree for the first time.
	CurrentUser currentuser;
	public override void _Ready()
	{
		currentuser = (CurrentUser)GetNode("/root/CurrentUser");
	}
  
	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }



	private void _on_Button_pressed()
	{
		var user = (LineEdit)GetNode("LineEdit");
		var pass = (LineEdit)GetNode("LineEdit2");
		string username = user.Text;
		string password = pass.Text;
		
		Utilisateur attempt = currentuser.database.GetUtilisateur(username);
		try {
			if (attempt.getMotDePasse() == password)
			{

				currentuser.user = currentuser.database.listeU[attempt.getId()];
				GetTree().ChangeScene("res://Scenes/Utilisateur.tscn");
			}
			else{
				Exception e = new Exception();
				 throw(e);
				}
			}
		catch(Exception)
		{
			var a = (RichTextLabel)GetNode("RichTextLabel");
			a.Show();
		}
		//bool answer = Connections.verifierCompte(user.Text, pass.Text);
		//GD.Print(answer);
	}
	
	
	
	
}


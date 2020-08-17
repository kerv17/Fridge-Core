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
    public override void _Ready()
    {
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
		
		BD db = new BD();

		//bool answer = Connections.verifierCompte(user.Text, pass.Text);
        //GD.Print(answer);
    }
	
	
	
	
}

using Fridge_2_0;
using Godot;
using System;
using System.Collections.Generic;

public class ModifierUtilisateur : WindowDialog
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    CurrentUser current;
    public Utilisateur user;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        current = (CurrentUser)GetNode("/root/CurrentUser");
    }
	
	private void onPopup()
	{
	    var textBox = (RichTextLabel)GetNode("ColorRect/RichTextLabel");
        textBox.Clear();
        string[] str = new string[5];
        str[0] = "Nom: "+user.getNom();
        str[1] = "Surnom: " + user.getSurnom();
        str[2] = "Email: " + user.getEmail();
        str[3] = "Dette: " + user.getDette();
        str[4] = "Mot de passe: " + "********";
        foreach(string s in str)
        {
            textBox.AddText(s);
            textBox.Newline();
        }
	}
    private void _on_Button_pressed()
    {
        var nom = (LineEdit)GetNode("Control/nom");
        var surnom = (LineEdit)GetNode("Control/surnom");
        var dette = (LineEdit)GetNode("Control/dette");
        var mdp = (LineEdit)GetNode("Control/mdp");
        var email = (LineEdit)GetNode("Control/email");

        user.setDette(Convert.ToDouble(dette.Text));
        user.setNom(nom.Text);
        user.setSurnom(surnom.Text);
        user.setMotDePasse(mdp.Text);
        user.setEmail(email.Text);
        current.database.OverrideUtilisateur(user);
        onPopup();
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}







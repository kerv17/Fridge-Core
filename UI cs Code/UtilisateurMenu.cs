using Godot;
using System;
using Fridge_2_0;

public class UtilisateurMenu : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
	public Utilisateur utilisateur;
	public Item item;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    public Utilisateur GetUtilisateur()
    {
        return utilisateur;
    }

    public Item GetItem()
    {
        return item;
    }

    public void setItem(Item i)
    {
        item = i;
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

using Godot;
using System;
using Fridge_2_0;
using System.Collections.Generic;

public class UtilisateurMenu : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	CurrentUser current;
	public Utilisateur utilisateur;
	public Item item;
	public List<Facture> factures;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		current = (CurrentUser)GetNode("/root/CurrentUser");
		utilisateur = current.user;
		var title = (RichTextLabel)GetNode("Panel/RichTextLabel");
		title.Text =  utilisateur.getSurnom() + " ("+ utilisateur.getDette().ToString("F") +"$)";
	}

	public void refreshUser()
	{
		int id = current.user.getId();
		current.database.refresh();
		current.user = current.database.listeU[id];
		var title = (RichTextLabel)GetNode("Panel/RichTextLabel");
		title.Text =  current.user.getSurnom() + " ("+ current.user.getDette().ToString("F") +"$)";
	}
	public void refreshHistorique()
	{
		factures = current.user.getHistorique();
	}
	
	public void refreshItems()
	{
		ItemList item = (ItemList)GetNode("Panel/TabContainer/Items");
		item.refreshItems();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}

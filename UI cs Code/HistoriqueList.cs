using Godot;
using System;
using Fridge_2_0;
using System.Collections.Generic;

public class HistoriqueList : ItemList
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        List<Facture> liste = genererListe(50);
        
        foreach (Facture item in liste) {
            AddItem(item.getItem().getNom() + "   " + item.getHorodate());
        }
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	private List<Facture> genererListe(int a)
    {
        var currentUser = (CurrentUser)GetNode("/root/CurrentUser");
		Utilisateur utilisateur = currentUser.user;






        List<Facture> liste = new List<Facture>();
        var rng = new RandomNumberGenerator();
        for (int i =0; i<a; i++)
        {
            rng.Randomize();
            liste.Add(new Facture(utilisateur, null));
        }
        return liste;
    }
}

using Godot;
using System;
using Fridge_2_0;
using System.Collections.Generic;

public class ItemList : Godot.ItemList
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        List<Item> liste = genererListe(50);
        
        foreach (Item item in liste) {
            AddItem(item.getNom() + "   " + item.getPrix() + "$  (" + item.getStock() + " en stock)");
        }
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    
    private List<Item> genererListe(int a)
    {
        List<Item> liste = new List<Item>();
        var rng = new RandomNumberGenerator();
        for (int i =0; i<a; i++)
        {
            rng.Randomize();
            liste.Add(new Item("Item " + i,Math.Round(rng.RandfRange(0,5),2)));
        }
        return liste;
    }
}

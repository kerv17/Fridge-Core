using Godot;
using System;
using Fridge_2_0;
using System.Collections.Generic;

public class HistoriqueList : ItemList
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    CurrentUser current;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        current = (CurrentUser)GetNode("/root/CurrentUser");
        refreshHistorique();
    }

    public void refreshHistorique()
    {
        var a = (CurrentUser)GetNode("/root/CurrentUser");
        a.database.refresh();
        Clear();
        List<Facture> factures = a.database.listeF;
        GD.Print(factures.Count);
        foreach (Facture f in factures)
        {
            if (f != null) {
                string str = "";
                str = String.Format("{0,3}: {1,22} : {2, -15} ({3,5}$)", f.getId(), f.getHorodate(), f.getItem().getNom(), f.getItem().getPrix().ToString("F"));
                AddItem(str);
            }
            

        }
    }

}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }


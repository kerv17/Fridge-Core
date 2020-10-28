using Godot;
using System;
using Fridge_2_0;
using System.Collections.Generic;

public class FactureListAdmin : ItemList
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    CurrentUser current;
    List<Facture> factures;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        current = (CurrentUser)GetNode("/root/CurrentUser");
        refreshFactures();
    }


    public void refreshFactures(){
        factures = current.database.listeF;
        Clear();
        foreach (Facture f in factures)
        {
            if (f != null)
            {
                string str = "";
                str = String.Format("{0,3}: {1,22} : {4,-15} achete {2, -15} ({3,5}$)", f.getId(), f.getHorodate(), f.getItem().getNom(), f.getItem().getPrix().ToString("F"), f.getUtilisateur().getSurnom());
                AddItem(str);
            }
        }
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

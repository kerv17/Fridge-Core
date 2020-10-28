using Godot;
using System;
using Fridge_2_0;
using System.Collections.Generic;

public class ItemListAdmin : Godot.ItemList
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	List<Item> items;
	CurrentUser current;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		current = (CurrentUser)GetNode("/root/CurrentUser");
		refreshItems();

	}

	private void _on_Buy_pressed()
	{
		var parent = (TabContainer)GetParent();
		if (IsAnythingSelected() && parent.CurrentTab == 0)
		{
			var confirm = (Confirmbuy)Owner.GetNode("Confirmbuy");
			var a = (UtilisateurMenu)Owner;
			var i = GetSelectedItems()[0];
			a.item = items[i + 1];
			confirm.PopupCentered();
		}
	}



	public void refreshItems()
	{
		var a = (CurrentUser)GetNode("/root/CurrentUser");
		a.database.refresh();
		items = a.database.listeI;
		Clear();
		GD.Print(items.Count);
		foreach (Item item in items)
		{
			if (item != null)
			{
				string str = String.Format("{0,3}: {1,-20} {2, 10}$ - {3,5} en stock", item.getId(), item.getNom(), item.getPrix().ToString("F"), item.getStock());
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

using Fridge_2_0;
using Godot;
using System;

public class ModifierItem : WindowDialog
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    CurrentUser current;
    public Item item;
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
        str[0] = "Nom: " + item.getNom();
        str[1] = "Prix: " + item.getPrix();
        str[2] = "Email: " + item.getStock();
        foreach (string s in str)
        {
            textBox.AddText(s);
            textBox.Newline();
        }
    }

    private void onResetStock()
    {
        item.setStock(0);
        onPopup();
    }

    private void onConfirm()
    {
        var nom = (LineEdit)GetNode("Control/nom");
        var cout = (LineEdit)GetNode("Control/prix");
        var stock = (LineEdit)GetNode("Control/stock");

        item.setNom(nom.Text);
        item.setPrix(Convert.ToDouble(cout.Text));
        item.addStock(Convert.ToInt32(stock.Text));
        current.database.OverrideItem(item);
        onPopup();
    }


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}







using Godot;
using System;
using Fridge_2_0;
public class StatsPanel : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Stats stats;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
       var current = (CurrentUser)GetNode("/root/CurrentUser");
       stats = new Stats(current.database);
       var textbox = (RichTextLabel)GetNode("Panel/Textbox");
       textbox.Text = stats.printStats();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

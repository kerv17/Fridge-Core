using Godot;
using System;
using Fridge_2_0;
public class Confirmbuy : ConfirmationDialog
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    CurrentUser currentuser;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentuser = (CurrentUser)GetNode("/root/CurrentUser");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }



    private void _on_Confirm_buy_confirmed()
    {
        Utilisateur utilisateur = currentuser.user;
        var control = (UtilisateurMenu)this.Owner;
	    Facture facture = new Facture(utilisateur,control.item);
        currentuser.database.creerFacture(facture);
        control.item.removeStock(1);
        if(control.item.getStock() <= 0){
            control.item.setStock(0);
            control.item.mettreAJourDernierMomentHorsStock();
        }
        utilisateur.setDette(utilisateur.getDette()+control.item.getPrix());
        currentuser.database.OverrideItem(control.item);
        currentuser.database.OverrideUtilisateur(utilisateur);
        control.refreshHistorique();
        control.refreshUser();

        var item = (ItemList)control.GetNode("Panel/TabContainer/Items");
        item.refreshItems();
        var factures = (HistoriqueList)control.GetNode("Panel/TabContainer/Historique");
        factures.refreshHistorique();
        // Replace with function body.
    }
}

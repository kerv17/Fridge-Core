using Godot;
using System;
using Fridge_2_0;
using System.Collections.Generic;

public class AdminPanel : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    enum Mode {
		Utilisateur = 1,
		Item = 0,
		Facture = 2
	}
	Mode mode = Mode.Utilisateur;
    CurrentUser current;
	List<Utilisateur> utilisateurs;
	List<Item> items;
	List<Facture> factures;
    Godot.ItemList itemList;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        current = (CurrentUser)GetNode("/root/CurrentUser");
        itemList = (Godot.ItemList)GetNode("Panel/TabContainer/Item");
        onUtilisateurs();
    }

    public void reloadLists()
    {
        current.database.refresh();
        utilisateurs = current.database.listeU;
        items = current.database.listeI;
        factures = current.database.listeF;
    }

    private void onUtilisateurs()
    {
        mode = Mode.Utilisateur;
        reloadLists();
        var utilisateurList = (Godot.ItemList)GetNode("Panel/TabContainer/Utilisateurs");
        utilisateurList.Clear();
        foreach(Utilisateur u in utilisateurs)
        {
            if (u!=null){
                string str = "";
                str = String.Format("{0,3}: {1,-15} {2,-30} - {3,12}",u.getId(),u.getSurnom(),"("+u.getNom()+")",u.getDette().ToString("C"));

                utilisateurList.AddItem(str);
            }
        }
        var control = (Control)GetNode("Panel/Control");
        control.Show();
    }
	private void onItems()
    {
        mode = Mode.Item;
        reloadLists();
        itemList.Clear();
        foreach (Item item in items)
        {
            string str = "";
            str = String.Format("{0,3}: {1,-20} {2, 10}$ - {3,5} en stock", item.getId(), item.getNom(), item.getPrix(), item.getStock());
            itemList.AddItem(str);
        }
        var control = (Control)GetNode("Panel/Control");
        control.Show();
    }

    private void onFactures()
    {
        GD.Print("Factures");
        mode = Mode.Facture;
        var control = (Control)GetNode("Panel/Control");
        control.Hide();
        itemList.Clear();
        reloadLists();
        foreach (Facture u in factures)
        {
            string str = "";
            str += u.getHorodate() + ":\t";
            str += u.getUtilisateur().getNom()+"\t"
                + u.getItem().getNom()+ " (" + u.getItem().getPrix() + "$)\t";
            itemList.AddItem(str);
        }
    }






    private void onNouveau()
    {
        switch (mode)
        {
            case Mode.Utilisateur:
                WindowDialog newUser = (WindowDialog)GetNode("NouvelUtilisateur");

                newUser.PopupCenteredRatio();
                break;
            case Mode.Item:
                WindowDialog newItem = (WindowDialog)GetNode("NouvelItem");

                newItem.PopupCenteredRatio();
                break;
        }
    }

    private void onModifier()
    {
        Godot.TabContainer tabContainer = (Godot.TabContainer)GetNode("Panel/TabContainer");
        
        Godot.ItemList list = (Godot.ItemList)tabContainer.GetChild(tabContainer.CurrentTab);
        int selected = 0;
        try
        {
            selected = list.GetSelectedItems()[0]+1;
            foreach (Utilisateur u in utilisateurs){
                if (u!=null)
                    GD.Print(u.getNom());
            }
            foreach(Item i in items){
                if (i!=null)
                    GD.Print(i.getNom());
            }
        }
        catch(Exception e)
        {
         
        }
        
        this.mode = (Mode)tabContainer.CurrentTab;
        switch (mode)
        {
            case Mode.Utilisateur:
                ModifierUtilisateur newUser = (ModifierUtilisateur)GetNode("ModifierUtilisateur");
                GD.Print(newUser);
                if (utilisateurs[selected] != null){
                    newUser.user = utilisateurs[selected];
                    newUser.admin = this;
                    newUser.PopupCenteredRatio();
                }
                break;

            case Mode.Item:
                ModifierItem newItem = (ModifierItem)GetNode("ModifierItem");
                if (items[selected]!=null){
                    newItem.item = items[selected];
                    newItem.admin = this;
                    newItem.PopupCenteredRatio();
                    

                }
                break;
        }
	}

    private void onModifierUtilisateurHide()
    {
        reloadLists();
        onUtilisateurs();
    }



    private void onNewUserButton()
    {
        WindowDialog newUser = (WindowDialog)GetNode("NouvelUtilisateur");
        Control control = (Control)GetNode("NouvelUtilisateur/Control");
        var surnom = (LineEdit)control.GetNode("surnom");
        var nom = (LineEdit)control.GetNode("nom");
        var email = (LineEdit)control.GetNode("email");
        var mdp = (LineEdit)control.GetNode("mdp");

        Utilisateur nouveau = new Utilisateur(nom.Text, surnom.Text, email.Text, mdp.Text);
        current.database.creerUtilisateur(nouveau);
        newUser.Hide();
        onUtilisateurs();
    }
	
	private void onNewItemButton()
    {
        WindowDialog newUser = (WindowDialog)GetNode("NouvelItem");
        Control control = (Control)GetNode("NouvelItem/Control");
        var nom = (LineEdit)control.GetNode("Nom");
        var prix = (LineEdit)control.GetNode("Prix");
        Item nouveau = new Item(nom.Text, Convert.ToDouble(prix.Text));
        current.database.creerItem(nouveau);
        newUser.Hide();
        onItems();
    }
	
	private void _on_TabContainer_tab_changed(int tab)
    {
        if (tab == (int)Mode.Utilisateur)
        {
            mode = Mode.Utilisateur;
            reloadLists();
            var utilisateurList = (Godot.ItemList)GetNode("Panel/TabContainer/Utilisateurs");
            utilisateurList.Clear();
            foreach (Utilisateur u in utilisateurs)
            {
                if(u != null){
                    string str = "";
                    str = String.Format("{0,3}: {1,-15} {2,-30} - {3,12}", u.getId(), u.getSurnom(), "(" + u.getNom() + ")", u.getDette().ToString("C"));

                    utilisateurList.AddItem(str);
                }
            }
            var control = (Control)GetNode("Panel/Control");
            control.Show();
        }
    }
}




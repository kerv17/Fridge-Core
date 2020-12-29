using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fridge_2_0;
using System.Net;
using Godot;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using MySql.Data.MySqlClient;

public class CurrentUser:Godot.Node
{
	public Utilisateur user;
	string connectionString = "";

	public BD database;// new BD();

	private void _on_Button_pressed()
	{
		Helper.dropTableaux(database.GetConnection());
		Helper.creerTableaux(database.GetConnection());
	}

	public void loadDBParams(){
		File file = new File();

		file.OpenEncryptedWithPass("user://dbFile.dat",File.ModeFlags.Read,OS.GetUniqueId());
		connectionString = file.GetAsText();
		file.Close();
		database = new BD(connectionString);
	}

	public void saveDBParams(){
		File file = new File();
		try{
			file.OpenEncryptedWithPass("user://dbFile.dat",File.ModeFlags.Write,OS.GetUniqueId());
			//file.Open("user://dbFile.dat",File.ModeFlags.Write);
			file.StoreString(database.GetConnectionString());	
			file.Close();
		}
		catch{}
	}

    public override void _Ready()
    {
        try{loadDBParams();}
		catch{
				database =new BD();
				GD.Print("Reuse");
		}
		
		saveDBParams();


		base._Ready();
    }
}



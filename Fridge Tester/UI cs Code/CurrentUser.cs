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

	public BD database = new BD();// new BD();

	private void _on_Button_pressed()
	{
		Helper.dropTableaux(database.GetConnection());
		Helper.creerTableaux(database.GetConnection());
	}
}



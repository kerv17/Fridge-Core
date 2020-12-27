/*
 * BD.cs
 * 
 * @date 12 Fév 2020
 * 
 * Interface avec la base de données SQL
 * 
 */

using Fridge_2_0;


namespace Fridge_2_0
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Data.SqlClient;
	using Godot;
	using MySql.Data.MySqlClient;

	public class BD
	{
		public BD()
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("host", "192.168.1.120");
			dict.Add("port", "3306");
			dict.Add("Database", "test");
			dict.Add("Encrypt", "false");
			//dict.Add("Persist Security Info", "False");
			dict.Add("UID", "Fridge");
			dict.Add("PWD", "comic123");
			
			connectionString_ = "";
			foreach (KeyValuePair<string,string> line in dict)
			{
				connectionString_ += line.Key + "=" + line.Value + ";";
			}
			GD.Print(connectionString_);
			
			
			cnn_ = new MySqlConnection(connectionString_);
			cnnUser_ = new MySqlConnection(connectionString_);
			cnnItems_ = new MySqlConnection(connectionString_);
			cnnFacture_ = new MySqlConnection(connectionString_);
			refresh();
			//Le string va dépendre des informations sur le Database. Il y avait mon IP, donc je l'ai enlevé


		}

		public void refresh()
		{
			listeU = GetUtilisateurs();
			listeI = GetItems();
			listeF = GetFactures();

		}


		private string connectionString_;
		private MySqlConnection cnn_;
		private MySqlConnection cnnUser_;
		private MySqlConnection cnnItems_;
		private MySqlConnection cnnFacture_;

		public List<Utilisateur> listeU;
		public List<Item> listeI;
		public List<Facture> listeF;

		public MySqlConnection GetConnection()
		{
			if (cnn_ != null)
				return cnn_;
			else
				Console.WriteLine("No Cnn_ found.");
			return null;
		}

		//Crée une liste d'objets Utilisateur à partir de la DB
		public List<Utilisateur> GetUtilisateurs()
		{
			cnnUser_.Open();
			List<Utilisateur> liste = new List<Utilisateur>();
			liste.Add(null);
			string command = "Select * from Utilisateur";

			MySqlDataReader dataReader = Helper.getReader(command, cnnUser_);


			Utilisateur utilisateur;
			while ((utilisateur = Helper.readUtilisateur(dataReader, this)) != null)
			{
				liste.Add(utilisateur);
			}
			cnnUser_.Close();
			return liste;
		}

		//Crée un objet Utilisateur à partir d'un surnom correspondant dans la DB
		public Utilisateur GetUtilisateur(string surnom)
        {
			
			cnnUser_.Open();
			string command = "Select * from Utilisateur Where surnom ='" + surnom+"';";

			MySqlDataReader dataReader = Helper.getReader(command, cnnUser_);
			Utilisateur utilisateur;
			utilisateur = Helper.readUtilisateur(dataReader, this);
			cnnUser_.Close();
			return utilisateur;
		}

		//Crée un objet Utilisateur à partir d'un id correspondant dans la DB
		public Utilisateur GetUtilisateur(int id)
		{
			cnnUser_.Open();
			string command = "Select * from Utilisateur Where id =" + id;

			MySqlDataReader dataReader = Helper.getReader(command, cnnUser_);
			Utilisateur utilisateur;
			utilisateur = Helper.readUtilisateur(dataReader, this);
			cnnUser_.Close();
			return utilisateur;
		}

		//Remplace les infos d'un Utilisateur dans la DB par celles du objet
		public void OverrideUtilisateur(Utilisateur utilisateur)
		{
			string commandNom =" nom = '" + utilisateur.getNom() + "' ";
			string commandSurnom = " surnom = '" + utilisateur.getSurnom()+"' ";
			string commandEmail = " email = '" + utilisateur.getEmail() + "' ";
			string commandMPD = " mdp = '" + utilisateur.getMotDePasse() + "' ";
			string commandDette = " dette = " + utilisateur.getDette()+ " ";

			List<string> list = new List<string>();
			list.Add(commandEmail);
			list.Add(commandDette);
			list.Add(commandMPD);
			list.Add(commandNom);
			list.Add(commandSurnom);

			foreach(string s in list){
				string commandtext =
					"UPDATE utilisateur SET" +
					s +
					"WHERE id = " + utilisateur.getId() + ";";

				
				Helper.executeCommand(commandtext, cnn_);
			}


		}

		public void creerUtilisateur(Utilisateur utilisateur)
		{
			string commandtext =
				"INSERT INTO Utilisateur(nom,surnom,email,mdp) VALUES(" +
				"'" + utilisateur.getNom() + "', " +
				"'" + utilisateur.getSurnom() + "', " +
				"'" + utilisateur.getEmail() + "', " +
				"'" + utilisateur.getMotDePasse() + "'" +
				");";
			Helper.executeCommand(commandtext, cnn_);
		}


		public List<Item> GetItems()
		{
			List<Item> liste = new List<Item>();
			liste.Add(null);
			cnnItems_.Open();
			string command = "Select * from Produit";

			MySqlDataReader dataReader = Helper.getReader(command, cnnItems_);


			Item item;
			while ((item = Helper.readItem(dataReader, this)) != null)
			{
				liste.Add(item);
			}
			cnnItems_.Close();
			return liste;
		}

		public Item GetItem(string nom)
		{
			cnnItems_.Open();
			string command = "Select * from Produit Where nom =" + nom;

			MySqlDataReader dataReader = Helper.getReader(command, cnnItems_);
			Item item;
			item = Helper.readItem(dataReader, this);
			cnnItems_.Close();
			return item;
		}

		public Item GetItem(int id)
		{
			cnnItems_.Open();
			string command = "Select * from Produit Where id =" + id;

			MySqlDataReader dataReader = Helper.getReader(command, cnnItems_);
			Item item;
			item = Helper.readItem(dataReader, this);
			cnnItems_.Close();
			return item;
		}

		public void OverrideItem(Item item)
		{
			string commandNom =" nom = '" + item.getNom() + "' ";
			string commandCout = " cout = " + item.getPrix()+" ";
			string commandStock =" stock = " + item.getStock()+ " ";

			List<string> list = new List<string>();
			list.Add(commandNom);
			list.Add(commandCout);
			list.Add(commandStock);
			foreach(string s in list){
				string commandtext =
					"UPDATE produit SET" +
					s +
					"WHERE id = " + item.getId() + ";";

				
				Helper.executeCommand(commandtext, cnn_);
			}
		}

		public void creerItem(Item item)
		{
			string commandtext =
				"INSERT INTO Produit(nom,cout,stock) VALUES(" +
				"'" + item.getNom() + "', " +
				"'" + item.getPrix() + "', " +
				"'" + item.getStock() + "'" +
				");";
			Helper.executeCommand(commandtext, cnn_);
		}




		public List<Facture> GetFactures()
		{
			List<Facture> liste = new List<Facture>();
			liste.Add(null);
			cnnFacture_.Open();
			string command = "Select * from Facture";
			string paramSupp = "";
			command += " " + paramSupp;
			command += " ORDER BY horodate ASC";

			MySqlDataReader dataReader = Helper.getReader(command, cnnFacture_);


			Facture facture;
			while ((facture = Helper.readFacture(dataReader, this)) != null)
				{
					liste.Add(facture);
				}
			cnnFacture_.Close();
			return liste;
		}

		



		public Facture GetFacture(int id)
		{
			cnnFacture_.Open();
			string command = "Select * from Facture Where id =" + id;

			MySqlDataReader dataReader = Helper.getReader(command, cnnFacture_);
			Facture facture;
			facture = Helper.readFacture(dataReader, this);
			cnnFacture_.Close();
			return facture;
		}

		public void creerFacture(Facture facture)
		{
			string commandtext =
				"INSERT INTO facture(utilisateur,produit,prix,horodate) VALUES(" +
				"'" + facture.getUtilisateur().getId() + "', " +
				"'" + facture.getItem().getId() + "', " +
				"'" + facture.getPrix() +"'," +
				Helper.formatHorodate(facture.getHorodate()) +
				");";
			Helper.executeCommand(commandtext, cnn_);
		}

	}

	//Une classe qui me permet de ne pas répéter 20 fois les mêmes lignes de code
	public static class Helper
	{

		public static Utilisateur readUtilisateur(MySqlDataReader reader, BD database)
		{
			
			if (!reader.Read())
				return null;
			int id;
			string nom, surnom, email, mdp;
			double dette;
			List<Item> items = new List<Item>();
			items = database.GetItems();
			List<Facture> factures = new List<Facture>();

			id = (int)reader.GetValue(0);
			nom = (string)reader.GetValue(1);
			surnom = (string)reader.GetValue(2);
			email = (string)reader.GetValue(3);
			mdp = (string)reader.GetValue(4);
			dette = (double)(decimal)reader.GetValue(5);

			Utilisateur utilisateur = new Utilisateur(
				id, nom, surnom, email, mdp, dette);
			
			//factures = database.GetFactures(utilisateur, null);
			return utilisateur;
		}

		public static Item readItem(MySqlDataReader reader, BD database)
		{
			if (!reader.Read())
				return null;

			int id, stock;
			string nom;
			double prix;

			id = (int)reader.GetValue(0);
			nom = (string)reader.GetValue(1);
			prix = Convert.ToDouble(reader.GetValue(2));
			stock = (int)reader.GetValue(3);
			Item item = new Item(id, nom, prix, stock);
			database.GetConnection().Close();
			
			return item;

		}

		public static Facture readFacture(MySqlDataReader reader, BD database)
		{
			if (!reader.Read())
				return null;
			
			int id;
			Utilisateur utilisateur = null;
			Item item;
			double prix;
			DateTime horodate;

			id = (int)reader.GetValue(0);
			utilisateur = database.listeU[(int)reader.GetValue(1)];
			item = database.listeI[(int)reader.GetValue(2)];
			prix = Convert.ToDouble(reader.GetValue(3));
			horodate = (DateTime)reader.GetValue(4);
			database.GetConnection().Close();
			Facture facture = new Facture(id, utilisateur, item, prix, horodate);
			utilisateur.ajouterFacture(facture);
			item.ajouterFacture(facture);
			return facture;
		}


		public static MySqlDataReader getReader(string request, MySqlConnection connection)
		{
			MySqlCommand command = new MySqlCommand(request, connection);
			MySqlDataReader dataReader = command.ExecuteReader();

			return dataReader;
		}

		public static void executeCommand(string request, MySqlConnection connection)
		{
			connection.Open();
			MySqlCommand command = new MySqlCommand(request, connection);
			command.ExecuteNonQuery();
			connection.Close();
		}

		public static string formatHorodate(DateTime dateTime)
		{
			string output = "'";

			output += dateTime.Year + "-";
			output += dateTime.Month + "-";
			output += dateTime.Day + " ";
			output += dateTime.Hour + ":";
			output += dateTime.Minute + ":";
			output += dateTime.Second + "'";
			return output;
		}

		//Installe les tableaux du SQL
		public static void creerTableaux(MySqlConnection c)
		{
			
			creerTableauUtilisateur(c);
			creerTableauProduits(c);
			creerTableauFactures(c);
			
		}


		//Supprime tous les tableaux du SQL
		public static void dropTableaux(MySqlConnection c)
		{
			dropTableau("Facture", c);
			dropTableau("Utilisateur",c);
			dropTableau("Produit", c);
			
		}

		public static void dropTableau(string nom, MySqlConnection c)
        {
			
			string text = "DROP TABLE IF EXISTS " + nom+";";
			executeCommand(text, c);
        }





		private static void creerTableauUtilisateur(MySqlConnection c)
		{
			string command =
				"CREATE TABLE Utilisateur (" +
				"id INT AUTO_INCREMENT PRIMARY KEY," +
				"nom VARCHAR(20) NOT NULL," +
				"surnom VARCHAR(20) NOT NULL," +
				"email VARCHAR(40) NOT NULL," +
				"mdp VARCHAR(16) NOT NULL," +
				"dette DECIMAL(5,2) DEFAULT 000.00" +
				"" +
				");";

			executeCommand(command, c);
		}

		private static void creerTableauProduits(MySqlConnection c)
		{
			string command =
				"CREATE TABLE Produit (" +
				"id INT AUTO_INCREMENT PRIMARY KEY," +
				"nom VARCHAR(20) NOT NULL," +
				"cout DECIMAL(4,2) DEFAULT 00.00," +
				"stock INT DEFAULT 0" +
				"" +
				");";
			executeCommand(command, c);
		}

		private static void creerTableauFactures(MySqlConnection c)
		{
			string command =
				"CREATE TABLE Facture (" +
				"id INT AUTO_INCREMENT," +
				"utilisateur INT NOT NULL," +
				"produit INT NOT NULL," +
				"prix DECIMAL(4,2) DEFAULT 00.00," +
				"horodate TIMESTAMP NOT NULL," +
				"PRIMARY KEY (id)," +
				"FOREIGN KEY (utilisateur) REFERENCES Utilisateur(id) ON DELETE CASCADE," +
				"FOREIGN KEY (produit) REFERENCES Produit(id) ON DELETE CASCADE" +
				");";
			executeCommand(command, c);
		}
	}


	

}

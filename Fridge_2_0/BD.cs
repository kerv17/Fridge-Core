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
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MySql.Data;
    using MySql.Data.MySqlClient;

    public class BD
    {
        public BD()
        {
            connectionString_ = "";//Le string va dépendre des informations sur le Database. Il y avait mon IP, donc je l'ai enlevé
            connect();
            
        }

        public void connect()
        {
            MySqlConnection cnn = new MySqlConnection(connectionString_);
            cnn_ = cnn;

        }


        private string connectionString_;
        private MySqlConnection cnn_;

        //Crée une liste d'objets Utilisateur à partir de la DB
        public List<Utilisateur.Utilisateur> GetUtilisateurs() {
            List<Utilisateur.Utilisateur> liste = new List<Utilisateur.Utilisateur>();
            cnn_.Open();

            string command = "Select * from Utilisateur";

            MySqlDataReader dataReader = Helper.getReader(command, cnn_);


            Utilisateur.Utilisateur utilisateur;
            while ((utilisateur = Helper.readUtilisateur(dataReader, this)) != null)
            {
                liste.Add(utilisateur);
            }
            cnn_.Close();
            return liste;
        }

        //Crée un objet Utilisateur à partir d'un surnom correspondant dans la DB
        public Utilisateur.Utilisateur GetUtilisateur(string surnom)
        {
            cnn_.Open();
            string command = "Select * from Utilisateur Where surnom ="+ surnom;

            MySqlDataReader dataReader = Helper.getReader(command, cnn_);
            Utilisateur.Utilisateur utilisateur;
            utilisateur = Helper.readUtilisateur(dataReader, this);
            cnn_.Close();
            return utilisateur;
        }

        //Crée un objet Utilisateur à partir d'un id correspondant dans la DB
        public Utilisateur.Utilisateur GetUtilisateur(int id)
        {
            cnn_.Open();
            string command = "Select * from Utilisateur Where id =" + id;

            MySqlDataReader dataReader = Helper.getReader(command, cnn_);
            Utilisateur.Utilisateur utilisateur;
            utilisateur = Helper.readUtilisateur(dataReader, this);
            cnn_.Close();
            return utilisateur;
        }

        //Remplace les infos d'un Utilisateur dans la DB par celles du objet
        public void OverrideUtilisateur(Utilisateur.Utilisateur utilisateur)
        {
            string commandtext =
                "UPDATE Utilisateur SET" +
                " email = '"+ utilisateur.getEmail() +"', "+
                "mdp = '"+ utilisateur.getMotDePasse()+"', "+
                "dette = " + utilisateur.getDette() +
                "WHERE id = " + utilisateur.getId() + ";";
            Helper.executeCommand(commandtext, cnn_);
        }

        public void creerUtilisateur(Utilisateur.Utilisateur utilisateur)
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


        public List<Item.Item> GetItems()
        {
            List<Item.Item> liste = new List<Item.Item>();
            cnn_.Open();

            string command = "Select * from Produit";

            MySqlDataReader dataReader = Helper.getReader(command, cnn_);


            Item.Item item;
            while ((item = Helper.readItem(dataReader, this)) != null)
            {
                liste.Add(item);
            }
            cnn_.Close();
            return liste;
        }

        public Item.Item GetItem(string nom)
        {
            cnn_.Open();
            string command = "Select * from Produit Where nom =" + nom;

            MySqlDataReader dataReader = Helper.getReader(command, cnn_);
            Item.Item item;
            item = Helper.readItem(dataReader, this);
            cnn_.Close();
            return item;
        }

        public Item.Item GetItem(int id)
        {
            cnn_.Open();
            string command = "Select * from Produit Where id =" + id;

            MySqlDataReader dataReader = Helper.getReader(command, cnn_);
            Item.Item item;
            item = Helper.readItem(dataReader, this);
            cnn_.Close();
            return item;
        }

        public void OverrideItem(Item.Item item)
        {
            string commandtext =
                "UPDATE Utilisateur SET" +
                " nom = '" + item.getNom() + "', " +
                "cout = " + item.getPrix() + ", " +
                "stock = " + item.getStock() +
                "WHERE id = " + item.getId() + ";";
            Helper.executeCommand(commandtext, cnn_);
        }

        public void creerItem(Item.Item item)
        {
            string commandtext =
                "INSERT INTO produits(nom,cout,stock) VALUES(" +
                "'" + item.getNom() + "', " +
                "'" + item.getPrix() + "', " +
                "'" + item.getStock() + "'" +
                ");";
            Helper.executeCommand(commandtext, cnn_);
        }




        public List<Facture.Facture> GetFactures(string paramSupp = null)
        {
            List<Facture.Facture> liste = new List<Facture.Facture> ();
            cnn_.Open();

            string command = "Select * from Facture";
            if (paramSupp != null)
                command += " " + paramSupp;
            command += " ORDER BY horodate DESC";

            MySqlDataReader dataReader = Helper.getReader(command, cnn_);


            Facture.Facture facture;
            while ((facture = Helper.readFacture(dataReader,this)) != null)
            {
                liste.Add(facture);
            }
            cnn_.Close();
            return liste;
        }

        public Facture.Facture GetFacture(int id)
        {
            cnn_.Open();
            string command = "Select * from Facture Where id =" + id;

            MySqlDataReader dataReader = Helper.getReader(command, cnn_);
            Facture.Facture facture;
            facture = Helper.readFacture(dataReader, this);
            cnn_.Close();
            return facture;
        }

        public void creerFacture(Facture.Facture facture)
        {
            string commandtext =
                "INSERT INTO facture(utilisateur,produit,horodate) VALUES(" +
                "'" + facture.getUtilisateur().getId() + "', " +
                "'" + facture.getItem().getId()+ "', " +
                Helper.formatHorodate(facture.getHorodate())+
                ");";
            Helper.executeCommand(commandtext, cnn_);
        }

    }

    //Une classe qui me permet de ne pas répéter 20 fois les mêmes lignes de code
    public static class Helper
    {

        public static Utilisateur.Utilisateur readUtilisateur(MySqlDataReader reader,BD database){
            if(!reader.Read())
                return null;
            int id;
            string nom, surnom, email, mdp;
            double dette;
            List<Facture.Facture> factures = new List<Facture.Facture>();

            id = (int)reader.GetValue(0);
            nom = (string)reader.GetValue(1);
            surnom = (string)reader.GetValue(2);
            email = (string)reader.GetValue(3);
            mdp = (string)reader.GetValue(4);
            dette = (double)(decimal)reader.GetValue(5);

            factures = database.GetFactures("WHERE utilisateur =" + id);

            Utilisateur.Utilisateur utilisateur = new Utilisateur.Utilisateur(
                id, nom, surnom, email, mdp, dette, factures);
            return utilisateur;
        }

        public static Item.Item readItem(MySqlDataReader reader,BD database)
        {
            if (!reader.Read())
                return null;

            int id, stock;
            string nom;
            double prix;
            List<Facture.Facture> factures = new List<Facture.Facture>();

            id = (int)reader.GetValue(0);
            nom = (string)reader.GetValue(1);
            prix = (double)reader.GetValue(2);
            stock = (int)reader.GetValue(3);
            factures = database.GetFactures("WHERE produit =" + id);

            Item.Item item = new Item.Item(id, nom, prix, stock, factures);
            return item;

        }

        public static Facture.Facture readFacture(MySqlDataReader reader, BD database)
        {
            if (!reader.Read())
                return null;

            int id;
            Utilisateur.Utilisateur utilisateur;
            Item.Item item;
            DateTime horodate;

            id = (int)reader.GetValue(0);
            utilisateur = database.GetUtilisateur((int)reader.GetValue(1));
            item = database.GetItem((int)reader.GetValue(2));
            horodate = (DateTime)reader.GetValue(3);

            Facture.Facture facture = new Facture.Facture(id, utilisateur, item, horodate);
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
            MySqlCommand command = new MySqlCommand(request, connection);
            command.ExecuteNonQuery();
        }

        public static string formatHorodate(DateTime dateTime)
        {
            string output="'";

            output += dateTime.Year + "-";
            output += dateTime.Month + "-";
            output += dateTime.Day + " ";
            output += dateTime.Hour + ":";
            output += dateTime.Minute + ":";
            output += dateTime.Second + "'";
            return output;
        }

        //Installe les tableaux du SQL
        static void creerTableaux(MySqlConnection c)
        {
            creerTableauUtilisateur(c);
            creerTableauProduits(c);
            creerTableauFactures(c);
        }


        //Supprime tous les tableaux du SQL
        static void dropTableaux(MySqlConnection c)
        {
            string command =
                "DROP TABLE facture; DROP TABLE membre; DROP TABLE produits;";
            executeCommand(command, c);
        }






        private static void creerTableauUtilisateur(MySqlConnection c)
        {
            string command =
                "CREATE TABLE Utilisateur (" +
                "id INT AUTO_INCREMENT," +
                "nom VARCHAR(20) NOT NULL," +
                "surnom VARCHAR(20) NOT NULL," +
                "email VARCHAR(40) NOT NULL," +
                "mdp VARCHAR(16) NOT NULL," +
                "dette DECIMAL(5,2) DEFAULT(000.00)," +
                "PRIMARY KEY (id)" +
                ");";

            executeCommand(command, c);
        }

        private static void creerTableauProduits(MySqlConnection c)
        {
            string command =
                "CREATE TABLE Produit (" +
                "id INT AUTO_INCREMENT," +
                "nom VARCHAR(20) NOT NULL," +
                "cout DECIMAL(4,2) DEFAULT(00.00)," +
                "stock INT DEFAULT(0)," +
                "PRIMARY KEY (id)" +
                ");";
            executeCommand(command, c);
        }

        private static void creerTableauFactures(MySqlConnection c)
        {
            string command =
                "CREATE TABLE Produit (" +
                "id INT AUTO_INCREMENT," +
                "utilisateur INT NOT NULL," +
                "produit INT NOT NULL," +
                "horodate TIMESTAMP NOT NULL," +
                "PRIMARY KEY (id)" +
                "FOREIGN KEY (utilisateur) REFERENCES Utilisateur(id) ON DELETE CASCADE,"+
                "FOREIGN KEY (produit) REFERENCES Produit(id) ON DELETE CASCADE" +
                ");";
            executeCommand(command, c);
        }
    }
}

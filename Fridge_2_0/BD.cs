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

    class BD
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
            while ((utilisateur = Helper.readUtilisateur(dataReader)) != null)
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
            utilisateur = Helper.readUtilisateur(dataReader);
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
            utilisateur = Helper.readUtilisateur(dataReader);
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


    }

    //Une classe qui me permet de ne pas répéter 20 fois les mêmes lignes de code
    public static class Helper
    {

        public static Utilisateur.Utilisateur readUtilisateur(MySqlDataReader reader){
            if(!reader.Read())
                return null;
            int id;
            string nom, surnom, email, mdp;
            double dette;

            id = (int)reader.GetValue(0);
            nom = (string)reader.GetValue(1);
            surnom = (string)reader.GetValue(2);
            email = (string)reader.GetValue(3);
            mdp = (string)reader.GetValue(4);
            dette = (double)(decimal)reader.GetValue(5);

            Utilisateur.Utilisateur utilisateur = new Utilisateur.Utilisateur(
                id, nom, surnom, email, mdp, dette);
            return utilisateur;
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
    }
}


using Fridge_2_0;
using Godot;
using System.Collections.Generic;

public class CurrentUser:Godot.Node
{
	public Utilisateur user;
	

	//DB
	#region DB
	public BD database;// new BD();
	string connectionString = "";
	private void _on_Button_pressed()
	{
		Helper.dropTableaux(database.GetConnection());
		Helper.creerTableaux(database.GetConnection());
	}
	#endregion
	////////////

    //LOCAL SAVING/LOADING
	#region LOADSAVE
	public void loadDBParams(){
		Godot.File file = new Godot.File();

		file.OpenEncryptedWithPass("user://dbFile.dat",Godot.File.ModeFlags.Read,OS.GetUniqueId());
		connectionString = file.GetAsText();
		file.Close();
		database = new BD(connectionString);
		
	}

	public void saveDBParams(){
		Godot.File file = new Godot.File();
		try{
			file.OpenEncryptedWithPass("user://dbFile.dat",File.ModeFlags.Write,OS.GetUniqueId());
			//file.Open("user://dbFile.dat",File.ModeFlags.Write);
			file.StoreString(database.GetConnectionString());	
			file.Close();
		}
		catch{}
	}

	public void loadMailParams(){
		
		//Read file with mail info
		Godot.File file = new Godot.File();
		file.OpenEncryptedWithPass("user://mailFile.dat",Godot.File.ModeFlags.Read,OS.GetUniqueId());
		string serviceUrl,port,email,password;
		serviceUrl = file.GetLine();
		port = file.GetLine();
		email = file.GetLine();
		password = file.GetLine();
		file.Close();

		//Create mail object
		mail = new Mail(serviceUrl,port.ToInt(),email,password);
	}
	
	public void saveMailParams(){
		Godot.File file = new Godot.File();
		try{
			file.OpenEncryptedWithPass("user://mailFile.dat",File.ModeFlags.Write,OS.GetUniqueId());
			//file.Open("user://mailFile.dat",File.ModeFlags.Write);
			
			file.Close();
		}
		catch{}
	}

	#endregion
	//////////////////////


	//MAILING FUNCTIONALITY
	#region Mailing
		private int automailCooldown;
		private double minDebtAutomail;
		private int[] time = {0,0};
		public Mail mail;

		//This event occurs every 3600 seconds from boot time, so every hour
		private void _on_MailTimer_timeout(){
			if(time[1]++>=24){
				time[1]= 0;
				time[0]++;
			}
			if (time[0]>=automailCooldown){
				automail();
			}
		}

		//Call this function to mail people
		public void automail(){
			database.refresh();
			List<Utilisateur> debtors = database.GetUtilisateurs(minDebtAutomail);
			foreach(Utilisateur u in debtors){
				mail.sendMailToUser(u);
			}
			time[0] = 0;
			time[1] = 0;
		}
	#endregion
	///////////////////////


	//ACTIONS TO DO ON BOOT
	public override void _Ready()
    {	
        try{loadDBParams();}
		catch{
				database =new BD();
				GD.Print("Reuse");
				saveDBParams();
		}
		base._Ready();
		Helper.dropTableaux(database.GetConnection());
		Helper.creerTableaux(database.GetConnection());
    }
	///////////////////////




}



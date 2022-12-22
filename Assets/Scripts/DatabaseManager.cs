using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using MySql.Data.MySqlClient;
using UnityEngine;


public class DatabaseManager : MonoBehaviour
{
    #region VARIABLES

    private string path = "Assets/Scripts/routes.txt";
    private string train = "Assets/Scripts/train.txt";
    private string machinist = "Assets/Scripts/machinist.txt";
    private string myCertificate = "Assets/SSL/ca-certificate.crt";

    [Header("Database Properties")] 
    string Host = "vultr-prod-3bfe867e-fd1a-4661-964c-7a93d5c43308-vultr-prod-258c.vultrdb.com";

    string Port = "16751";
    string User = "mrtadmin";
    string Password = "hcL^D7Vh7SLcFZ";
    string Database = "mrtdb";
    
    
    private MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
    private MySqlConnection connection = new MySqlConnection();
    

    #endregion

    #region UNITY METHODS
    
    private void Start()
    {
        SetupConnection();
        
        // AddTrain();
        // AddMachinist();

    }

    #endregion

    #region METHODS
    
   

    void SetupConnection()
    {
        conn_string.UserID = User;
        conn_string.Password = Password;
        conn_string.Server = Host;
        conn_string.Port = Convert.ToUInt32(Port);
        conn_string.Database = Database;
        conn_string.SslMode = MySqlSslMode.VerifyCA;
        conn_string.CertificateFile = myCertificate;
        
        connection = new MySqlConnection(conn_string.ToString());
        print("Connection ready");
    }
    
    
    
    

    // private void AddRoutes()
    // {
    //     
    //     using var cmd = new MySqlCommand();
    //     cmd.Connection = connection;
    //     
    //     
    //     // Read text file
    //     foreach (string line in System.IO.File.ReadLines(path))
    //     {
    //         List<string> data = new List<string>(line.Split(','));
    //         string origin = data[0];
    //         string destination = data[1];
    //         string cost = data[2];
    //         
    //         
    //         try
    //         {
    //             print("About to establish connection");
    //             using (connection)
    //             {
    //                 connection.Open();
    //                 print("MySQL - Opened Connection");
    //                 cmd.CommandText = $"SHOW DATABASES";
    //                     // $"INSERT IGNORE INTO routes (station_origin,station_destination,price) VALUES('{origin}','{destination}',{cost})";
    //                     
    //                          
    //                 cmd.ExecuteNonQuery();
    //             }
    //         }
    //         catch (MySqlException exception)
    //         {
    //             print(exception.Message);
    //         }
    //         
    //
    //         
    //     }
    //
    //     print("Successfully added new routes");
    //
    //
    // }
    //
    // private void AddMachinist()
    // {
    //     
    //     using var cmd = new MySqlCommand();
    //     cmd.Connection = connection;
    //     
    //     
    //     // Read text file
    //     foreach (string line in System.IO.File.ReadLines(machinist))
    //     {
    //         List<string> data = new List<string>(line.Split(','));
    //         string name = data[0];
    //         
    //         
    //         try
    //         {
    //             using (connection)
    //             {
    //                 connection.Open();
    //                 print("MySQL - Opened Connection");
    //                 cmd.CommandText =
    //                     $"INSERT IGNORE INTO machinist (machinist_name) VALUES('{name}')";
    //                     
    //                          
    //                 cmd.ExecuteNonQuery();
    //             }
    //         }
    //         catch (MySqlException exception)
    //         {
    //             print(exception.Message);
    //         }
    //         
    //
    //         
    //     }
    //
    //     print("Successfully added new machinist");
    //
    //
    // }
    //
    //
    // private void AddTrain()
    // {
    //     
    //     using var cmd = new MySqlCommand();
    //     cmd.Connection = connection;
    //     
    //     
    //     // Read text file
    //     foreach (string line in System.IO.File.ReadLines(train))
    //     {
    //         List<string> data = new List<string>(line.Split(','));
    //         string rail = data[0];
    //         string type = data[1];
    //         string carriage = data[2];
    //
    //
    //         try
    //         {
    //             using (connection)
    //             {
    //                 connection.Open();
    //                 print("MySQL - Opened Connection");
    //                 cmd.CommandText =
    //                     $"INSERT IGNORE INTO train (railway,train_type,carriage) VALUES('{rail}','{type}',{carriage})";
    //                     
    //                          
    //                 cmd.ExecuteNonQuery();
    //             }
    //         }
    //         catch (MySqlException exception)
    //         {
    //             print(exception.Message);
    //         }
    //         
    //
    //         
    //     }
    //
    //     print("Successfully added new train");
    //
    //
    // }
    //
    //
    //
    // public void AddUser(string phoneNumber, string fullName, string email,string pin)
    // {
    //     
    //     try
    //     {
    //         using (connection)
    //         {
    //             connection.Open();
    //             using var cmd = new MySqlCommand();
    //             cmd.Connection = connection;
    //             cmd.CommandText = $"INSERT IGNORE INTO users (name,email,phone_number,pin) VALUES ('{fullName}','{email}','{phoneNumber}','{pin}')";
    //             cmd.ExecuteNonQuery();
    //             print("MySQL - Opened Connection");
    //         }
    //     }
    //     catch (MySqlException exception)
    //     {
    //         print(exception.Message);
    //     }
    //     
    // }
    
    

    

    #endregion
}
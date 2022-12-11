using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using UnityEngine;


public class DatabaseManager : MonoBehaviour
{
    #region VARIABLES

    private string path = "Assets/Scripts/routes.txt";

    [Header("Database Properties")] public string User = "doadmin";
    public string Password = "AVNS_KoU3bp-RJSxuf5mk-xU";
    public string Host = "db-mysql-sgp1-09156-do-user-13057152-0.b.db.ondigitalocean.com";
    public string Port = "25060";
    public string Database = "mrtdb";
    
    private MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
    private MySqlConnection connection = new MySqlConnection();
    

    #endregion

    #region UNITY METHODS
    
    private void Start()
    {
        SetupConnection();
        AddRoutes();

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
        connection = new MySqlConnection(conn_string.ToString());
    }

    private void AddRoutes()
    {
        
        using var cmd = new MySqlCommand();
        cmd.Connection = connection;
        
        
        // Read text file
        foreach (string line in System.IO.File.ReadLines(path))
        {
            List<string> data = new List<string>(line.Split(','));
            string origin = data[0];
            string destination = data[1];
            string cost = data[2];
            
            
            try
            {
                using (connection)
                {
                    connection.Open();
                    print("MySQL - Opened Connection");
                    cmd.CommandText =
                        $"INSERT IGNORE INTO routes (station_origin,station_destination,price) VALUES('{origin}','{destination}',{cost})";
                        
                             
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException exception)
            {
                print(exception.Message);
            }
            

            
        }

        print("Successfully added new routes");


    }

    
    
    public void AddUser(string phoneNumber, string fullName, string email,string pin)
    {
        
        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"INSERT IGNORE INTO users (name,email,phone_number,pin) VALUES ('{fullName}','{email}','{phoneNumber}','{pin}')";
                cmd.ExecuteNonQuery();
                print("MySQL - Opened Connection");
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        
    }
    
    

    

    #endregion
}
using System;
using System.Collections.Generic;
using System.Linq;
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
        // AddRoutes();
        // AddTrain();
        // AddMachinist();

    }

    #endregion

    
    
   

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
    
    
    #region DATABASE SETUP
    

    private void AddRoutes()
    {
        
        using var cmd = new MySqlCommand();
        cmd.Connection = connection;
        
        
        // Read text file
        foreach (string line in System.IO.File.ReadLines(path))
        {
            List<string> data = new List<string>(line.Split(','));
            var id = data[0];
            var originId = data[1];
            var destinationId = data[2];
            var cost = data[3];
            
            
            try
            {
                print("About to establish connection");
                using (connection)
                {
                    connection.Open();
                    print("MySQL - Opened Connection to Routes");
                    cmd.CommandText = $"INSERT IGNORE INTO Routes (route_id,station_origin_id,station_destination_id,route_price) VALUES('{id}','{originId}','{destinationId}','{cost}')";
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
    //
    private void AddMachinist()
    {
        
        using var cmd = new MySqlCommand();
        cmd.Connection = connection;
        
        
        // Read text file
        foreach (string line in System.IO.File.ReadLines(machinist))
        {
            List<string> data = new List<string>(line.Split(','));
            string name = data[0];
            
            
            try
            {
                using (connection)
                {
                    connection.Open();
                    print("MySQL - Opened Connection to INSERT MACHINIST");
                    cmd.CommandText =
                        $"INSERT IGNORE INTO Machinists (machinist_name) VALUES('{name}')";
                        
                             
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException exception)
            {
                print(exception.Message);
            }
            
    
            
        }
    
        print("Successfully added new machinist");
    
    
    }
    
    
    private void AddTrain()
    {
        
        using var cmd = new MySqlCommand();
        cmd.Connection = connection;
        
        
        // Read text file
        foreach (string line in System.IO.File.ReadLines(train))
        {
            List<string> data = new List<string>(line.Split(','));
            string type = data[0];
            int carriage = Convert.ToInt32(data[1]);
            int machinistId = Convert.ToInt32(data[2]);
    
    
            try
            {
                using (connection)
                {
                    connection.Open();
                    print("MySQL - Opened Connection");
                    cmd.CommandText =
                        $"INSERT IGNORE INTO train (train_type,carriages,machinist_id) VALUES('{type}',{carriage},{machinistId})";
                        
                             
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException exception)
            {
                print(exception.Message);
            }
            
    
            
        }
    
        print("Successfully added new train");
    
    
    }
    #endregion
    public void AddUser( string fullName, string email,string phoneNumber,string pin,char gender,int age)
    {
        
        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"INSERT IGNORE INTO Users (full_name,email,mobile_number,pin,sex,age) VALUES ('{fullName}','{email}','{phoneNumber}','{pin}','{gender}',{age})";
                cmd.ExecuteNonQuery();
                print("MySQL - Opened Connection");
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();
        
    }
    
    public int GetUserID(string mobileNumber)
    {
        
        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"SELECT user_id from Users WHERE mobile_number='{mobileNumber}'";
                var myReader = cmd.ExecuteReader();
                
                while (myReader.Read())
                {
                    int userID = Convert.ToInt32(myReader.GetString(0));
                    print("User ID: "+userID);
                    connection.Close();
                    return userID;
                }
                print("MySQL - Opened Connection To GET USER ID");
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();

        return 0;
    }

    public Boolean CheckUserEmail(string email)
    {
        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $" SELECT EXISTS(SELECT * from Users WHERE email='{email}')";
                var myReader = cmd.ExecuteReader();
                
                    while (myReader.Read())
                    {
                        int numberExist = Convert.ToInt32(myReader.GetString(0));
                        connection.Close();
                        switch (numberExist)
                        {
                            case 1:
                                print("Email Found!");
                                return true; 
                                
                            
                            default:
                                print("Email Not Found!");
                                return false;
                                
                                
                        }
                    }
                    print("MySQL - Opened Connection To Check Email");
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();

        return false;
    }
    
    public Boolean CheckUserNumber(string phoneNumber)
    {
        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $" SELECT EXISTS(SELECT * from Users WHERE mobile_number='{phoneNumber}')";
                var myReader = cmd.ExecuteReader();
                
                while (myReader.Read())
                {
                    int numberExist = Convert.ToInt32(myReader.GetString(0));
                    connection.Close();
                    switch (numberExist)
                    {
                        case 1:
                            print("Mobile Number Found!");
                            return true; 
                            
                            
                        default:
                            print("Error: Mobile Number Not Found!");
                            return false;
                            
                                
                    }
                }
                print("MySQL - Opened Connection To Check Mobile Number");
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();

        return false;
    }
    
    public Boolean CheckUserPin(string mobileNumber,string pinNumber)
    {
        print("Login Number" + mobileNumber);
        print("Login PIN "+ pinNumber);
        try
        {
            using (connection)
            {
                
                connection.Open();
                print("MySQL - Opened Connection To Check Mobile Number");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $" SELECT pin from Users WHERE mobile_number='{mobileNumber}'";
                var myReader = cmd.ExecuteReader();
                
                while (myReader.Read())
                {
                    string savedPin = (myReader.GetString(0));
                    
                    print("Stored PIN is " + savedPin);
                    if (pinNumber == savedPin)
                    {
                        print("Login Success!");
                        connection.Close();
                        return true;
                    }
                    
                }
                
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();

        return false;
    }

    public Dictionary<string,int> GetStationDict()
    {
        
        Dictionary<string,int> stationDict = new Dictionary<string, int>();
        try
        {
            using (connection)
            {

                connection.Open();
                print("MySQL - Opened Connection To GET STATIONS");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $" SELECT * from Stations";
                var myReader = cmd.ExecuteReader();
                while (myReader.HasRows)
                {
                    
                    while (myReader.Read())
                    {
                        stationDict.Add(myReader.GetString(1),myReader.GetInt32(0));
                    }
                    myReader.NextResult();
                    
                }

                if (!myReader.IsClosed)
                {
                    myReader.Close();
                }
                return stationDict;
                
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();

        return null;
    } 
    public int GetRoutePrice(int originId,int destinationId)
    {
        
        try
        {
            using (connection)
            {
                
                connection.Open();
                print("MySQL - Opened Connection To GET ROUTE PRICE");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $" SELECT route_price from Routes WHERE station_origin_id='{originId}' AND station_destination_id = '{destinationId}'";
                var myReader = cmd.ExecuteReader();
                
                while (myReader.Read())
                {
                    int routePrice = Convert.ToInt32((myReader.GetString(0)));
                    connection.Close();
                    return routePrice;
                }
                
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();

        return 0;
    } 
    public void AddTickets(string TransactionDate,string IdTicket, int TicketPrice, int TicketStatus, int IdUser, int IdTrain, int IdPayment)
    {
        
        using var cmd = new MySqlCommand();
        cmd.Connection = connection;
        
        
        // Read text file
        foreach (string line in System.IO.File.ReadLines(machinist))
        {
            List<string> data = new List<string>(line.Split(','));
            string name = data[0];
            
            
            try
            {
                using (connection)
                {
                    connection.Open();
                    print("MySQL - Opened Connection");
                    cmd.CommandText =
                        $"INSERT IGNORE INTO Tickets (Transaction_Date,ticket_id,price,status,user_id,train_id,payment_id) VALUES('{TransactionDate}','{IdTicket}','{IdUser}','{IdTrain}','{IdPayment}')";
                        
                             
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException exception)
            {
                print(exception.Message);
            }
            
    
            
        }
        connection.Close();
    
        print("Successfully added new Ticket");
    
    
    }
    
    

    

   
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.Networking;


public class DatabaseManager : MonoBehaviour
{
   
    

    
    
    

    
    #region VARIABLES
    

    private string path = "Assets/Scripts/routes.txt";
    
    
    

    [Header("Database Properties")]
    string Host = "";

    string Port = "";
    string User = "";
    string Password = "";
    string Database = "";


    private MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
    private MySqlConnection connection = new MySqlConnection();


    #endregion

    #region CLASSES

    public class Route
    {
        public int RouteId,OriginId,DestId,Price;
        

        public Route(int routeId, int originId, int destId, int price)
        {
            RouteId = routeId;
            OriginId = originId;
            DestId = destId;
            Price = price;
            
        }
    }

    public class Ticket
    {
        
        public int TicketId,UserId, RouteId, PaymentId, StatusId;
        public string Date, Time;

        public Ticket(int ticketId, int userId, int routeId, int paymentId, int statusId,string date,string time)
        {
            TicketId = ticketId;
            UserId = userId;
            RouteId = routeId;
            PaymentId = paymentId;
            StatusId = statusId;
            Date = date;
            Time = time;

            
        }
        
    }

    public class AppUser
    {
        public int UserId;
        public string FullName,Email,Pin,MobileNumber,BirthDate;
        public char Sex;

        public AppUser()
        {
            
        }

        public AppUser(int userId, string birthDate, string fullName, string email, string pin, string mobileNumber, char sex)
        {
            UserId = userId;
            BirthDate = birthDate;
            FullName = fullName;
            Email = email;
            Pin = pin;
            MobileNumber = mobileNumber;
            Sex = sex;
        }
    }
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
        // conn_string.SslMode = MySqlSslMode.VerifyCA;
        // conn_string.CertificateFile = myCertificate;

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
                    cmd.CommandText =
                        $"INSERT IGNORE INTO Routes (route_id,station_origin_id,station_destination_id,route_price) VALUES('{id}','{originId}','{destinationId}','{cost}')";
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
    

    #endregion

    #region User Functions
    public void AddUser(string fullName, string email, string phoneNumber, string pin, char gender, string dateOfBirth)
    {

        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $"INSERT IGNORE INTO Users (full_name,email,mobile_number,pin,sex,date_of_birth) VALUES ('{fullName}','{email}','{phoneNumber}','{pin}','{gender}','{dateOfBirth}')";
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
                    print("User ID: " + userID);
                    connection.Close();
                    return userID;
                }
                if (!myReader.IsClosed)
                {
                    myReader.Close();
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
                if (!myReader.IsClosed)
                {
                    myReader.Close();
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
                if (!myReader.IsClosed)
                {
                    myReader.Close();
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

    public Boolean CheckUserPin(string mobileNumber, string pinNumber)
    {
        print("Login Number" + mobileNumber);
        print("Login PIN " + pinNumber);
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
                if (!myReader.IsClosed)
                {
                    myReader.Close();
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

    public AppUser GetIndividualUserData(int userId)
    {
        AppUser appUser = new AppUser();
        try
        {
            using (connection)
            {

                connection.Open();
                print("MySQL - Opened Connection To GET INDIVIDUAL USER DATA");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $" SELECT * from Users WHERE user_id = '{userId}'";

                var myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    appUser.FullName= myReader.GetString(1);
                    appUser.Sex = Convert.ToChar(myReader.GetString(2));
                    appUser.Email= myReader.GetString(3);
                    appUser.Pin = myReader.GetString(4);
                    appUser.MobileNumber = myReader.GetString(5);
                    appUser.BirthDate = myReader.GetString(6);
                }

                
                    
                

                return appUser;
            }
            
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();
        return appUser;
    }

    public List<AppUser> GetUserList()
    {
        List<AppUser> appUserList = new List<AppUser>();
        
        
        try
        {
            using (connection)
            {
                
                connection.Open();
                print("MySQL - Opened Connection To GET USER LIST");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $" SELECT * from Users";
                                  
                var myReader = cmd.ExecuteReader();
                
                while (myReader.HasRows)
                {
                    print("Found user list");
                    while (myReader.Read())
                    {
                        int DbUserId = Convert.ToInt32(myReader.GetString(0));
                        string DbFullName = myReader.GetString(1);
                        char DbSex = Convert.ToChar(myReader.GetString(2));
                        string DbEmail = myReader.GetString(3);
                        string DbPin = myReader.GetString(4);
                        string DbMobileNumber = myReader.GetString(5);
                        string DbBirthDate= myReader.GetString(6);
                        AppUser newUser = new AppUser(DbUserId, DbBirthDate, DbFullName, DbEmail, DbPin, DbMobileNumber,
                            DbSex);
                        appUserList.Add(newUser);
                        
                    }
                    myReader.NextResult();
                }
                if (!myReader.IsClosed)
                {
                    myReader.Close();
                }
                connection.Close();
                return appUserList;

            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();

        return new List<AppUser>();
    }

    public void SetUserData(int appUserId,string newName,char newGender,string newEmail,string newPin,string newPhone,string newDate )
    {
        
        try
        {
            using (connection)
            {
                
                connection.Open();
                print("MySQL - Opened Connection To SET NEW USER DATA");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $"UPDATE Users SET full_name = '{newName}',sex='{newGender}',email='{newEmail}',pin='{newPin}',mobile_number='{newPhone}',date_of_birth='{newDate}' WHERE user_id = '{appUserId}'";

                var myReader = cmd.ExecuteNonQuery();
                
                
                
                connection.Close();
                

            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();
    }
    #endregion
    #region Admin Functions
    public void AddAdmin(string fullName, string email, string phoneNumber, string pass, char gender, string dateOfBirth)
    {

        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $"INSERT IGNORE INTO Admins(full_name,sex,email,password,mobile_number,date_of_birth) VALUES ('{fullName}','{gender}','{email}','{pass}','{phoneNumber}','{dateOfBirth}')";
                cmd.ExecuteNonQuery();
                print("MySQL - Opened Connection TO ADD ADMIN");
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }

        connection.Close();

    }
    public int GetAdminID(string mobileNumber)
    {

        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"SELECT admin_id from Admins WHERE mobile_number='{mobileNumber}'";
                var myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    int userID = Convert.ToInt32(myReader.GetString(0));
                    print("User ID: " + userID);
                    connection.Close();
                    return userID;
                }
                if (!myReader.IsClosed)
                {
                    myReader.Close();
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
    public Boolean CheckAdminEmail(string email)
    {
        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $" SELECT EXISTS(SELECT * from Admins WHERE email='{email}')";
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
                if (!myReader.IsClosed)
                {
                    myReader.Close();
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

    public Boolean CheckAdminNumber(string phoneNumber)
    {
        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $" SELECT EXISTS(SELECT * from Admins WHERE mobile_number='{phoneNumber}')";
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
                if (!myReader.IsClosed)
                {
                    myReader.Close();
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

    public Boolean CheckAdminPassword(string mobileNumber, string pinNumber)
    {
        print("Login Number" + mobileNumber);
        print("Login PIN " + pinNumber);
        try
        {
            using (connection)
            {

                connection.Open();
                print("MySQL - Opened Connection To Check Mobile Number");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $" SELECT password from Admins WHERE mobile_number='{mobileNumber}'";
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
                if (!myReader.IsClosed)
                {
                    myReader.Close();
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
    #endregion
    
    public Dictionary<string, int> GetStationDict()
    {


        Dictionary<string, int> stationDict = new Dictionary<string, int>();
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
                        stationDict.Add(myReader.GetString(1), myReader.GetInt32(0));
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
    

    public Tuple<string,string> GetStationsFromRouteId(int routeId)
    {
        
        try
        {
            using (connection)
            {
                int originId = 0;
                int destId = 0;
                string originName = "", destName = "";

                connection.Open();
                print("MySQL - Opened Connection To GET ROUTE PRICE");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $" SELECT station_origin_id,station_destination_id from Routes WHERE route_id = '{routeId}'";
                var myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    originId= Convert.ToInt32((myReader.GetString(0)));
                    destId = Convert.ToInt32((myReader.GetString(1)));
                }
                if (!myReader.IsClosed)
                {
                    myReader.Close();
                }

                cmd.CommandText = $"SELECT station_name from Stations WHERE station_id = {originId}";
                 myReader = cmd.ExecuteReader();
                 while (myReader.Read())
                 {
                     originName = myReader.GetString(0);
                 }
                 if (!myReader.IsClosed)
                 {
                     myReader.Close();
                 }
                 cmd.CommandText = $"SELECT station_name from Stations WHERE station_id = {destId}";
                 myReader = cmd.ExecuteReader();
                 while (myReader.Read())
                 {
                     destName = myReader.GetString(0);
                 }
                 if (!myReader.IsClosed)
                 {
                     myReader.Close();
                 }
                 
                 connection.Close();
                 return Tuple.Create<string, string>(originName,destName);


            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }

        connection.Close();

        return null;
    }
    public int GetRoutePriceFromId(int routeId)
    {
        int routePrice = 0;

        try
        {
            using (connection)
            {

                connection.Open();
                print("MySQL - Opened Connection To GET ROUTE PRICE");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $" SELECT route_price from Routes WHERE route_id = '{routeId}'";
                var myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    routePrice = Convert.ToInt32((myReader.GetString(0)));
                    connection.Close();
                    return routePrice;
                }
                if (!myReader.IsClosed)
                {
                    myReader.Close();
                }
                connection.Close();
                return routePrice;

            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }

        connection.Close();

        return 0;
    }
    public int GetRoutePrice(int originId, int destinationId)
    {
        int routePrice = 0;

        try
        {
            using (connection)
            {

                connection.Open();
                print("MySQL - Opened Connection To GET ROUTE PRICE");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $" SELECT route_price from Routes WHERE station_origin_id='{originId}' AND station_destination_id = '{destinationId}'";
                var myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    routePrice = Convert.ToInt32((myReader.GetString(0)));
                    
                    
                }
                if (!myReader.IsClosed)
                {
                    myReader.Close();
                }
                connection.Close();
                return routePrice;

            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }

        connection.Close();

        return 0;
    }

    public void SetRoutePrice(int routeId, int newPrice)
    {
        try
        {
            using (connection)
            {

                connection.Open();
                print("MySQL - Opened Connection To SET ROUTE PRICE");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $"UPDATE Routes SET route_price = '{newPrice}' WHERE route_id = {routeId}";
                var myReader = cmd.ExecuteNonQuery();
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }

        connection.Close();

        
    }
    
    public int GetRouteIdFromStations(int originId, int destinationId)
    {

        try
        {
            using (connection)
            {

                connection.Open();
                print("MySQL - Opened Connection To GET ROUTE ID");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $" SELECT route_id from Routes WHERE station_origin_id='{originId}' AND station_destination_id = '{destinationId}'";
                var myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    int dbId = Convert.ToInt32((myReader.GetString(0)));
                    connection.Close();
                    return dbId;
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

    public string GetPayTypeName(int payId)
    {
        try
        {
            using (connection)
            {

                connection.Open();
                print("MySQL - Opened Connection To GET Payment Name");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $" SELECT payment_type from Payments WHERE payment_id = {payId}";
                var myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    string payTypeName = myReader.GetString(0);
                    connection.Close();
                    return payTypeName;
                }

            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }

        connection.Close();

        return null;
    }
    public Dictionary<string, int> GetPayTypeDict()
    {


        Dictionary<string, int> payTypeDict = new Dictionary<string, int>();
        try
        {
            using (connection)
            {

                connection.Open();
                print("MySQL - Opened Connection To GET Payment Type");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $" SELECT * from Payments";
                var myReader = cmd.ExecuteReader();
                while (myReader.HasRows)
                {

                    while (myReader.Read())
                    {
                        payTypeDict.Add(myReader.GetString(1), myReader.GetInt32(0));
                    }

                    myReader.NextResult();

                }

                if (!myReader.IsClosed)
                {
                    myReader.Close();
                }

                return payTypeDict;

            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }

        connection.Close();

        return null;
    }
    

    public void AddTickets(int ticketStatus, int userId, int paymentId,int routeId,string iDate,string iDateTime)
    {
        
        try
        {
            using (connection)
            {
                connection.Open();
                using var cmd = new MySqlCommand();
                cmd.Connection = connection;
                print("-----------------------------");
                print("Adding new ticket with the following credentials:");
                print("Ticket status: "+ ticketStatus);
                print("Ticket user Id: " + userId);
                print("Ticket paymentID: "+ paymentId);
                print("Ticket routeId: "+ routeId);
                print("-----------------------------");
                cmd.CommandText = $"INSERT  INTO Tickets (status_id,user_id,payment_id,route_id,transaction_date,transaction_datetime) VALUES('{ticketStatus}','{userId}','{paymentId}','{routeId}','{iDate}','{iDateTime}')";
                cmd.ExecuteNonQuery();
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
    }
    
    



    
    
    

    public List<Ticket> GetUserOngoingTickets(int userId)
    {
        List<Ticket> ongoingTicketList = new List<Ticket>();
        
        try
        {
            using (connection)
            {
                
                connection.Open();
                print("MySQL - Opened Connection To GET ONGOING TICKETS");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $" SELECT * from Tickets WHERE user_id = {userId} AND (status_id = 1 OR status_id=2) ";
                                  
                var myReader = cmd.ExecuteReader();
                
                while (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        print("Found a ticket");
                        int  ticketId = Convert.ToInt32(myReader.GetString(0));
                        
                        int routeId = Convert.ToInt32(myReader.GetString(4));
                        int statusId = Convert.ToInt32(myReader.GetString(1));
                        int paymentId = Convert.ToInt32(myReader.GetString(3));
                        string dateDb = myReader.GetString(5);
                        string timeDb = myReader.GetString(6);
                        print("i:"+ticketId+"r:"+routeId+"s:"+statusId+"p:"+paymentId);
                        Ticket ongoingTicket = new Ticket(ticketId,userId,routeId,paymentId,statusId,dateDb,timeDb);
                        ongoingTicketList.Add(ongoingTicket);
                        print("found ticket with status id "+statusId);
                    }
                    myReader.NextResult();
                }
                if (!myReader.IsClosed)
                {
                    myReader.Close();
                }
                connection.Close();
                return ongoingTicketList;

            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();

        return new List<Ticket>();
    }
    public List<Ticket> GetUserHistoryTickets(int userId)
    {
        List<Ticket> ongoingTicketList = new List<Ticket>();
        try
        {
            using (connection)
            {
                
                connection.Open();
                print("MySQL - Opened Connection To GET HISTORY TICKETS");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText =
                    $" SELECT * from Tickets WHERE user_id = {userId} AND (status_id = 3 OR status_id=4) ";
                                  
                var myReader = cmd.ExecuteReader();
                
                while (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        print("Found a ticket");
                        int  ticketId = Convert.ToInt32(myReader.GetString(0));
                        
                        int routeId = Convert.ToInt32(myReader.GetString(4));
                        int statusId = Convert.ToInt32(myReader.GetString(1));
                        int paymentId = Convert.ToInt32(myReader.GetString(3));
                        string dateDb = myReader.GetString(5);
                        string timeDb = myReader.GetString(6);
                        print("i:"+ticketId+"r:"+routeId+"s:"+statusId+"p:"+paymentId);
                        Ticket ongoingTicket = new Ticket(ticketId,userId,routeId,paymentId,statusId,dateDb,timeDb);
                        ongoingTicketList.Add(ongoingTicket);
                        print("found ticket with status id "+statusId);
                    }
                    myReader.NextResult();
                }
                if (!myReader.IsClosed)
                {
                    myReader.Close();
                }
                connection.Close();
                return ongoingTicketList;

            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();

        return new List<Ticket>();
    }

    public void CheckTicketExpiry()
    {
        string currentDate = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy");
        try
        {
            using (connection)
            {
                connection.Open();
                print("MySQL - Checking and Updating Ticket Expiry...");
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"UPDATE Tickets SET status_id = 4 WHERE NOT transaction_date = '{currentDate}'";
                cmd.ExecuteNonQuery();
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();
    }
    public void ChangeTicketStatus(int ticketId, int newStatus)
    {

        try
        {
            using (connection)
            {
                connection.Open();
                print("MySQL - Ticket " + ticketId + " Status Modified to " + newStatus);
                using var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"UPDATE Tickets SET status_id = '{newStatus}' WHERE ticket_id = '{ticketId}'";
                cmd.ExecuteNonQuery();
            }
        }
        catch (MySqlException exception)
        {
            print(exception.Message);
        }
        connection.Close();
    }

    
    
    

    

   
}
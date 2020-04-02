using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public class ManageGuest : IManage<Guest>
    {
        //Lokal database
        //private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Azure
        private const string ConnectionString =
            @"Data Source=arsenserver.database.windows.net;Initial Catalog=ArsensDatabase;User ID=ArsenAdmin;Password=SecretPassword1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL = "Select * From Guest";
        private const string GET_ONE = "Select * From Guest Where Guest_No = @GuestId";
        private const string INSERT = "Insert into Guest values (@GuestNr, @Name, @Address)";
        private const string UPDATE = "Update Guest Set Guest_No = @GuestNr, Name = @Name, Address = @Address Where Guest_No = @GuestId";
        private const string DELETE = "Delete From Guest Where Guest_No = @GuestId";

        public IEnumerable<Guest> Get()
        {
            List<Guest> liste = new List<Guest>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GET_ALL, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Guest guest = readGuest(reader);
                liste.Add(guest);
            }
            conn.Close();
            return liste;
        }

        private Guest readGuest(SqlDataReader reader)
        {
            Guest guest = new Guest();
            guest.GuestNr = reader.GetInt32(0);
            guest.Name = reader.GetString(1);
            guest.Address = reader.GetString(2);
            return guest;
        }
        public Guest Get(int id)
        {
            Guest guest = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GET_ONE, conn);
            cmd.Parameters.AddWithValue("@GuestId", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                guest = readGuest(reader);
            }
            conn.Close();
            return guest;
        }

        
        public bool Post(Guest guest)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(INSERT, conn);
            cmd.Parameters.AddWithValue("@GuestNr", guest.GuestNr);
            cmd.Parameters.AddWithValue("@Name", guest.Name);
            cmd.Parameters.AddWithValue("@Address", guest.Address);
            int noOfRowsAffected = cmd.ExecuteNonQuery();

            ok = noOfRowsAffected == 1 ? true : false;
            
            conn.Close();
            return ok;
        }

        
        public bool Put(int id, Guest guest)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(UPDATE, conn);
            cmd.Parameters.AddWithValue("@GuestNr", guest.GuestNr);
            cmd.Parameters.AddWithValue("@Name", guest.Name);
            cmd.Parameters.AddWithValue("@Address", guest.Address);
            cmd.Parameters.AddWithValue("@GuestId", id);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;
            conn.Close();
            return ok;
        }

        // DELETE: api/Guest/5
        public bool Delete(int id)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(DELETE, conn);
            cmd.Parameters.AddWithValue("@GuestId", id);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;
            conn.Close();
            return ok;
        }
    }
}
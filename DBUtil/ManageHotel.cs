using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public class ManageHotel : IManage<Hotel>
    {
        //Lokal database
        //private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Azure
        private const string ConnectionString = @"Data Source=arsenserver.database.windows.net;Initial Catalog = ArsensDatabase; User ID = ArsenAdmin; Password=SecretPassword1234;Connect Timeout = 30; Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL = "Select * From Hotel";
        private const string GET_ONE = "Select * From Hotel Where Hotel_No = @HotelId";
        private const string INSERT = "Insert into Hotel values (@HotelNr, @Name, @Address)";
        private const string UPDATE = "Update Hotel Set Hotel_No = @HotelNr, Name = @Name, Address = @Address Where Hotel_No = @HotelId";
        private const string DELETE = "Delete From Hotel Where Hotel_No = @HotelId";

        public IEnumerable<Hotel> Get()
        {
            List<Hotel> hotelList = new List<Hotel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GET_ALL, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Hotel hotel = readHotel(reader);
                hotelList.Add(hotel);
            }
            conn.Close();
            return hotelList;
        }

        private Hotel readHotel(SqlDataReader reader)
        {
            Hotel hotel = new Hotel();
            hotel.HotelNr = reader.GetInt32(0);
            hotel.Name = reader.GetString(1);
            hotel.Address = reader.GetString(2);
            return hotel;
        }
        
        public Hotel Get(int id)
        {
            Hotel hotel = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GET_ONE, conn);
            cmd.Parameters.AddWithValue("@HotelId", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                hotel = readHotel(reader);
            }
            conn.Close();
            return hotel;
        }

        
        public bool Post(Hotel hotel)
        {
            bool ok = false; 

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(INSERT, conn);
            cmd.Parameters.AddWithValue("@HotelNr", hotel.HotelNr);
            cmd.Parameters.AddWithValue("@Name", hotel.Name);
            cmd.Parameters.AddWithValue("@Address", hotel.Address);
            int noOfRowsAffected = cmd.ExecuteNonQuery();

            ok = noOfRowsAffected == 1 ? true : false;

            conn.Close();

            return ok;

        }

        public bool Put(int id, Hotel hotel)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(UPDATE, conn);
            cmd.Parameters.AddWithValue("@HotelNr", hotel.HotelNr);
            cmd.Parameters.AddWithValue("@Name", hotel.Name);
            cmd.Parameters.AddWithValue("@Address", hotel.Address);
            cmd.Parameters.AddWithValue("@HotelId", id);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;
            conn.Close();
            return ok;
        }

        public bool Delete(int id)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(DELETE, conn);
            cmd.Parameters.AddWithValue("@HotelId", id);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;
            conn.Close();
            return ok;
        }

    }
}
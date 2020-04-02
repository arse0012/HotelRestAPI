using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public class ManageRoom
    {
        //Lokal database
        //private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = HotelDbtest2; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Azure
        private const string ConnectionString =
            @"Data Source=arsenserver.database.windows.net;Initial Catalog=ArsensDatabase;User ID=ArsenAdmin;Password=SecretPassword1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL = "Select * From Room";
        private const string GET_ONE = "Select * From Room Where Room_No = @RoomId and Hotel_No = @HotelId";
        private const string INSERT = "Insert into Room values (@RoomNr, @HotelNo, @RoomType, @Price)";
        private const string UPDATE = "Update Room Set Room_No = @RoomNr, Hotel_No = @HotelNo, Types = @RoomType, Price = @Price Where Room_No = @RoomId and Hotel_No = @HotelId";
        private const string DELETE = "Delete From Room Where Room_No = @RoomId and Hotel_No = @HotelId";
        public IEnumerable<Room> Get()
        {
            List<Room> roomList = new List<Room>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GET_ALL, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Room room = readRoom(reader);
                roomList.Add(room);
            }
            conn.Close();
            return roomList;
        }

        private Room readRoom(SqlDataReader reader)
        {
            Room room = new Room();
            room.RoomNr = reader.GetInt32(0);
            room.HotelNo = reader.GetInt32(1);
            room.RoomType = reader.GetString(2);
            room.Price = reader.GetDouble(3);
            return room;
        }

        public Room Get(int id, int hotelNo)
        {
            Room room = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GET_ONE, conn);
            cmd.Parameters.AddWithValue("@RoomId", id);
            cmd.Parameters.AddWithValue("@HotelId", hotelNo);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                room = readRoom(reader);
            }
            conn.Close();
            return room;
        }

        public bool Post(int id, int hotelNo, Room room)
        {
            bool ok = false;
            SqlConnection conn=new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd=new SqlCommand(INSERT, conn);
            cmd.Parameters.AddWithValue("@RoomNr", room.RoomNr);
            cmd.Parameters.AddWithValue("@HotelNo", room.HotelNo);
            cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
            cmd.Parameters.AddWithValue("@Price", room.Price);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;
            conn.Close();
            return ok;
        }

        public bool Put(int id, int hotelNo, Room room)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(UPDATE, conn);
            cmd.Parameters.AddWithValue("@RoomNr", room.RoomNr);
            cmd.Parameters.AddWithValue("@HotelNo", room.HotelNo);
            cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
            cmd.Parameters.AddWithValue("@Price", room.Price);
            cmd.Parameters.AddWithValue("@HotelId", id);
            cmd.Parameters.AddWithValue("@RoomId", hotelNo);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;
            conn.Close();
            return ok;
        }

        public bool Delete(int id, int hotelNo)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(DELETE, conn);
            cmd.Parameters.AddWithValue("@RoomId", id);
            cmd.Parameters.AddWithValue("@HotelId", hotelNo);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;
            conn.Close();
            return ok;
        }
    }
}
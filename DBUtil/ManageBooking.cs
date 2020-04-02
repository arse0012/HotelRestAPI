using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public class ManageBooking
    {
        //Lokal database
        //private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Azure
        private const string ConnectionString =
            @"Data Source=arsenserver.database.windows.net;Initial Catalog=ArsensDatabase;User ID=ArsenAdmin;Password=SecretPassword1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL = "Select * From Booking";
        private const string GET_ONE = "Select * From Booking Where Booking_Id = @BookingId";
        private const string INSERT = "Insert into Booking values ()";
        private const string UPDATE = "";
        private const string DELETE = "";
        public IEnumerable<Booking> Get()
        {
            List<Booking> BookingList = new List<Booking>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GET_ALL, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Booking booking = readBooking(reader);
                BookingList.Add(booking);
            }
            conn.Close();
            return BookingList;
        }
        private Booking readBooking(SqlDataReader reader)
        {
            Booking booking = new Booking();
            booking.HotelNr = reader.GetInt32(0);
            
            return booking;
        }

        public Booking Get(int id)
        {
            Booking booking = null;

            return booking;
        }

        public bool Post(Booking booking)
        {
            bool ok = false;

            return ok;
        }

        public bool Put(int id, Booking booking)
        {
            bool ok = false;

            return ok;
        }

        public bool Delete(int id)
        {
            bool ok = false;

            return ok;
        }
    }
}
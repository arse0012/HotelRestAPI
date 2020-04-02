using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ModelLibrary;

namespace HotelRestAPI.Controllers
{
    public class BookingsController : ApiController
    {
        // GET: api/Bookings
        public IEnumerable<Booking> Get()
        {
            return Get();
        }

        // GET: api/Bookings/5
        public Booking Get(int id)
        {
            return Get(id);
        }

        // POST: api/Bookings
        public bool Post([FromBody]string value)
        {
            return Post(value);
        }

        // PUT: api/Bookings/5
        public bool Put(int id, [FromBody]string value)
        {
            return Put(id, value);
        }

        // DELETE: api/Bookings/5
        public bool Delete(int id)
        {
            return Delete(id);
        }
    }
}

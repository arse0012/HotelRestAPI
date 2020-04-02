using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelRestAPI.DBUtil;
using ModelLibrary;

namespace HotelRestAPI.Controllers
{
    public class RoomController : ApiController
    {
        private static ManageRoom manager = new ManageRoom();
        // GET: api/Room
        public IEnumerable<Room> Get()
        {
            return manager.Get();
        }

        // GET: api/Room/5
        public Room Get(int id)
        {
           throw new NotImplementedException();
        }

        // Get: api/room/1/2
        [HttpGet]
        [Route("api/room/{id}/{hotelNo}")]
        public Room GetOneRoom(int id, int hotelNo)
        {
            return manager.Get(id, hotelNo);
        }


        // POST: api/Room
        public bool Post([FromBody]Room value)
        {
            throw new NotImplementedException();
        }
        
        //  Post: api/room/1/2
        [HttpPost]
        [Route("api/room/{id}/{hotelNo}")]
        public bool PostOneRoom(int id, int hotelNo,[FromBody]Room value)
        {
            return manager.Post(id, hotelNo, value);
        }

        // PUT: api/Room/5
        public bool Put(int id, [FromBody]Room value)
        {
            throw new NotImplementedException();
        }

        //  Put: api/room/1/2
        [HttpPut]
        [Route("api/room/{id}/{hotelNo}")]
        public bool PutOneRoom(int id, int hotelNo, [FromBody]Room value)
        {
            return manager.Put(id, hotelNo, value);
        }

        // DELETE: api/Room/5
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        //  Delete: api/room/1/2
        [HttpDelete]
        [Route("api/room/{id}/{hotelNo}")]
        public bool DeleteOneRoom(int id, int hotelNo)
        {
            return manager.Delete(id, hotelNo);
        }
    }
}

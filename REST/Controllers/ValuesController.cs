using Microsoft.AspNetCore.Mvc;
using REST.Model;
using REST.Manager;
using System.Net.Http;
using System.Net;
using System.Collections;


namespace REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ArrayList Get()
        {
            DataBaseManager manager = new DataBaseManager();
            return manager.getAllPersons(); ;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<PersonClass> Get(long id)
        {
            DataBaseManager manager = new DataBaseManager();
            PersonClass person = manager.getPersonById(id);
     
            return person;
        }

        // POST api/values
        [HttpPost]
        public HttpResponseMessage Post([FromBody] PersonClass value)
        {
            DataBaseManager manager = new DataBaseManager();
            long id;
            id = manager.savePerson(value);
            value.ID = (int)id;
            HttpRequestMessage request = new HttpRequestMessage();
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Created);
            return response;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public HttpResponseMessage Put(long id, [FromBody] PersonClass value)
        {
            DataBaseManager manager = new DataBaseManager();
            bool recordExist = false;
            recordExist = manager.updatePerson(id, value);
            HttpRequestMessage request = new HttpRequestMessage();
            HttpResponseMessage response;
            if (recordExist)
            {
                response = request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(long id)
        {
            DataBaseManager manager = new DataBaseManager();
            bool recordExist = false;
            recordExist = manager.deletePerson(id);
            HttpRequestMessage request = new HttpRequestMessage();
            HttpResponseMessage response;
            if (recordExist)
            {
                response = request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}

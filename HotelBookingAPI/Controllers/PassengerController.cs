using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;
using MvcContrib.ActionResults;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly ApiContext _context;

        public PassengerController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создать пассажира.
        /// </summary>
        /// <param name="passenger">Пассажир</param>
        /// <returns></returns>
        [HttpPost]
        public XmlResult Create(Passenger passenger)
        {

            var passengerInDB = _context.Passengers.Find(passenger.Id);

            if (passengerInDB == null)
                return new XmlResult(NotFound());

            passengerInDB = passenger;

            _context.SaveChanges();

            return new XmlResult(Ok(passengerInDB));

        }

        // Get
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Passengers.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Passengers.Find(id);

            if (result == null)
                return new JsonResult(NotFound());

            _context.Passengers.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        // Get all
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Passengers.ToList();

            return new JsonResult(Ok(result));
        }
       
    }
}

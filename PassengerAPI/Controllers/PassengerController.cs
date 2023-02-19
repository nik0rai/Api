using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;
using HotelBookingAPI.Utils;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly ApiContext _context;
        Generator generator = new UsingPrimitiveRandom();

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
        [Produces("application/xml")]
        public IActionResult Create(Passenger passenger)
        {
            var result = SimpleValidator.CheckDuplicate(this, _context, ref passenger);

            if (result.GetType() == Ok().GetType())
            {
                _context.Passengers.Add(passenger);
                _context.SaveChanges();
                return Created($"{nameof(passenger.guid)}", passenger.guid);
            }

            return result;
        }

        /// <summary>
        /// Создать случайного пассажира.
        /// </summary>
        /// <param name="passenger">Пассажир</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/xml")]
        public IActionResult CreateRandom()
        {
            var passenger = generator.Generate(); //создаёт новые объекты через new
            passenger.State = State.HaveIdeaState;

            var result = SimpleValidator.CheckDuplicate(this, _context, ref passenger);

            if (result.GetType() == Ok().GetType())
            {
                _context.Passengers.Add(passenger);
                _context.SaveChanges();
                return Created($"{nameof(passenger.guid)}", passenger.guid);
            }

            return result;
        }

        /// <summary>
        /// Создать случайного пассажира с указанным состоянием.
        /// </summary>
        /// <param name="passenger">Пассажир</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/xml")]
        public IActionResult CreatePassengerWithState(State state)
        {
            var passenger = generator.Generate(); //создаёт новые объекты через new
            passenger.State = state;

            var result = SimpleValidator.CheckDuplicate(this, _context, ref passenger);

            if (result.GetType() == Ok().GetType())
            {
                _context.Passengers.Add(passenger);
                _context.SaveChanges();
                return Created($"{nameof(passenger.guid)}", passenger.guid);
            }

            return result;
        }

        /// <summary>
        /// Получить пассажира по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        [Produces("application/xml")]
        public IActionResult Get([FromRoute] Guid id)
        {
            var result = _context.Passengers.Find(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Удалить пассажира по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [Produces("application/xml")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = _context.Passengers.Find(id);

            if (result == null)
                return NotFound();

            _context.Passengers.Remove(result);
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Получить всех пассажиров.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/xml")]
        public IActionResult GetAll()
        {
            var result = _context.Passengers.ToList();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Установить состояние пассажиру.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}/{state}")]
        [Produces("application/xml")]
        public IActionResult SetState([FromRoute] Guid id, [FromRoute] State state)
        {
            var result = _context.Passengers.Find(id);

            if (result == null)
                return NotFound();

            result.State = state;
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Установить багаж пассажиру.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}/{bagage}")]
        [Produces("application/xml")]
        public IActionResult SetBagage([FromRoute] Guid id, [FromRoute] bool bagage)
        {
            var result = _context.Passengers.Find(id);

            if (result == null)
                return NotFound();

            result.Bagage = bagage;
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Установить запрос пассажиру.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        [Produces("application/xml")]
        public IActionResult SetRequest([FromRoute] Guid id, Prompt request)
        {
            var result = _context.Passengers.Find(id);

            if (result == null)
                return NotFound();

            result.Request = request;
            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Установить состояние пассажирам.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/xml")]
        public IActionResult SetStates(State state, IEnumerable<Guid> guids)
        {
            List<Guid> notFoundPassengers = new();
            ActionResult result = Ok();

            var flag = false;
            foreach (var guid in guids)
            {
                var find = _context.Passengers.Find(guid);

                if (find != null)
                {
                    find.State = state;
                }
                else
                {
                    notFoundPassengers.Add(guid);
                    flag = true;
                }
            }

            if (flag)
            {
                result = NotFound(notFoundPassengers);
            }

            _context.SaveChanges();

            return result;
        }
    }
}
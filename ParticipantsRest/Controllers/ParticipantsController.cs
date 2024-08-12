using Microsoft.AspNetCore.Mvc;
using ParticipantsLib;



namespace ParticipantsRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private ParticipantsRepository ParticipantsRepository;

        public ParticipantsController(ParticipantsRepository participantsRepository)
        {
            ParticipantsRepository = participantsRepository;
        }
        // GET: api/<ParticipantsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<List<Participants>> Get()
        {
             List<Participants> participantsList = ParticipantsRepository.GetAll();
            if (participantsList.Any())
            {
               
                return Ok(participantsList);
            }
            else
            {
                return NoContent();
            }

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{id}")]
        public ActionResult<Participants> Get(int id)
        {
           Participants p = ParticipantsRepository.GetById(id);
            if (p == null)
            {
                return NotFound("No such class, id: \"" + id);
            }
            else
            {
                return Ok(p);
            }
            
        }

        // POST api/<ActorsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Participants> Post([FromBody] Participants participants)
        {
            try
            {
                Participants AddParticipant = ParticipantsRepository.Add(participants);
                return Created("/" + AddParticipant.Id, AddParticipant);
            }
            catch (Exception ex) when (ex is ArgumentException ||
                               ex is ArgumentOutOfRangeException)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public ActionResult<Participants> Delete(int id)
        {
            Participants p1 = ParticipantsRepository.Delete(id);
            if (p1 == null)
            {
               return NotFound("No such class, id: \"" + id);
            }
            else
            {
                return Ok(p1);
            }

            
        }
    }
}

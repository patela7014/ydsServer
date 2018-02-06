using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Core.Models;
using DAL.Persistence;
using AutoMapper;
using DAL.Repository;
using DAL.Resources;
using DAL.Core;

namespace ApiServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ApiContext _context;
        private readonly IRepository<Event> repository;
        private readonly IRepository<EventAttendance> attendanceRepository;
        private readonly ISabhaRepository sabhaRepository;

        public EventsController(IUnitOfWork unitOfWork, IMapper mapper, ApiContext context, IRepository<Event> repository, IRepository<EventAttendance> attendanceRepository, ISabhaRepository sabhaRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _context = context;
            this.repository = repository;
            this.attendanceRepository = attendanceRepository;
            this.sabhaRepository = sabhaRepository;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<IEnumerable<EventListResource>> GetEvent()
        {
            var events = this.repository.GetAll();

            var allEvents = await events.ToListAsync();

            var result = mapper.Map<IEnumerable<Event>, IEnumerable<EventListResource>>(allEvents);

            return result;
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // GET: api/Events/5
        [HttpGet("{id}/sabha/{sabhaId}/attendance")]
        public async Task<IActionResult> GetEventAttendance([FromRoute] string id, string sabhaId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var eventData = this.repository.FindBy(e => e.Id == id);

            var eventInfo = await eventData.SingleOrDefaultAsync();

            var eventResult = mapper.Map<Event, EventListResource>(eventInfo);

            var sabhaUsers = sabhaRepository.GetSabhaUsers(s => s.Id == sabhaId);

            var allSabhaUsers = await sabhaUsers.SingleOrDefaultAsync();

            var result = mapper.Map<Sabha, SabhaListResource>(allSabhaUsers);

            eventResult.AllUsers = result.Users;

            return Ok(eventResult);
        }

        // POST: api/Events/5
        [HttpPost("{id}/attendance")]
        public async Task<IActionResult> SaveEventAttendance([FromBody] AddAttendanceResource attendanceResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventData = repository.FindBy(c => c.Id == attendanceResource.EventId);
            var eventAdded = await eventData.SingleOrDefaultAsync();

            var eventSave = mapper.Map<AddAttendanceResource, Event>(attendanceResource, eventAdded);
            //repository.Add(eventSave);

            await unitOfWork.CompleteAsync();
            var result = mapper.Map<Event, EventAddResource>(eventAdded);
            return Ok(result);
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] string id, [FromBody] Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] EventAddResource @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventSave = mapper.Map<EventAddResource, Event>(@event);
            eventSave.Id = repository.GenerateID();
            eventSave.Created = DateTime.Now;
            repository.Add(eventSave);

            await unitOfWork.CompleteAsync();
            var eventData = repository.FindBy(c => c.Id == eventSave.Id);
            var eventAdded = await eventData.SingleOrDefaultAsync();
            var result = mapper.Map<Event, EventAddResource>(eventAdded);
            return Ok(result);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            return Ok(@event);
        }

        private bool EventExists(string id)
        {
            return _context.Event.Any(e => e.Id == id);
        }

        
    }
}
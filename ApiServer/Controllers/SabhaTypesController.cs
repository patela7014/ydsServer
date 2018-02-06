using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Core.Models;
using DAL.Persistence;
using DAL.Core;
using AutoMapper;
using DAL.Repository;
using DAL.Resources;

namespace ApiServer.Controllers
{
    [Produces("application/json")]
    [Route("api/SabhaTypes")]
    public class SabhaTypesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ApiContext _context;
        private readonly IRepository<SabhaType> repository;

        public SabhaTypesController(IUnitOfWork unitOfWork, IMapper mapper, ApiContext context, IRepository<SabhaType> repository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _context = context;
            this.repository = repository;
        }

        // GET: api/SabhaTypes
        [HttpGet]
        public async Task<IEnumerable<SabhaTypeResource>> GetSabhaType()
        {
            var sabhaTypes = this.repository.GetAll();

            var allSabhaTypes = await sabhaTypes.ToListAsync();

            var result = mapper.Map<IEnumerable<SabhaType>, IEnumerable<SabhaTypeResource>>(allSabhaTypes);

            return result;
        }

        // GET: api/SabhaTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSabhaType([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sabhaType = await _context.SabhaType.SingleOrDefaultAsync(m => m.Id == id);

            if (sabhaType == null)
            {
                return NotFound();
            }

            return Ok(sabhaType);
        }

        // PUT: api/SabhaTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSabhaType([FromRoute] string id, [FromBody] SabhaType sabhaType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sabhaType.Id)
            {
                return BadRequest();
            }

            _context.Entry(sabhaType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SabhaTypeExists(id))
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

        // POST: api/SabhaTypes
        [HttpPost]
        public async Task<IActionResult> PostSabhaType([FromBody] SabhaTypeResource sabhaType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sabhaTypeSave = mapper.Map<SabhaTypeResource, SabhaType>(sabhaType);
            sabhaTypeSave.Id = repository.GenerateID();
            sabhaTypeSave.IsActive = true;

            repository.Add(sabhaTypeSave);

            await unitOfWork.CompleteAsync();
            var sabhaTypeData = repository.FindBy(c => c.Id == sabhaTypeSave.Id);
            var sabhaTypeAdded = await sabhaTypeData.SingleOrDefaultAsync();
            var result = mapper.Map<SabhaType, SabhaTypeResource>(sabhaTypeAdded);
            return Ok(result);
        }

        // DELETE: api/SabhaTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSabhaType([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sabhaType = await _context.SabhaType.SingleOrDefaultAsync(m => m.Id == id);
            if (sabhaType == null)
            {
                return NotFound();
            }

            _context.SabhaType.Remove(sabhaType);
            await _context.SaveChangesAsync();

            return Ok(sabhaType);
        }

        private bool SabhaTypeExists(string id)
        {
            return _context.SabhaType.Any(e => e.Id == id);
        }
    }
}
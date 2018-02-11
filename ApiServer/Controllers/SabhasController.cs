﻿using System;
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
    [Route("api/Sabhas")]
    public class SabhasController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ApiContext _context;
        private readonly IRepository<Sabha> repository;
        private readonly IRepository<User> userRepository;

        public SabhasController(IUnitOfWork unitOfWork, IMapper mapper, ApiContext context, IRepository<Sabha> repository, IRepository<User> userRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _context = context;
            this.repository = repository;
            this.userRepository = userRepository;
        }

        // GET: api/Sabhas
        [HttpGet]
        public async Task<IEnumerable<SabhaListResource>> GetSabha()
        {
            var sabhas = this.repository.GetAll();

            var allSabhas = await sabhas.ToListAsync();

            var result = mapper.Map<IEnumerable<Sabha>, IEnumerable<SabhaListResource>>(allSabhas);

            return result;
        }

        // GET: api/Sabhas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSabha([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sabhaData = repository.FindBy(c => c.Id == id);
            var sabha = await sabhaData.SingleOrDefaultAsync();

            var result = mapper.Map<Sabha, SabhaListResource>(sabha);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Sabhas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSabha([FromRoute] string id, [FromBody] Sabha sabha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sabha.Id)
            {
                return BadRequest();
            }

            _context.Entry(sabha).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SabhaExists(id))
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

        // POST: api/Sabhas
        [HttpPost]
        public async Task<IActionResult> PostSabha([FromBody] SabhaAddResource sabha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sabhaSave = mapper.Map<SabhaAddResource, Sabha>(sabha);
            sabhaSave.Id = repository.GenerateID();
            sabhaSave.IsActive = true;
            repository.Add(sabhaSave);

            await unitOfWork.CompleteAsync();
            var sabhaData = repository.FindBy(c => c.Id == sabhaSave.Id);
            var sabhaAdded = await sabhaData.SingleOrDefaultAsync();
            var result = mapper.Map<Sabha, SabhaAddResource>(sabhaAdded);
            return Ok(result);
        }

        // POST: api/Sabhas
        [HttpPost("{id}/users")]
        public async Task<IActionResult> AddUsersToSabha([FromBody] AddSabhaUserResource sabhaUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = userRepository.GetAll();

            var allUsers = await users.ToListAsync();

            var sabhaData = repository.FindBy(c => c.Id == sabhaUser.SabhaId);
            var sabhaAdded = await sabhaData.SingleOrDefaultAsync();
            List<string> usersList = new List<string>();
            
            foreach (var user in allUsers)
            {
                usersList.Add(user.Id);
            }
            sabhaUser.IncludedUsers = usersList;
            var sabhaUsersSave = mapper.Map<AddSabhaUserResource, Sabha>(sabhaUser, sabhaAdded);
        
            await unitOfWork.CompleteAsync();
            var result = mapper.Map<Sabha, SabhaListResource>(sabhaUsersSave);
            return Ok(result);
        }

        // DELETE: api/Sabhas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSabha([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sabha = await _context.Sabha.SingleOrDefaultAsync(m => m.Id == id);
            if (sabha == null)
            {
                return NotFound();
            }

            _context.Sabha.Remove(sabha);
            await _context.SaveChangesAsync();

            return Ok(sabha);
        }

        private bool SabhaExists(string id)
        {
            return _context.Sabha.Any(e => e.Id == id);
        }

        //private async Task<IEnumerable<SabhaListResource>> SabhaUsers()
        //{
        //    var sabhas = this.repository.GetAll();

        //    var allSabhas = await sabhas.ToListAsync();

        //    var result = mapper.Map<IEnumerable<Sabha>, IEnumerable<SabhaListResource>>(allSabhas);

        //    return _context.Event.Any(e => e.Id == id);
        //}
    }
}
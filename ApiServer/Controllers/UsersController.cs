﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Core.Models;
using DAL.Persistence;
using DAL.Repository;
using AutoMapper;
using DAL.Resources;
using DAL.Core;

namespace ApiServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ApiContext _context;
        private readonly IRepository<User> repository;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, ApiContext context, IRepository<User> repository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _context = context;
            this.repository = repository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<UsersListResource>> GetUser()
        {
            var users = this.repository.GetAll();
            users = users.OrderBy(u => u.Created);
            var allUsers = await users.ToListAsync();

            var result = mapper.Map<IEnumerable<User>, IEnumerable<UsersListResource>>(allUsers);

            return result;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userData = repository.FindBy(c => c.Id == id);
            var user = await userData.SingleOrDefaultAsync();

            var result = mapper.Map<User, UsersListResource>(user);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserRegistration[] modelArr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (UserRegistration model in modelArr)
            {
                var userIdentity = mapper.Map<UserRegistration, User>(model);
                userIdentity.Id = repository.GenerateID();
                repository.Add(userIdentity);
                await unitOfWork.CompleteAsync();
            }
            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
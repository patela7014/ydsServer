using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Core;
using DAL.Core.Models;
using DAL.Helpers;
using DAL.Repository;
using DAL.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IRepository<User> repository;
        private readonly IRepository<Address> addressRepository;
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private static readonly Random getrandom = new Random();
        public AuthController(IUnitOfWork unitOfWork, IMapper mapper, IRepository<User> repository, IRepository<Address> addressRepository, UserManager<User> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.repository = repository;
            this.addressRepository = addressRepository;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialResource credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id.ToString()));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        // POST api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserRegistration[] modelArr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var userIdentity = mapper.Map<UserRegistration, User>(model);
            //repository.Add(userIdentity);
            //var result = await _userManager.CreateAsync(userIdentity, model.Password);
            //if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            foreach (UserRegistration model in modelArr)
            {
                model.Password = "dasnadas";
                var userIdentity = mapper.Map<UserRegistration, User>(model);
                if (userIdentity.Email == null || userIdentity.Email == "")
                {
                    userIdentity.Email = $"testing{getrandom.Next(1, 100000)}@gmail.com";
                }
                userIdentity.UserName = userIdentity.Email;
                var result = await _userManager.CreateAsync(userIdentity, model.Password);

                if (userIdentity.Id != null)
                {
                    var newAddressResource = new AddressResource {Id = addressRepository.GenerateID(), City = model.City, Country = model.Country, IsActive = true, State = model.State, Street = $"{model.Street} {model.Apt}" };
                    var newAddress = mapper.Map<AddressResource, Address>(newAddressResource);
                    newAddress.UserId = userIdentity.Id;
                    addressRepository.Add(newAddress);
                }
                //if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
                await unitOfWork.CompleteAsync();
            }
            return NoContent();

        }

        private bool UserExists(string name)
        {
            var userToVerify = _userManager.FindByNameAsync(name);

            if (userToVerify == null)
            {
                return false;
            }
            return true;
        }
    }

    

}
using AutoMapper;
using E_commerce.Core.Entities.identity;
using E_commerce.Core.IServices;
using E_commerce.Dtos;
using E_commerce.Errors;
using E_commerce.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerce.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        [HttpPost("login")]
        public async Task<ActionResult<userDto>> Login(LoginDto loginDto)
        {
            //email checking 
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            //password checking
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            //valid --> return user Dto
            return Ok(new userDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });

        }


        [HttpPost("register")]
        public async Task<ActionResult<userDto>> Register(RegisterDto registerDto)
        {
            if(DublicateEmailValidation(registerDto.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] {"this email is already taken"} });

            var user = new AppUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0]
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));


            return Ok(new userDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<userDto>> GetCurrentUser()
        {
            //get user-email
            var email = User.FindFirstValue(ClaimTypes.Email);
            //find by email
            var user = await _userManager.FindByEmailAsync(email);
            //return user
            return Ok(new userDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }


        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {

            var user = await _userManager.FindUserAddressAsync(User);
            var adress = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(adress);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
        {
            var address = _mapper.Map<AddressDto, Address>(updatedAddress);

            var user = await _userManager.FindUserAddressAsync(User);
            address.Id = user.Address.Id;
            user.Address = address;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(updatedAddress);
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> DublicateEmailValidation(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}

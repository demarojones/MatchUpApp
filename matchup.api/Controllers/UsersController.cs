using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using matchup.api.Data;
using matchup.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace matchup.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var userModels = _mapper.Map<IEnumerable<UserListModel>>(users);
            return Ok(userModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var userEntity = await _repo.GetUser(id);
            var userModel = _mapper.Map<UserDetailModel>(userEntity);
            return Ok(userModel);
        }
        
    }
}
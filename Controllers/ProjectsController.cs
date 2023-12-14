using AutoMapper;
using FollowingErrors.Dtos;
using FollowingErrors.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FollowingErrors.Controllers
{
    [Route($"api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProjectsController : ControllerBase
    {
        readonly BugsManager _db;
        readonly IMapper _mapper;

        public ProjectsController(BugsManager db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Projects = await _db.Project.ToListAsync();
            return Ok(Projects);
        }

    }
}

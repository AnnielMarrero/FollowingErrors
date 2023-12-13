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
    public class BugsController : ControllerBase
    {
        readonly BugsManager _db;
        readonly IMapper _mapper;
        public BugsController(BugsManager db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BugFilter filters)
        {
           //it's better using services instead of charge the controller 
            var bugs = _db.Bug.AsNoTracking();
            if (filters.UserId != null)
                bugs = bugs.Where( _ => _.UserId == filters.UserId);
            if (filters.ProjectId != null)
                bugs = bugs.Where(_ => _.ProjectId == filters.ProjectId);
            if (filters.StartDate != null)
                bugs = bugs.Where(_ => _.CreationDate >= filters.StartDate);
            if (filters.EndDate != null)
                bugs = bugs.Where(_ => _.CreationDate <= filters.EndDate);

            bugs = bugs.Include(_ => _.User).Include(_ => _.Project);
            var bugsDto = await bugs.ToListAsync();
            return bugsDto.Count == 0 ? NotFound("Any data match the filters") : Ok(new { bugs= _mapper.Map<ICollection<BugDto>>(bugsDto) } );
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddBugDto addBug)
        {
            Bug bug = _mapper.Map<Bug>(addBug);
            bug.CreationDate = DateTime.Now;
            _db.Bug.Add(bug);
            await _db.SaveChangesAsync();
            return Created($"/api/bugs/{bug.Id}", bug);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return await _db.Bug.AsNoTracking().Include(_ => _.Project).Include(_ => _.User)
                .FirstOrDefaultAsync(model => model.Id == id)
                is Bug model
                    ? Ok(_mapper.Map<BugDto>(model))
                    : NotFound();
        }
    }
}

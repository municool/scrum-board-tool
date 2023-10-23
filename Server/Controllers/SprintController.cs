using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scrum_board_tool.Server.Model;
using scrum_board_tool.Shared;

namespace scrum_board_tool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SprintController : ControllerBase
    {

        private readonly IDbContextFactory<ScrumBoardDbContext> _dbContextFactory;

        public SprintController(IDbContextFactory<ScrumBoardDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<Sprint> GetAllSprints()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Sprint.ToList();
            }
        }

        [HttpGet("{id}")]
        public Sprint? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Sprint.FirstOrDefault(p => p.Id == id);
            }
        }

        [HttpGet("GetByProjectId/{projectId}")]
        public IEnumerable<Sprint> GetByProjectId(int projectId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Sprint.Include(s => s.Project).Where(p => p.Project.Id == projectId).ToList();
            }
        }

        [HttpGet("GetCurrentSprint")]
        public Sprint? GetCurrentSprint()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var currentDate = DateTime.Now;
                var currentS = context.Sprint.Include(s => s.BacklogItems).ThenInclude(b => b.Tasks).ThenInclude(t => t.User).FirstOrDefault(s => s.StartDate <= currentDate && s.EndDate >= currentDate);
                
                if(currentS == null)
                {
                    currentS = context.Sprint.Where(s => s.EndDate < currentDate)
                                   .OrderByDescending(s => s.EndDate).Include(s => s.BacklogItems).ThenInclude(b => b.Tasks).ThenInclude(t => t.User)
                                   .FirstOrDefault();
                }
                if (currentS == null)
                {
                    currentS = context.Sprint.Where(s => s.StartDate > currentDate)
                                   .OrderByDescending(s => s.EndDate).Include(s => s.BacklogItems).ThenInclude(b => b.Tasks).ThenInclude(t => t.User)
                                   .FirstOrDefault();
                }

                return currentS;
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] Sprint sprint)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var currentProject = context.Project.Include(p => p.Sprints).FirstOrDefault(p => p.Id == sprint.Project.Id);

                if (currentProject == null)
                {
                    return NotFound();
                }

                currentProject.Sprints.Add(sprint);
                context.Project.Update(currentProject);  
                
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public ActionResult Edit(int id, [FromBody] Sprint sprint)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldSprint = context.Sprint.FirstOrDefault(p => p.Id == id);

                if (oldSprint != null)
                {
                    oldSprint.Name = sprint.Name;
                    oldSprint.StartDate = sprint.StartDate;
                    oldSprint.EndDate = sprint.EndDate;

                    context.Sprint.Update(oldSprint);
                    context.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var sprint = context.Sprint.FirstOrDefault(p => p.Id == id);

                if (sprint != null)
                {
                    context.Sprint.Remove(sprint);
                    context.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
    }
}

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

        [HttpGet("id")]
        public Sprint? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Sprint.FirstOrDefault(p => p.Id == id);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] Sprint project)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Sprint.Add(project);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("id")]
        public ActionResult Edit(int id, [FromBody] Sprint project)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldSprint = context.Sprint.FirstOrDefault(p => p.Id == id);

                if (oldSprint != null)
                {
                    oldSprint.Name = project.Name;
                    oldSprint.StartDate = project.StartDate;
                    oldSprint.EndDate = project.EndDate;

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

        [HttpDelete("id")]
        public ActionResult Delete(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var project = context.Sprint.FirstOrDefault(p => p.Id == id);

                if (project != null)
                {
                    context.Sprint.Remove(project);
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scrum_board_tool.Server.Model;
using scrum_board_tool.Shared;

namespace scrum_board_tool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BacklogController : ControllerBase
    {

        private readonly IDbContextFactory<ScrumBoardDbContext> _dbContextFactory;

        public BacklogController(IDbContextFactory<ScrumBoardDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<BacklogItem> GetAllBacklogItems()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.BacklogItem.ToList();
            }
        }

        [HttpGet("id")]
        public BacklogItem? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.BacklogItem.FirstOrDefault(p => p.Id == id);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] BacklogItem project)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.BacklogItem.Add(project);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("id")]
        public ActionResult Edit(int id, [FromBody] BacklogItem project)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldBacklogItem = context.BacklogItem.FirstOrDefault(p => p.Id == id);

                if (oldBacklogItem != null)
                {
                    oldBacklogItem.Name = project.Name;
                    oldBacklogItem.Description = project.Description;
                    oldBacklogItem.State = project.State;
                    oldBacklogItem.Effort = project.Effort;
                    oldBacklogItem.Sprint = project.Sprint;

                    context.BacklogItem.Update(oldBacklogItem);
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
                var project = context.BacklogItem.FirstOrDefault(p => p.Id == id);

                if (project != null)
                {
                    context.BacklogItem.Remove(project);
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

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

        [HttpGet("{id}")]
        public BacklogItem? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.BacklogItem.FirstOrDefault(p => p.Id == id);
            }
        }

        [HttpGet("GetByProjectId/{projectId}")]
        public IEnumerable<BacklogItem> GetByProjectId(int projectId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var bItems = context.BacklogItem.Where(b => b.Sprint.Project.Id == projectId).Include(b => b.Sprint).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                return bItems;
            }
        }


        [HttpPost]
        public ActionResult Create([FromBody] BacklogItem bItem)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var currentSprintId = bItem.Sprint?.Id ?? 0;
                var sprint = context.Sprint.Include(p => p.BacklogItems).FirstOrDefault(s => s.Id == currentSprintId);

                if (sprint == null)
                {
                    return NotFound();
                }

                sprint.BacklogItems.Add(bItem);

                context.Sprint.Update(sprint);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public ActionResult Edit(int id, [FromBody] BacklogItem bItem)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldBacklogItem = context.BacklogItem.FirstOrDefault(p => p.Id == id);

                if (oldBacklogItem != null)
                {
                    oldBacklogItem.Name = bItem.Name;
                    oldBacklogItem.Description = bItem.Description;
                    oldBacklogItem.State = bItem.State;
                    oldBacklogItem.Effort = bItem.Effort;
                    oldBacklogItem.Sprint = bItem.Sprint;

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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var bItem = context.BacklogItem.FirstOrDefault(p => p.Id == id);

                if (bItem != null)
                {
                    context.BacklogItem.Remove(bItem);
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

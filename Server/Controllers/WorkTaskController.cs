using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scrum_board_tool.Server.Model;
using scrum_board_tool.Shared;

namespace scrum_board_tool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkTaskController : ControllerBase
    {

        private readonly IDbContextFactory<ScrumBoardDbContext> _dbContextFactory;

        public WorkTaskController(IDbContextFactory<ScrumBoardDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<WorkTask> GetAllWorkTasks()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Task.ToList();
            }
        }

        [HttpGet("{id}")]
        public WorkTask? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Task.FirstOrDefault(p => p.Id == id);
            }
        }

        [HttpGet("GetByBItemId/{bItemId}")]
        public IEnumerable<WorkTask> GetByBItemId(int bItemId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var workTasks = context.Task.Where(b => b.BacklogItem.Id == bItemId).ToList();
                return workTasks;
            }
        }


        [HttpPost]
        public ActionResult Create([FromBody] WorkTask workTask)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var currentSprintId = workTask.BacklogItem?.Id ?? 0;
                var bItem = context.BacklogItem.Include(p => p.Tasks).FirstOrDefault(s => s.Id == currentSprintId);

                if (bItem == null)
                {
                    return NotFound();
                }

                bItem.Tasks.Add(workTask);

                context.BacklogItem.Update(bItem);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public ActionResult Edit(int id, [FromBody] WorkTask workTask)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldWorkTask = context.Task.FirstOrDefault(p => p.Id == id);

                if (oldWorkTask != null)
                {
                    oldWorkTask.Name = workTask.Name;
                    oldWorkTask.Description = workTask.Description;
                    oldWorkTask.State = workTask.State;
                    oldWorkTask.TimeRemaining = workTask.TimeRemaining;

                    context.Task.Update(oldWorkTask);
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
                var workTask = context.Task.FirstOrDefault(p => p.Id == id);

                if (workTask != null)
                {
                    context.Task.Remove(workTask);
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scrum_board_tool.Server.Model;
using scrum_board_tool.Shared;

namespace scrum_board_tool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IDbContextFactory<ScrumBoardDbContext> _dbContextFactory;

        public ProjectController(IDbContextFactory<ScrumBoardDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<Project> GetAllProjects()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Project.ToList();
            }
        }

        [HttpGet("id")]
        public Project? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Project.FirstOrDefault(p => p.Id == id);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] Project project)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Project.Add(project);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("id")]
        public ActionResult Edit(int id, [FromBody] Project project)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldProject = context.Project.FirstOrDefault(p => p.Id == id);

                if (oldProject != null)
                {
                    oldProject.Name = project.Name;
                    oldProject.Description = project.Description;

                    context.Project.Update(oldProject);
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
                var project = context.Project.FirstOrDefault(p => p.Id == id);

                if (project != null)
                {
                    context.Project.Remove(project);
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

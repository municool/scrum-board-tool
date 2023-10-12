using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scrum_board_tool.Server.Model;
using scrum_board_tool.Shared;

namespace scrum_board_tool.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IDbContextFactory<ScrumBoardDbContext> _dbContextFactory;

        public UserController(IDbContextFactory<ScrumBoardDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.User.ToList();
            }
        }

        [HttpGet("id")]
        public User? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.User.FirstOrDefault(p => p.Id == id);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] User project)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.User.Add(project);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("id")]
        public ActionResult Edit(int id, [FromBody] User project)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldUser = context.User.FirstOrDefault(p => p.Id == id);

                if (oldUser != null)
                {
                    oldUser.Name = project.Name;
                    oldUser.Role = project.Role;

                    context.User.Update(oldUser);
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
                var project = context.User.FirstOrDefault(p => p.Id == id);

                if (project != null)
                {
                    context.User.Remove(project);
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

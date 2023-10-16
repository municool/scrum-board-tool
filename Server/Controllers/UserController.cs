using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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

        [HttpGet("{id}")]
        public User? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.User.FirstOrDefault(p => p.Id == id);
            }
        }

        [HttpGet("GetByProjectId/{projectId}")]
        public IEnumerable<User> GetByProjectId(int projectId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var users = context.User.Where(u => u.Project.Id == projectId).ToList();
                return users;
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] User user)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var project = context.Project.FirstOrDefault(p => p.Id == user.Project.Id);
                if(project == null)
                {
                    return NotFound();
                }

                project.Users.Add(user);
                context.Project.Update(project);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public ActionResult Edit(int id, [FromBody] User user)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldUser = context.User.FirstOrDefault(p => p.Id == id);

                if (oldUser != null)
                {
                    oldUser.Name = user.Name;
                    oldUser.Role = user.Role;

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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = context.User.FirstOrDefault(p => p.Id == id);

                if (user != null)
                {
                    context.User.Remove(user);
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


namespace scrum_board_tool.Shared
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Sprint> Sprints { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; } = null!;
    }
}

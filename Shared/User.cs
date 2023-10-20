namespace scrum_board_tool.Shared
{
    public class User
    {
        public User()
        {
            Project = new Project();
            Tasks = new HashSet<WorkTask>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Role Role { get; set; }

        public virtual Project? Project { get; set; } = null!;
        public virtual ICollection<WorkTask> Tasks { get; set; } = null!;
    }
}
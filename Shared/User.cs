namespace scrum_board_tool.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public virtual Project Project { get; set; } = null!;
        public virtual ICollection<Task> Tasks { get; set; } = null!;
    }
}
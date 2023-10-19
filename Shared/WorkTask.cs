namespace scrum_board_tool.Shared
{
    public class WorkTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TimeRemaining { get; set; }
        public StateOfItems State { get; set; }

        public virtual BacklogItem? BacklogItem { get; set; } = null!;
        public virtual User? User { get; set; } = null!;
    }
}

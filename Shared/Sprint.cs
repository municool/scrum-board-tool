namespace scrum_board_tool.Shared
{
    public class Sprint
    {
        public Sprint()
        {
            BacklogItems = new HashSet<BacklogItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Project? Project { get; set; } = null!;
        public virtual ICollection<BacklogItem> BacklogItems { get; set; } = null!;
    }
}

using scrum_board_tool.Server.Model;

namespace scrum_board_tool.Shared
{
    public class BacklogItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority {  get; set; }
        public StateOfItems State {  get; set; }
        public int Effort { get; set; }

        public virtual Sprint? Sprint { get; set; }
        public virtual ICollection<Task> Tasks{ get; set; } = null!;
    }
}

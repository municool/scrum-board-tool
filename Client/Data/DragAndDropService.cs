namespace scrum_board_tool.Client.Data
{
    public class DragAndDropService
    {
        public object Data { get; set; } = new object();
        public string Zone { get; set; } = string.Empty;

        public void StartDrag(object data, string zone)
        {
            Data = data;
            Zone = zone;
        }

        public bool Accepts(string zone)
        {
            return Zone == zone;
        }
    }
}

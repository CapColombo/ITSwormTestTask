namespace ITSwormTestTask.Database
{
    public class Furniture
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<Panel> Panels { get; set; }
    }
}

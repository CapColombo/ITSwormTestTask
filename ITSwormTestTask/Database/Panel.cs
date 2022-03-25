namespace ITSwormTestTask.Database
{
    public class Panel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Fastener>? Fasteners { get; set; } // крепежи
        public List<Accessorie>? Accessories { get; set; } // комплектующие
        public Furniture Furniture { get; set; }
    }
}

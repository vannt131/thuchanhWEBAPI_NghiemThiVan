namespace ThucHanhDBFirst.Entites
{
    public class CustomerOrder
    {
        public long Id { get; set; }

        public string Name { get; set; } = "";

        public int? Age { get; set; }

        public string? Gender { get; set; } = "";

        public string? Address { get; set; } = "";

        public int Status { get; set; }

        public string Username { get; set; } = "";

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = "";

        public DateTime? UpdatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public virtual List<Product>? Products { get; set; }
    }
}

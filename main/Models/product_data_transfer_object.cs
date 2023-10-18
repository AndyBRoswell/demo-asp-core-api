namespace main.Models {
    public class product_data_transfer_object {
        public long ID { get; set; }
        public string? brand { get; set; }
        public string? model { get; set; }
        public string? batch { get; set; }
        public string? type { get; set; } // Planned: string[]. Serialize to JSON instead.
        public string? intro { get; set; }
        public string? spec { get; set; } // Planned: object. Serialize to JSON instead.
    }
}

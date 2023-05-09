namespace BoxMachineInventary.Models
{
    public class CardbordPalletInventary
    {
        public int Id { get; set; }

        public int? Code { get; set; }

        public string? Provider { get; set; }

        public int? Size { get; set; }

        public int? PalletQuantity { get; set; }

        public DateTime? EntryDate { get; set; }

        public DateTime? UsedDate { get; set; }

        public int? IsUsed { get; set; }
    }
}

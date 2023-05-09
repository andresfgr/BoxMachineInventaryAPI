namespace BoxMachineInventary.Models
{
    public class BoxMachineProductionSheet
    {
        public int Id { get; set; }

        public int BoxSizeId { get; set; }

        public int? ProducedQuantity { get; set; }

        public DateTime? ProducedDate { get; set; }
    }
}

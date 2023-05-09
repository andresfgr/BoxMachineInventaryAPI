namespace BoxMachineInventary.EntityModels
{
    public class BoxMachineProductionSheetModel
    {
        public int Id { get; set; }

        public int BoxSizeId { get; set; }

        public string? BoxSizeName { get; set; }

        public int? ProducedQuantity { get; set; }

        public DateTime? ProducedDate { get; set; }
    }
}

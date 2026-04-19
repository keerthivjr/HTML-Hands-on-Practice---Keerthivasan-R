namespace Shared.DTOs
{
    public class CreateOrderDTO
    {
        public int UserId { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
    }

    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
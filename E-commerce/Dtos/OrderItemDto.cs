﻿namespace E_commerce.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string PicUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
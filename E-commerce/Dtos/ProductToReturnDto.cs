using E_commerce.Core.Entities;

namespace E_commerce.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public decimal Price { get; set; }
      
        public int ProductBrandId { get; set; }
        
        public string Brand { get; set; }
       
        public int ProductTypeId { get; set; }
        
        public string ProductType { get; set; }
    }
}

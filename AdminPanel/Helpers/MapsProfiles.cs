using AdminPanel.Models;
using AutoMapper;
using E_commerce.Core.Entities;

namespace AdminPanel.Helpers
{
    public class MapsProfiles : Profile
    {
        public MapsProfiles()
        {
            CreateMap<Product,ProductVM>().ReverseMap();
        }
    }
}

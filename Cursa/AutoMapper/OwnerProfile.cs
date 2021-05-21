using AutoMapper;
using Cursa.ViewModels.OwnerVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class OwnerProfile:Profile
    {
        public OwnerProfile()
        {
            CreateMap<Owner, OwnerDisplayViewModel>();
        }
    }
}
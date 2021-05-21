using AutoMapper;
using Cursa.ViewModels.ContractorsVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ContractorsProfile:Profile
    {
        public ContractorsProfile()
        {
            CreateMap<Contractor, ContractorsDisplayViewModel>();
        }
    }
}
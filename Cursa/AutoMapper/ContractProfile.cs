using AutoMapper;
using Cursa.ViewModels.SubProjectVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ContractProfile:Profile
    {
        public ContractProfile()
        {
            CreateMap<Contract, ContractPartDisplayViewModel>();
        }
    }
}
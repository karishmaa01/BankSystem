using AutoMapper;
using BankSystem.Domain.Model;
using BankSystem.DTO;
using BankSystem.Service.Mapping;

namespace BankSystem.MapperProfile
{
    public class ATDtoProfile :Profile
    {
        public ATDtoProfile()
        {
            CreateMap<ATDto, ATMapping>();
            //(source,destination)

        }
    }
}

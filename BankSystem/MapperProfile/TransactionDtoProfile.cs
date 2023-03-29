using AutoMapper;
using BankSystem.Domain.Model;
using BankSystem.DTO;
using BankSystem.Service.Mapping;

namespace BankSystem.MapperProfile
{
    public class TransactionDtoProfile :Profile
    {
        public TransactionDtoProfile()
        {
            CreateMap<TransactionDto,TDetails>();
            //(source,destination)
          
        }
    }
}

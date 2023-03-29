using AutoMapper;
using BankSystem.Domain.Model;
using BankSystem.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Service.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CustomerMapping, Customer>().ReverseMap();
            //(source,destination)

        }
    }
}

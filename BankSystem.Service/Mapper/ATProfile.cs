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
    public class ATProfile :Profile
    {
        public ATProfile()
        {
            CreateMap< ATMapping, AmountTransfer>().ReverseMap();
            //(source,destination)

        }
    }
}

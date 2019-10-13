using AutoMapper;
using romeojovelchatapi.Domain.Models;
using romeojovelchatapi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Mapping
{
    public class GeneralMappingProfile:Profile
    {
        public GeneralMappingProfile()
        {
            CreateMap<TbUser, RegisterRequest>()
                .ForMember(x=>x.Email,y=>y.MapFrom(dest=>dest.DsEmail))
                .ForMember(x=>x.Password,y=>y.MapFrom(source=>source.DsPassword)).ReverseMap();
        }
    }
}

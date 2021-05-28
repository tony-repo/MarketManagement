using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarketManagement.Model.Domain;
using MarketManagement.Model.Entities;

namespace MarketManagement.Model.AutoMapper
{
    public static class MapperSetup
    {
        public static MapperConfiguration MapperConfiguration => new MapperConfiguration(
            cfg =>
            {
                //cfg.AddProfile(EntiyToDomainProfile);
                //cfg.AddProfile(DomainToDtoProfile);
                cfg.CreateMap<UserEntity, User>()
                    .ForMember(e => e.OrganizationId, o => o.Ignore());
                cfg.CreateMap<User, UserEntity>()
                    .ForMember(e => e.OrganizationId, o => o.Ignore());
            }
            );
    }
}

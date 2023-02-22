using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Configuration;
using TME.Scenario.Default.Enums;
using TME.Scenario.Default.Interfaces;
using TME.Scenario.Default.Items;
using TME.Scenario.Default.Scenario;
using TME.SpecTests.Mapping.Models;

namespace TME.SpecTests.Mapping
{
    public class MappingConfiguration : MapperConfigurationExpression
    {
        public MappingConfiguration()
        {
            CreateMap<TestCharacter, Character>(MemberList.None)
                .ForMember(a => a.Warriors, b => b.Ignore())
                .ForMember(a => a.Riders, b => b.Ignore())
                .ForMember(a => a.Units, b => b.MapFrom(c =>
                    new List<IUnit>
                    {
                        new Warriors {Type = UnitType.Warrior, Total = c.Warriors},
                        new Riders {Type = UnitType.Rider, Total = c.Riders}
                    }));

            CreateMap<TestRegiment, Regiment>(MemberList.None);
            CreateMap<TestStronghold, Stronghold>(MemberList.None);

        }
    }
}

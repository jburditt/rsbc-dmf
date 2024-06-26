﻿using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using PidpAdapter;
using Rsbc.Dmf.CaseManagement.Service;
using RSBC.DMF.MedicalPortal.API.ViewModels;

namespace RSBC.DMF.MedicalPortal.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //#TODO Move this to shared folder
            CreateMap<Timestamp, DateTimeOffset>()
             .ConvertUsing(src => src.ToDateTimeOffset());

            CreateMap<Document, CaseDocument>()
                .ForMember(dest => dest.DmerType, opt => opt.MapFrom(src => src.DmerType))
                .ForMember(dest => dest.DmerStatus, opt => opt.MapFrom(src => src.DmerStatus))
                // TODO rename to IdCode
                .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.Case.CaseNumber))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Case.Person.FullName))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Case.Person.Birthday))
                .ForMember(dest => dest.ComplianceDate, opt => opt.MapFrom(src => src.ComplianceDate));

            CreateMap<LegacyDocument, ViewModels.CaseDocument>()
             .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
             .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreateDate));

            CreateMap<DocumentSubType, ViewModels.DocumentSubTypes>();

            CreateMap<EndorsementDto, Endorsement>();
            CreateMap<PidpAdapter.Licence, ViewModels.Licence>();
        }
    }

    public static class AutoMapperExtensions
    {
        public static void AddAutoMapperSingleton(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}

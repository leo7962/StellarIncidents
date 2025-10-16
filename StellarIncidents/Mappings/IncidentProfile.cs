using AutoMapper;
using StellarIncidents.Application.Dtos;
using StellarIncidents.Domain.Entities;

namespace StellarIncidents.Mappings;

public class IncidentProfile : Profile
{
    public IncidentProfile()
    {
        // Incident → IncidentDto
        CreateMap<Incident, IncidentDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.ReporterName, opt => opt.MapFrom(src => src.ReporterUser.FullName))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));

        // Comment → CommentDto
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.AuthorUser.FullName));

        // DTO → Entity (para creación)
        CreateMap<CreateIncidentDto, Incident>();
        CreateMap<CommentCreateDto, Comment>();
    }
}
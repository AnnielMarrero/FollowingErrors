using AutoMapper;
using FollowingErrors.Dtos;
using FollowingErrors.Entities;

namespace FollowingErrors.Mapper
{
    public class BugMapperProfile : Profile
    {
        public BugMapperProfile()
        {
            CreateMap<AddBugDto, Bug>();

            CreateMap<Bug, BugDto>()
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(e => e.User.Name))
                .ForMember(dto => dto.UserId, opt => opt.MapFrom(e => e.User.Id))
                .ForMember(dto => dto.Project, opt => opt.MapFrom(e => e.Project.Name))
                .ForMember(dto => dto.ProjectId, opt => opt.MapFrom(e => e.Project.Id));
        }
    }
}

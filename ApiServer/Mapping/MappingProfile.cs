using AutoMapper;
using DAL.Core.Models;
using DAL.Resources;
using System.Linq;

namespace ApiServer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resouce with Reverse
            CreateMap<UserRegistration, User>()
                .ReverseMap();

            CreateMap<UsersListResource, User>()
                .ReverseMap();

            CreateMap<EventAddResource, Event>()
                .ReverseMap();

            CreateMap<EventListResource, Event>()
                .ReverseMap()
                .ForMember(t => t.Attendance, opt => opt.MapFrom(u => u.Users.Select(ua => new AttendanceResource { UserId = ua.User.Id })));

            CreateMap<SabhaTypeResource, SabhaType>()
                .ForMember(t => t.Sabha, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<SabhaAddResource, Sabha>()
                .ReverseMap();

            CreateMap<Sabha, SabhaListResource>()
                .ForMember(t => t.SabhaType, opt => opt.MapFrom(s => s.SabhaType.Type))
                .ForMember(t => t.Users, opt => opt.MapFrom(u => u.Users.Select(ua => new UsersListResource { Id = ua.User.Id, FirstName = ua.User.FirstName, MidName = ua.User.MidName, LastName = ua.User.LastName, HomePhone = ua.User.HomePhone, PhoneNumber = ua.User.PhoneNumber, Email = ua.User.Email })))
                .ReverseMap();

            CreateMap<AddAttendanceResource, Event>()
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.Users, opt => opt.Ignore())
                .AfterMap((a, e) => {
                    // Remove unselected Users
                    var removedUsers = e.Users.Where(u => !a.Users.Contains(u.UserId)).ToList();
                    foreach (var f in removedUsers)
                    {
                        e.Users.Remove(f);
                    }

                    // Add new Users
                    var addedUsers = a.Users.Where(id => !e.Users.Any(u => u.UserId == id)).Select(id => new EventAttendance { UserId = id }).ToList();
                    foreach (var f in addedUsers)
                    {
                        e.Users.Add(f);
                    }
                })
                .ReverseMap();
        }

    }
}

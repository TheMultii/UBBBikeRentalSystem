using AutoMapper;
using UBBBikeRentalSystem.Converters;
using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Mapper {
    public class UBBMapperProfile : Profile {
        public UBBMapperProfile(UBBBikeRentalSystemDatabase db) {

            CreateMap<VehicleDetailViewModel, Vehicle>()
                .ForMember(
                    dest => dest.VehicleType,
                    opt => opt.MapFrom(src => VehicleTypeConverter.ToVehicleType(src.VehicleType, db))
                );
            CreateMap<Vehicle, VehicleDetailViewModel>()
                .ForMember(
                    dest => dest.VehicleType,
                    opt => opt.MapFrom(src => VehicleTypeConverter.ToVehicleTypeEnum(src.VehicleType))
                );

            CreateMap<VehicleItemViewModel, Vehicle>()
                .ForMember(
                    dest => dest.VehicleType,
                    opt => opt.MapFrom(src => VehicleTypeConverter.ToVehicleType(src.VehicleType, db))
                );
            CreateMap<Vehicle, VehicleItemViewModel>()
                .ForMember(
                    dest => dest.VehicleType,
                    opt => opt.MapFrom(src => VehicleTypeConverter.ToVehicleTypeEnum(src.VehicleType))
                );

            CreateMap<ReservationPoint, ReservationPointViewModel>().ReverseMap();

            CreateMap<Reservation, ReservationViewModel>()
                .ForMember(
                    dest => dest.User,
                    opt => opt.MapFrom(src => src.UserID)
                )
                .ForMember(
                    dest => dest.Vehicle,
                    opt => opt.MapFrom(src => src.VehicleID)
                )
                .ForMember(
                    dest => dest.ReservationPoint,
                    opt => opt.MapFrom(src => src.ReservationPoint)
                )
                .ForMember(
                    dest => dest.ReturnPoint,
                    opt => opt.MapFrom(src => src.ReturnPoint)
                ).ReverseMap();

            CreateMap<User, UserViewModel>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.UserName)
                );

            CreateMap<User, UserWithReservationsViewModel>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.UserName)
                );
        }

        public UBBMapperProfile() : this(new UBBBikeRentalSystemDatabase()) {}
    }
}
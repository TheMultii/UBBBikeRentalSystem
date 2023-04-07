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

            CreateMap<Reservation, ReservationViewModel>().ReverseMap();

            CreateMap<User, UserViewModel>().ReverseMap();
        }

        public UBBMapperProfile() : this(new UBBBikeRentalSystemDatabase()) { }
    }
}
using AutoMapper;
using apicourseproject1.Data.Entities;
using apicourseproject1.Data.DTOs;

namespace apicourseproject1.Data.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarDto, Car>()
                .ForMember(car => car.Id,
                    opt => opt.MapFrom(carDto => carDto.Id))
                .ForMember(car => car.make,
                    opt => opt.MapFrom(carDto => carDto.Make))
                .ForMember(car => car.citympg,
                    opt => opt.MapFrom(carDto => carDto.Citympg))
                .ForMember(car => car.cylindernumber,
                    opt => opt.MapFrom(carDto => carDto.Cylindernumber))
                .ForMember(car => car.enginesize,
                    opt => opt.MapFrom(carDto => carDto.Enginesize))
                .ForMember(car => car.horsepower,
                    opt => opt.MapFrom(carDto => carDto.Horsepower))
                .ForMember(car => car.carbody,
                    opt => opt.MapFrom(carDto => carDto.Carbody))
                .ForMember(car => car.peakrpm,
                    opt => opt.MapFrom(carDto => carDto.Peakrpm))
                .ForMember(car => car.price,
                    opt => opt.MapFrom(carDto => carDto.Price))
                .ReverseMap();

            CreateMap<List<CarFlat>, CarDto>()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => src.First().Id))
                .ForPath(dest => dest.Make, opt => opt.MapFrom(src => src.First().make))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src));

            CreateMap<OptionsDto, CarFlat>()
                .ForMember(dest => dest.option_id, opt => opt.MapFrom(src => src.OptionId))
                .ForMember(dest => dest.option_name, opt => opt.MapFrom(src => src.OptionName))
                .ForMember(dest => dest.option_price, opt => opt.MapFrom(src => src.OptionPrice))
                .ReverseMap();
        }
    }
}
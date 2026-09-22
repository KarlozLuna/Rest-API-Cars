using apicourseproject1.Data.DTOs;
using apicourseproject1.Data.Entities;
using apicourseproject1.Data.Interfaces;
using AutoMapper;
namespace apicourseproject1.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CarDto>> GetAll(
            bool showDeleted,
            int pageNumber,
            int pageSize)
        {
            var cars = await _carRepository.Get(
                showDeleted,
                pageNumber,
                pageSize);

            return _mapper.Map<IEnumerable<CarDto>>(cars);
        }
        public async Task<List<CarFlat>> Get(int id)
        {
            if (id == 0)
            {
                throw new Exception("Invalid Id");
            }
            return await _carRepository.Get(id);
        }

        public async Task<Car> Insert(Car car)
        {
            var newId = await _carRepository.UpsertAsync(car);
            if (newId > 0)
            {
                car.Id = newId;
            }
            else
            {
                throw new Exception("Failed to insert car");
            }
            return car;
        }

        public async Task<Car> Update(Car car)
        {
            if (car.Id == 0)
            {
                throw new Exception("Id must be set");
            }

            var oldId = car.Id;

            var newId = await _carRepository.UpsertAsync(car);

            if (newId != oldId)
            {
                throw new Exception("Failed to update car");
            }
            return car;
        }

        public async Task Delete(int id)
        {
            var car = await _carRepository.Get(id);

            if (car == null)
            {
                throw new Exception("Car not found");
            }

            await _carRepository.DeleteAsync(id);

            return;
        }
    }
    }

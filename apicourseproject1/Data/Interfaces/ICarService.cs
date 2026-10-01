using apicourseproject1.Data.Entities;

namespace apicourseproject1.Data.Interfaces
{
    public interface ICarService
    {
        Task<Car> Insert(Car car);
        Task<Car> Update(Car car);
        Task Delete(int id);
        Task<List<CarFlat>> Get(int id);
    }


}

using apicourseproject1.Data.Entities;

namespace apicourseproject1.Data.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> Get(
            bool returnDeletedRecords,
            int pageNumber,
            int pageSize);

        Task<List<CarFlat?>> Get(int id);

        Task<int> UpsertAsync(Car car);

        Task<int> DeleteAsync(int id);
    }
}

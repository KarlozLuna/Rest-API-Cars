
namespace apicourseproject1.Data.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string make { get; set; } = null!;
        public string citympg { get; set; } = null!;
        public string cylindernumber { get; set; } = null!;
        public string enginesize { get; set; } = null!;
        public string horsepower { get; set; } = null!;
        public string carbody { get; set; } = null!;
        public string peakrpm { get; set; } = null!;
        public string price { get; set; } = null!;
        public string? is_deleted { get; set; }
    }
}

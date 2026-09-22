namespace apicourseproject1.Data.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Make { get; set; } = null!;
        public string Citympg { get; set; } = null!;
        public string Cylindernumber { get; set; } = null!;
        public string Enginesize { get; set; } = null!;
        public string Horsepower { get; set; } = null!;
        public string Carbody { get; set; } = null!;
        public string Peakrpm { get; set; } = null!;
        public string Price { get; set; } = null!;
        public List<OptionsDto> Options { get; set; }
    }
}
using apicourseproject1.Data.DTOs;
using apicourseproject1.Data.Entities;
using apicourseproject1.Data.Interfaces;
using apicourseproject1.Services;
using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace apicourseproject1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {

        private readonly ILogger<CarController> _logger;
        private readonly CarService _carService;
        private readonly IMapper _mapper;

        public CarController(ILogger<CarController> logger,
            CarService carService,
            IMapper mapper)
        {
            _logger = logger;
            _carService = carService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Car>> Insert([FromBody] CarDto carAsDto)
        {
            try
            {
                if (carAsDto == null)
                {
                    return BadRequest("No car was provided");
                }

                var carToInsert = _mapper.Map<Car>(carAsDto);
                var insertedCar = await _carService.Insert(carToInsert);
                var insertedCarDto = _mapper.Map<CarDto>(insertedCar);
                var location = $"https://localhost:7128/car/{insertedCarDto.Id}";
                return Created(location, insertedCarDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get all the cars in the Database 
        /// </summary>
        /// <param name="returnDeletedRecords">If true, the method will return all the records</param> 
        /// <response code="200">Cars returned</response>
        /// <response code="404">Specified Car not found</response>
        /// <response code="500">An Internal Server Error prevented the request from being executed.</response>
        [HttpGet("{showDeleted}/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAll(
            [FromRoute] bool showDeleted,
            [FromRoute] int pageNumber,
            [FromRoute] int pageSize)
         {
                var cars = await _carService.GetAll(showDeleted, pageNumber, pageSize);

                return Ok(cars);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> Get(int id)
        {
            var car = await _carService.Get(id);
            if (car == null)
            {
                return NotFound();
            }
            var carDto = _mapper.Map<CarDto>(car); 
            return carDto;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Car car)
        {
            try
            {
                await _carService.Update(car);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _carService.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return NoContent();
        }
    }
}

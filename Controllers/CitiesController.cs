using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/cities")]
    /*[Route("api/[controller]")]*/
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        /*[HttpGet("api/cities")]*/
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();


            var results = _mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);
            //var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);

            /*var results = new List<CityWithoutPointsOfInterestDto>();

            foreach(var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterestDto
                {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name
                });
            }*/

            return Ok(results);

            //return Ok(CitiesDataStore.Current.Cities);
            /*var temp = new JsonResult(CitiesDataStore.Current.Cities);
            temp.StatusCode = 200;
            return temp;*/
        }
        /*public JsonResult GetCities()
        {
            return new JsonResult(CitiesDataStore.Current.Cities);
            //return new JsonResult(new List<object>()
            //{
            //   new { id=1, Name="New York City" },
            //    new { id=2, Name="Antwerp"}
            //});
        }*/

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var cityResult = _mapper.Map<CityDto>(city);

                /*var cityResult = new CityDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description
                };

                foreach(var poi in city.PointsOfInterest)
                {
                    cityResult.PointsOfInterest.Add(
                        new PointOfInterestDto()
                        {
                            Id = poi.Id,
                            Name = poi.Name,
                            Description = poi.Description
                        });
                }*/

                return Ok(cityResult);
            }

            var cityWithoutPointsOfInterestResult = _mapper.Map<CityWithoutPointsOfInterestDto>(city);

           /* var cityWithoutPointsOfInterestResult =
                new CityWithoutPointsOfInterestDto()
                {
                    Id = city.Id,
                    Description = city.Description,
                    Name = city.Name
                };*/

            return Ok(cityWithoutPointsOfInterestResult);

            // find city
            /*var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);*/
        }
        /*[HttpGet("api/cities/{id}")]*/
        /*public JsonResult GetCity(int id)
        {
            return new JsonResult(
                CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id)
                );
        }*/
    }
}

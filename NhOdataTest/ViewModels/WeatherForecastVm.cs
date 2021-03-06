﻿using AutoMapper;
using NhOdataTest.Entities;
using System;

namespace NhOdataTest.ViewModels
{
    public class WeatherForecastVm : BaseWeatherForecastVm
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string Summary { get; set; }

        public string Town2 { get; set; }
    }

    public class WeatherForecastVmAutoMapperProfile : Profile
    {
        public WeatherForecastVmAutoMapperProfile()
        {
            CreateMap<WeatherForecast, WeatherForecastVm>()
                .ForMember(d => d.TemperatureF, map => map.MapFrom(s => 32 + (int)(s.TemperatureC / 0.5556)))
                .ForMember(d => d.Town, map => map.MapFrom(s => s.Town.Name))
                .ForMember(d => d.Town2, map => map.MapFrom(s => s.Town.Name));
        }
    }
}

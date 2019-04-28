using AutoMapper;
using FwData.Entities;
using Movies.Models;
using System;

namespace Movies.Mappers
{
    public class MovieMapProfile : Profile
    {
        public MovieMapProfile()
        {
            CreateMap<Movie, MovieModel>()
                .ForMember(m => m.AverageRating, o => o.MapFrom(s => ConvertToAvreageRating(s.AverageRating)));
        }
        double ConvertToAvreageRating(double rating)
        {
            return Math.Round(rating * 2, MidpointRounding.AwayFromZero) / 2;
        }
    }
}
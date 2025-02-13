using AutoMapper;
using Movies.API.DTOs;
using Movies.API.Models;

namespace Movies.API.Mappers
{
	public class MovieProfile : Profile
	{
		public MovieProfile()
		{
			CreateMap<MovieDTO, Movie>().ReverseMap();
		}
	}
}

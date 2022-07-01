using AutoMapper;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.Application.MovieOperations.Queries.GetMovieById;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.Entities;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Movie, GetMoviesViewModel>().ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>src.Year.Year));
        CreateMap<Movie, GetMovieByIdViewModel>().ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>src.Year.Year));
        CreateMap<CreateMovieModel, Movie>().ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>new DateTime(src.Year,01,01)));
        CreateMap<UpdateMovieModel, Movie>()
            .ForMember(dest=> dest.Id, opt=> opt.Ignore())
            .ForMember(dest=> dest.Name, opt=> opt.Condition(src=>src.Name is not null))
            .ForMember(dest=> dest.Year, opt=> opt.Condition(src=>src.Year is not null))
            .ForMember(dest=> dest.Price, opt=> opt.Condition(src=>src.Price is not null))
            .ForMember(dest=> dest.GenreId, opt=> opt.Condition(src=>src.GenreId is not null))
            .ForMember(dest=> dest.DirectorId, opt=> opt.Condition(src=>src.DirectorId is not null))
            .ForMember(dest=> dest.IsActive, opt=> opt.Condition(src=>src.IsActive is not null));
    }
}
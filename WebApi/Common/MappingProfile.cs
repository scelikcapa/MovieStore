using AutoMapper;
using WebApi.Entities;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.Application.MovieOperations.Queries.GetMovieById;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.Application.CustomerOperations.Queries.GetCustomers;
using WebApi.Application.CustomerOperations.Queries.GetCustomerById;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.UpdateCustomer;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.Application.ActorOperations.Queries.GetActorById;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.Application.DirectorOperations.Queries.GetDirectorById;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.Application.CustomerMoviesOperations.Queries.GetCustomerMoviesById;
using System.Globalization;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // MovieOperations
        CreateMap<Movie, GetMoviesViewModel>().ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>src.Year.Year));
        CreateMap<Movie, GetMovieByIdViewModel>().ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>src.Year.Year));
        CreateMap<CreateMovieModel, Movie>().ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>new DateTime(src.Year,01,01)));
        CreateMap<UpdateMovieModel, Movie>()
            .ForMember(dest=> dest.Id, opt=> opt.Ignore())
            .ForMember(dest=> dest.Title, opt=> opt.Condition(src=>src.Title is not null))
            .ForMember(dest=> dest.Year, opt=> opt.Condition(src=>src.Year is not null))
            .ForMember(dest=> dest.Year, opt=> opt.MapFrom(src=>new DateTime(src.Year.Value,01,01)))
            .ForMember(dest=> dest.Price, opt=> opt.Condition(src=>src.Price is not null))
            .ForMember(dest=> dest.GenreId, opt=> opt.Condition(src=>src.GenreId is not null))
            .ForMember(dest=> dest.DirectorId, opt=> opt.Condition(src=>src.DirectorId is not null))
            .ForMember(dest=> dest.IsActive, opt=> opt.Condition(src=>src.IsActive is not null));
        
        // CustomerOperations
        CreateMap<Customer, GetCustomersViewModel>();
        CreateMap<Customer, GetCustomerByIdViewModel>();
        CreateMap<CreateCustomerModel, Customer>();
         CreateMap<UpdateCustomerModel, Customer>()
            .ForMember(dest=> dest.Id, opt=> opt.Ignore());
        // CustomerGenres
        CreateMap<Genre, GetCustomerGenresViewModel>();
        // CustomerMovies
        CreateMap<CustomerMovie, GetCustomerMoviesByIdViewModel>()
            .ForMember(dest=> dest.OrderDate, opt=> opt.MapFrom(src=> src.OrderDate.ToString("yyyy-MM-dd hh:mm:ss")));

        // ActorOperations
        CreateMap<Actor, GetActorsViewModel>();
        CreateMap<Actor, GetActorByIdViewModel>();
        CreateMap<CreateActorModel, Actor>();
         CreateMap<UpdateActorModel, Actor>()
            .ForMember(dest=> dest.Id, opt=> opt.Ignore())
            .ForMember(dest=> dest.Name, opt=> opt.Condition(src=> src.Name is not null))
            .ForMember(dest=> dest.Surname, opt=> opt.Condition(src=> src.Surname is not null));

        // DirectorOperations
        CreateMap<Director, GetDirectorsViewModel>();
        CreateMap<Director, GetDirectorByIdViewModel>();
        CreateMap<CreateDirectorModel, Director>();
        CreateMap<UpdateDirectorModel, Director>()
            .ForMember(dest=> dest.Id, opt=> opt.Ignore())
            .ForMember(dest=> dest.Name, opt=> opt.Condition(src=> src.Name is not null))
            .ForMember(dest=> dest.Surname, opt=> opt.Condition(src=> src.Surname is not null));
        
        
    }
}
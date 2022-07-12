using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.UnitTests.TestSetup;

public class CommonTestFixture 
{
    public MovieStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        //Context.Database.EnsureDeleted();
        var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase("MovieStoreTestDb").Options;
        Context = new MovieStoreDbContext(options);
        Context.Database.EnsureCreated();
        Context.CreateGenres();
        Context.CreateDirectors();
        Context.CreateActors();
        Context.CreateCustomers();
        Context.CreateMovies();
        Context.CreateCustomerMovies();

        Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
    }
}
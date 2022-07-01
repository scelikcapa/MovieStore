using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IMovieStoreDbContext context;

    public CustomersController(IMovieStoreDbContext context, IMapper mapper)
    {
        this.mapper = mapper;
        this.context = context;
    }

    


}
using LibraryManagement.Controllers;
using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Api.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : BaseController<Genre>
    {
        public GenresController(IBaseService<Genre> _service): base(_service)
        {
        }
    }
}

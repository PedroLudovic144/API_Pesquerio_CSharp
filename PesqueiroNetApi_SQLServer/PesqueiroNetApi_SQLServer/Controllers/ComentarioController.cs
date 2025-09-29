using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PesqueiroNetApi.Data;


namespace PesqueiroNetApi_SQLServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentarioController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ComentarioController(AppDbContext db)
        {
           _db = db;
        }

    }
}
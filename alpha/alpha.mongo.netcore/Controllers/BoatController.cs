﻿using Alpha.Mongo.Netcore.Models;
using Alpha.Mongo.Netcore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoatController : AlphaController<Boat>
    {
        public BoatController(IAlphaRepository<Boat> repository) : base(repository)
        {

        }
    }
}

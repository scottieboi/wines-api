using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WinesApi.Models;

namespace WinesApi.Api.FindWines
{
    [ApiController]
    [Route("wines")]
    public class FindWinesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Box> Get()
        {
            using (var db = new DataContext())
            {
                return db.Boxes.ToList();
            }
        }
    }
}
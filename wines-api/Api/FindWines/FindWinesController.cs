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
        private IFindWinesService _findWinesService;

        public FindWinesController(IFindWinesService findWinesService)
        {
            _findWinesService = findWinesService;
        }

        [HttpGet]
        public IEnumerable<FindWinesResponse> Get()
        {
            return _findWinesService.Find();
        }
    }
}
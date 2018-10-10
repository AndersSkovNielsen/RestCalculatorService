using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ModelLibrary;
using ModelLibrary.Model;

namespace RestCalculatorService.Controllers
{
    [Produces("application/json")]
    [Route("api/Calculator/")]
    public class CalculatorController : Controller
    {
        // GET: api/Calculator
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Calculator/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Calculator
        [HttpPost("Add", Name = "Add")]
        public int PostAdd([FromBody]Data data)
        {
            return data.A + data.B;
        }

        //Andet eksempel på en URI med variabler, der kan bruges i en browser (fordi kun GET virker i browser da vi ikke kan lave body)
        // GET: api/Calculator
        [HttpGet("Add/{x}/{y}", Name = "Add")]
        public int GetAdd(int x, int y)
        {
            return x + y;
        }

        //Tredje eksempel for api/Calculator?Operation=Add (hvor Operation er en String property i vores QueryData klasse)
        // POST: api/Calculator
        [HttpPost("Add", Name = "Add")]
        public int PostGeneric([FromBody]Data data, [FromQuery] QueryData qData)
        {
            if (qData.Operation == "Add")
            {
                return data.A + data.B;
            }

            if (qData.Operation == "Sub")
            {
                return data.A + data.B;
            }

            if (qData.Operation == "Mul")
            {
                return data.A + data.B;
            }

            if (qData.Operation == "Div")
            {
                return data.A + data.B;
            }

            throw new ArgumentException("Operation not supported");
        }

        // POST: api/Calculator
        [HttpPost("Subtract", Name = "Subtract")]
        public int PostSubtract([FromBody]Data data)
        {
            return data.A - data.B;
        }

        // POST: api/Calculator
        [HttpPost("Multiply", Name = "Multiply")]
        public int PostMultiply([FromBody]Data data)
        {
            return data.A * data.B;
        }

        // POST: api/Calculator
        [HttpPost("Divide", Name = "Divide")]
        public int PostDivide([FromBody]Data data)
        {
            return data.A / data.B;
        }

        // PUT: api/Calculator/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

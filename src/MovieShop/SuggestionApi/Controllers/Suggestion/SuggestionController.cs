using Microsoft.AspNetCore.Mvc;
using SuggestionApi.NLP.TernaryTree;
using System.Collections.Generic;

namespace SuggestionApi.Controllers.Suggestion
{
    [ApiController]
    [Route("[controller]")]
    public class SuggestionController : ControllerBase
    {
        private readonly ITernarySearchFactory _ternarySearchFactory;

        public SuggestionController(ITernarySearchFactory ternarySearchFactory)
        {
            _ternarySearchFactory = ternarySearchFactory;
        }

        [HttpGet]
        public string Get()
        {
            return "I am alive!";
        }

        [HttpGet("{index}")]
        public IEnumerable<string> GetQuery(string index, string q)
        {
            var ternarySearch = _ternarySearchFactory.Get(index);

            var result = ternarySearch.Compleate(q);

            return result;
        }
    }
}
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Alinta.Core.Responses
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; }

        public ApiBadRequestResponse(ModelStateDictionary modelState)
            : base(StatusCodes.Status400BadRequest)
        {            
            Errors = modelState.SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage).ToArray();
        }
    }
}

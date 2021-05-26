using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Constants;
using Zedcrest.DocumentManager.Domain.Models.RequestModels.CommandRequestModels;
using Zedcrest.DocumentManager.Domain.Models.RequestModels.QueryRequestModels;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels;

namespace Zedcrest.DocumentManager.Application.Features.Documents
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]UploadUserRequestModel model)
        {
            try
            {
               var response = await _mediator.Send(model);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string> { Message =ResponseMessages.InternalError, Success = false, Data =ex.Message });
            }
        }



        [HttpGet(template:"{reference}")]
        public async Task<IActionResult> Get([FromRoute]string reference)
        {
            try
            {
                var model = new GetUserByReferenceRequestModel
                {
                    Reference = reference
                };

                var response = await _mediator.Send(model);

                return StatusCode(201,response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string> { Message = ResponseMessages.InternalError, Success = false, Data = ex.Message });
            }
        }
    }
}

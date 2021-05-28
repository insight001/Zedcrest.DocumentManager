using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Constants;
using Zedcrest.DocumentManager.Domain.Exceptions;
using Zedcrest.DocumentManager.Domain.Models.RequestModels.CommandRequestModels;
using Zedcrest.DocumentManager.Domain.Models.RequestModels.QueryRequestModels;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels.CommandResponseModels;
using Zedcrest.DocumentManager.Domain.Models.ResponseModels.QueryResponseModels;

namespace Zedcrest.DocumentManager.Application.Features.Documents
{
    [Route("api/v1/document-manager")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// This endpoints allows user to upload profile data and documents
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(APIResponse<UploadUserResponseModel>),(int)HttpStatusCode.Created)]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]UploadUserRequestModel model)
        {
            if (!ModelState.IsValid)
                return StatusCode(400, new APIResponse<string> { Success = false, Message = "Some parameters failed validation" });
            try
            {
               var response = await _mediator.Send(model);

                return StatusCode(201,response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string> { Message =ResponseMessages.InternalError, Success = false, Data =ex.Message });
            }
        }


        /// <summary>
        /// This endpoint returns user profile and documents on receipt of the user's reference
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(APIResponse<GetUserByReferenceResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(APIResponse<string>), (int)HttpStatusCode.NotFound)]
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

                return StatusCode(200,response);
            }
            catch(RestException ex)
            {
                return StatusCode((int)ex.Code, new APIResponse<string> { Message = ex.Message, Success = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string> { Message = ResponseMessages.InternalError, Success = false, Data = ex.Message });
            }
        }
    }
}

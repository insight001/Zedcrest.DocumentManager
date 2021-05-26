using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zedcrest.DocumentManager.Domain.Models.DTO;

namespace Zedcrest.DocumentManager.Domain.Models.ResponseModels.QueryResponseModels
{
    public class GetUserByReferenceResponseModel
    {
        public UserDTO Profile { get; set; }
        public List<DocumentDTO> Documents { get; set; }
    }
}

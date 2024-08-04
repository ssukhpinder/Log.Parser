using Asp.Versioning;
using Log.Parser.BL.Interface;
using Log.Parser.BL.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Log.Parser.Server.Controllers.v1
{
    [ApiVersion(1.0)]
    [Route("api/v{v:apiVersion}/parse")]
    [ApiController]
    public class FileParseController : ControllerBase
    {

        private readonly IParserService _parserService;
        private readonly IFileManageService _fileManageService;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="parserService"></param>
        /// <param name="fileManageService"></param>
        public FileParseController(IParserService parserService, IFileManageService fileManageService)
        {
            _parserService = parserService;
            _fileManageService = fileManageService;
        }


        /// <summary>
        /// A method which takes three arguments as
        /// 
        /// 1. File Upload
        /// 2. Start date and time [Optional]
        /// 3. End date and time [Optional]
        /// 
        /// and used to fetch number of accesses to webserver per host
        /// </summary>
        /// <param name="file"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("access-per-host")]
        [ProducesResponseType<ResponseMetaData<List<KeyValueResponse>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseMetaData<List<KeyValueResponse>>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseMetaData<List<KeyValueResponse>>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAccessPerHost(IFormFile file, DateTime? startDate = null, DateTime? endDate = null)
        {
            var filePath = await _fileManageService.Upload(file);

            var responseMetadata = new ResponseMetaData<List<KeyValueResponse>>()
            {
                Status = HttpStatusCode.OK,
                IsError = false,
                Result = await _parserService.GetAccessPerHost(filePath, startDate, endDate)
            };
            return StatusCode((int)responseMetadata.Status, responseMetadata);
        }

        /// <summary>
        /// A method which takes three arguments as
        /// 
        /// 1. File Upload
        /// 2. Start date and time [Optional]
        /// 3. End date and time [Optional]
        /// 
        /// and used to fetch number of successful resource accesses by URI. Only “GET” accesses to each URI are to be
        /// counted. Only requests which resulted in the HTTP reply code 200 (“OK”) are to be counted.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("success-per-uri")]
        [ProducesResponseType<ResponseMetaData<List<KeyValueResponse>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ResponseMetaData<List<KeyValueResponse>>>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ResponseMetaData<List<KeyValueResponse>>>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSuccessPerUri(IFormFile file, DateTime? startDate = null, DateTime? endDate = null)
        {
            var filePath = await _fileManageService.Upload(file);
            var responseMetadata = new ResponseMetaData<List<KeyValueResponse>>()
            {
                Status = HttpStatusCode.OK,
                IsError = false,
                Result = await _parserService.GetSuccessPerUri(filePath, startDate, endDate)
            };
            return StatusCode((int)responseMetadata.Status, responseMetadata);
        }
    }
}

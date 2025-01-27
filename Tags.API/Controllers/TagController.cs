using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tags.Application.Models;
using Tags.Application.Models.ModelsDTO.TagModels;
using Tags.Application.Requests.TagRequests.Download;
using Tags.Application.Requests.TagRequests.Get;

namespace Tags.API.Controllers
{
    /// <summary>
    /// Controller to get tags.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public sealed class TagController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Action to get the tags list from database.
        /// </summary>
        /// <response code="200">Returns the paged result of TagPercentageDTO from database.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<TagPercentageDTO>))]
        public async Task<ActionResult> Get([FromQuery] GetTagsListQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Action to get the tags list from SO API. New tags will be added to database. Tags existed in datebase will be updated.
        /// </summary>
        /// <response code="200">Returns the paged result of TagDTO fetched from SO API.</response>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<TagDTO>))]
        public async Task<ActionResult> DownloadTags([FromQuery] DownloadTagsQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }
    }
}
using System;
using System.Threading.Tasks;
using AttributeRouting.Web.Http;
using Cbhs.Svc.OshcReceipt.Application.Queries.GetPremiumByMemberId;
using Cbhs.Svc.OshcReceipt.Application.Queries.GetQuote;
using Cbhs.Svc.OshcReceipt.Application.Queries.GetReceiptByReceiptId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cbhs.Svc.OshcReceipt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : BaseController
    {
        //PLEASE NOTE URI ARE NOT RESOURCE BASE. This is in preparation to Azure Functions
        [HttpGet("GetQuote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuoteViewModel>> GetQuote([FromQuery] decimal discountPercentage,
            [FromQuery] decimal rebatePercentage,
            [FromQuery] decimal productCost,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            return Ok(await Mediator.Send(new GetQuoteQuery
            {
                DiscountPercentage = discountPercentage,
                RebatePercentage = rebatePercentage,
                ProductCost = productCost,
                StartDate = startDate,
                EndDate = endDate
            }));
        }

        [HttpGet("GetReceiptByReceiptId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuoteViewModel>> GetReceiptByReceiptId(int id)
        {
            return Ok(await Mediator.Send(new GetReceiptByReceiptIdQuery
            {
                ReceiptId = id
            }));
        }

        [HttpGet("CalculatePremium/{memberId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuoteViewModel>> CalculatePremium(int memberId)
        {
            return Ok(await Mediator.Send(new GetPremiumByMemberIdQuery
            {
                memberId = memberId
            }));
        }
    }
}
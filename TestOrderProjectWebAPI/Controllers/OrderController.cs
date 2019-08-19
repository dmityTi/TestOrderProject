using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTOs.OrderAggregate;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TestOrderProjectWebAPI.Models.RequestOrderAggregate;

namespace TestOrderProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService service)
        {
            _orderService = service;
        }
        
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        /// /// <response code="200">Returns orders</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        /// <summary>
        /// Get order by oxid
        /// </summary>
        /// <param name="oxid"></param>
        /// <returns>order</returns>
        /// <response code="200">Returns the order</response>
        /// <response code="404">If the order is not exist</response>  
        [HttpGet("{oxid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(long oxid)
        {
            var order = _orderService.GetOrder(oxid, true);
            if(order == null)
                return NotFound();

            return Ok(order);
        }
        
        /// <summary>
        /// Create orders.
        /// </summary>
        /// <remarks>
        /// Note that orders in xml-format
        /// 
        /// Sample request:
        ///        <!--<orders>
        ///           <order>
        ///              <oxid>70002222</oxid>
        ///              <orderdate>2018-12-14T15:48:11</orderdate>
        ///               <billingaddress>
        ///                   <billemail>wittmann.k@web.de</billemail>
        ///                   <billfname>Krissi Wittmann</billfname>
        ///                   <billstreet>Allerstrasse</billstreet>
        ///                   <billstreetnr>47</billstreetnr>
        ///                   <billcity>Berlin</billcity>
        ///                   <country>
        ///                      <geo>DE</geo>
        ///                  </country>
        ///                  <billzip>12049</billzip>
        ///              </billingaddress>
        ///              <payments>
        ///                  <payment>
        ///                      <method-name>INVOICE</method-name>
        ///                      <amount>130.5</amount>
        ///                   </payment>
        ///              </payments>
        ///               <articles>
        ///                  <orderarticle>
        ///                   <artnum>00471500007</artnum>
        ///                  <title>Track Suit Women</title>
        ///                   <amount>1</amount>
        ///                    <brutprice>60</brutprice>
        ///                    </orderarticle>
        ///                </articles>
        ///            </order>
        ///        </orders> -->
        /// </remarks>
        /// <param name="orders"></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody]OrderRequestArrayModel orders)
        {
            if (orders?.Orders == null)
                return BadRequest();

            IEnumerable<OrderDTO> orderDTOs;
            try
            {
                 orderDTOs = Mapper.Map<List<OrderRequestModel>, List<OrderDTO>>(orders.Orders);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            
            await _orderService.CreateOrders(orderDTOs);
            return Ok();
        }

        /// <summary>
        /// Update order by oxid
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///        [{
        ///            "op": "replace",
        ///            "path": "/status",
        ///           "value": "Processed"
        ///        }]
        /// </remarks>
        /// <param name="oxid"></param>
        /// <param name="patchOrder"></param>
        /// <returns></returns>
        [HttpPatch("{oxid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch(long oxid, [FromBody] JsonPatchDocument<OrderDTO> patchOrder)
        {                     
            if(patchOrder == null)
                return BadRequest();

            var orderDto = _orderService.GetOrder(oxid, true);
            if (orderDto == null)
                return NotFound();

            patchOrder.ApplyTo(orderDto);

            await _orderService.UpdateOrder(orderDto);
            return NoContent();
        }
    }
}
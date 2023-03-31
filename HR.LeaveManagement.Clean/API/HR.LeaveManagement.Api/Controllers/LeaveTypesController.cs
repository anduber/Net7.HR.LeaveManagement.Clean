using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Features.LeaveType.Command.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Command.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Command.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        // GET: api/LeaveTypes
        [HttpGet]
        public async Task<List<LeaveTypeDto>> Get()
        {
            return await _mediator.Send(new GetLeaveTypeQuery());
        }

        // GET: api/LeaveTypes/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypesDetailsQuery(id));
            return Ok(leaveType);
        }

        // POST: api/LeaveTypes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(CreateLeaveTypeCommand leaveTypeCommand)
        {
            var response = await _mediator.Send(leaveTypeCommand);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT: api/LeaveTypes/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(UpdateLeaveTypeCommand updateLeaveTypeCommand)
        {
            await _mediator.Send(updateLeaveTypeCommand);
            return NoContent();
        }

        // DELETE: api/LeaveTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteLeaveTypeCommand = new DeleteLeaveTypeCommand { Id = id };
            await _mediator.Send(deleteLeaveTypeCommand);
            return NoContent();
        }
    }
}

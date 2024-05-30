using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignR.Api;

namespace SignR
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHubContext<ChatHub> hubContext;

        public ValuesController(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }


        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            await hubContext.Clients.All.SendAsync("Okkk");
            return Ok("1");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be null or empty.");
            }

            await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok("Message sent successfully.");
        }

    }
}

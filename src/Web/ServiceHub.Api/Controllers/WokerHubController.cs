using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServiceHub.Api.Hubs;
using ServiceWorker.Core.DTO;
using ServiceWorker.Core.Menssages.Commands;
using ServiceWorker.Core.SignalR;

namespace ServiceHub.Api.Controllers
{
    [Route("Hub/[controller]")]
    [ApiController]
    public class WokerHubController : ControllerBase
    {

        private readonly IHubContext<WokerHub> _wokerHubContext;
        private readonly ILogger<WokerHubController> _logger;

        public WokerHubController(IHubContext<WokerHub> wokerHubContext,
                                  ILogger<WokerHubController> logger)
        {
            _wokerHubContext = wokerHubContext;
            _logger = logger;
        }

        #region Metodos        
        [HttpPost("biometria/{group}")]
        public async Task<IActionResult> LeitorBiometrico([FromRoute] string group)
        {
            //Notifica todos os hubs conectados
            //await _wokerHubContext.Clients.All.SendAsync(group + SignalRName.WokerHub, mensagem);

            //await _wokerHubContext.Clients.All.SendAsync(SignalRName.WokerHub, command);
            await _wokerHubContext.Clients.Group(group.ToUpper()).SendAsync(SignalRName.WokerHub, new RequestHubDTO(CommandConst.REALIZA_LEITURA_BIOMETRIA));
            return Ok();
        }

        [HttpPost("nfc/{group}")]
        public async Task<IActionResult> LeitorNfc([FromRoute] string group)
        {
            //await _wokerHubContext.Clients.All.SendAsync(SignalRName.WokerHub, command);
            await _wokerHubContext.Clients.Group(group.ToUpper()).SendAsync(SignalRName.WokerHub, new RequestHubDTO(CommandConst.REALIZA_LEITURA_NFC));
            return Ok();
        }
        #endregion
    }
}
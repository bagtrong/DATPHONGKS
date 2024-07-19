using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.WebApi;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace KStypemig.Controllers.ControllerAPI
{

    public class BotController : ApiController
    {
        private readonly IBotFrameworkHttpAdapter _adapter;
        private readonly IBot _bot;

        public BotController(IBotFrameworkHttpAdapter adapter, IBot bot)
        {
            _adapter = adapter;
            _bot = bot;
        }

        public async Task<HttpResponseMessage> Post()
        {
            // Handle incoming bot messages
            var activity = await Request.Content.ReadAsAsync<Activity>();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            await _adapter.ProcessAsync(Request, response, _bot, default);
            return response;
        }

    }
    //

}

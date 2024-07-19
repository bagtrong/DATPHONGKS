using System;
using System.Web;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;
namespace KStypemig.Bot
{

    //public class EchoBot : ActivityHandler
    //{
    //    protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    //    {
    //        var replyText = $"You said: {turnContext.Activity.Text}";
    //        await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
    //    }
    //}

    public class EchoBot : IBot
    {
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var replyText = $"You said: {turnContext.Activity.Text}";
                await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
            }
        }
    }

}

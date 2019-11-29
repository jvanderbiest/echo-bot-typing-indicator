// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var activity = await CreateActivityWithTextAndSpeak($"Echo: {turnContext.Activity.Text}");
            await RunSomething();
            await turnContext.SendActivityAsync(activity, cancellationToken);
            await RunSomething();
        }

        private async Task RunSomething()
        {
            await Task.Delay(5000);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    var activity = MessageFactory.Text($"Hello and welcome!");
                    await turnContext.SendActivityAsync(activity, cancellationToken);
                }
            }
        }

        private async Task<IActivity> CreateActivityWithTextAndSpeak(string message)
        {
            await Task.Delay(1000);

            var activity = MessageFactory.Text(message);
            string speak = @"<speak version='1.0' xmlns='https://www.w3.org/2001/10/synthesis' xml:lang='en-US'>	
              <voice name='Microsoft Server Speech Text to Speech Voice (en-US, JessaRUS)'>" +
              $"{message}" + "</voice></speak>";
            activity.Speak = speak;
            return activity;
        }
    }
}

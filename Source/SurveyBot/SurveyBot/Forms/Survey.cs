using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;

namespace SurveyBot
{
    [Serializable]
    public class Survey
    {
        public static IForm<Survey> BuildForm()
        {
            OnCompletionAsyncDelegate<Survey> processSurvey = async (context, state) =>
            {
                // we store the data in Table storage
                new StorageHelper().StoreSurvey(state.ConvertToDTO());
                await context.PostAsync("Thanks! The survey is done. We registered your information and will get back to you as soon as possible.");
            };

            return new FormBuilder<Survey>()
                    .Message("Welcome to the quick survey! Let's ask you some questions")
                    .AddRemainingFields()
                    .OnCompletion(processSurvey)
                    .Build();
        }

        [Prompt("Please give me your name")]
        public string Name;

        [Pattern(@"^\w+([\.+_])*\w+@\w+(\.\w+)+$")]
        [Template(TemplateUsage.NotUnderstood, "\"{0}\" does not seem to be a valid email.")]
        [Prompt("Could you please give me your **email** so we can get back to you")]
        public string Email;

        [Numeric(1, 10)]
        [Prompt("Could you rate your experience with the bot (from 1 to 10)")]
        public string Mark;
    }
}
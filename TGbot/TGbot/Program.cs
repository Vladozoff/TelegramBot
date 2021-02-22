using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TGbot
{
    class Program
    {
        static public ITelegramBotClient botClient;
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("1561413152:AAEVC7VbONJLKd0KXMm8qB97SNlxP1qJyWI");    //
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine( $"Hello, World! I am user {me.Id} and my name is {me.FirstName}. username={me.Username}");

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);


        }
        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)                                                    // если текстовое сообщение 
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id} and chat username={e.Message.Chat.Username}");

                switch (e.Message.Text)
                {
                    case var message when message == "/start":
                        await SendMessage(e.Message.Chat, "Привет привет");
                        break; 

                    case var message when message == "/help":
                        await SendMessage(e.Message.Chat, "Сейчас помогу...");
                        break;

                    case var message when message == "/hello":
                        await SendMessage(e.Message.Chat, "Здарова");
                        break;
                }
                        

            }
        }

        static async Task  SendMessage(Chat chatId, string message)
        {
            await botClient.SendTextMessageAsync(
              chatId: chatId,
              text: message
            );
        }
    }
}

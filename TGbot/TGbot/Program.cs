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
            botClient = new TelegramBotClient("YOUR_BOT_TOKEN");    
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine( $"Спасите меня, я бот по имени {me.FirstName}, мой UserName {me.Username}");

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
                        await SendMessage(e.Message.Chat, "Привет, "+e.Message.Chat.FirstName+"! Ты попал в VzBot! Здесь ты можешь узнать о моем создателе немного больше.\n Для справки введи команду: /help");
                        break; 

                    case var message when message == "/help":
                        await SendMessage(e.Message.Chat, "Команды:\n/info - Связь со создателем\n/skill - Скиллы создателя\n/github - ссылка на GitHub\n/hh - ссылка на резюме");
                        break;

                    case var message when message == "/github":
                        await SendMessage(e.Message.Chat, "В нём немного скудно. Но держи ссылку на GitHub: https://github.com/Vladozoff");
                        break;

                    case var message when message == "/hh":
                        await SendMessage(e.Message.Chat, "Не знаю зачем тебе это... Но держи ссылку на hh: https://kazan.hh.ru/resume/7fa8afffff083fcb680039ed1f504379477234");
                        break;

                    case var message when message == "/info":
                        await SendMessage(e.Message.Chat, "Почта: nikitinvlad98@gmail.com\nTelegtam: @Vladozofff \nНомер телефона: Одному боту известно:)");
                        break;

                    case var message when message == "/skill":
                        await SendMessage(e.Message.Chat, "Навыки:\n-C#(ООП, Entity Framework)\n-SQL\n-HTML+CSS\n-IDA PRO + Ollydbg");
                        break;

                    default:
                        await SendMessage(e.Message.Chat, "Внимание команда, не найдена!");
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViberMessageSenderAPI.Models;
using ViberMessageSenderAPI.Contracts;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using Viber.Bot;
using Azure;
using Com.CloudRail.SI.Services;
using Com.CloudRail.SI.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO;

namespace ViberMessageSenderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViberMessagesController : ControllerBase
    {
        private readonly IViberMessageService _messageService;
        private readonly IConfiguration _configuration;
        private readonly ViberBotClient _viberBotClient;

        public ViberMessagesController(IViberMessageService messageService, IConfiguration configuration, ViberBotClient viberBotClient)
        {
            _messageService = messageService;
            _configuration = configuration;
            _viberBotClient = viberBotClient;
        }


        [HttpPost]
        public IActionResult CreateMessage(PhoneSender message)
        {
            var result = _messageService.SendViberMessage(message);

            if (result)
            {
                return Ok("Message sent successfully");
            }
            else
            {
                return BadRequest("Failed to send the message");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMessageById(int id)
        {
            var message = _messageService.GetViberMessageById(id);

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMessage(int id, PhoneSender message)
        {
            var updatedMessage = _messageService.UpdateViberMessage(id, message);

            if (updatedMessage != null)
            {
                return Ok(updatedMessage);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var result = _messageService.DeleteViberMessage(id);

            if (result)
            {
                return Ok("Message deleted successfully");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("setwebhook")]
        public IActionResult SetWebhook()
        {
            string authToken = _configuration["AppSettings:ViberAuthToken"];
            string webhookUrl = "https://localhost:44395/api/viber/handlewebhook";

            string[] eventTypes = { "message", "subscribed", "unsubscribed", "delivered", "seen" };

            var payload = new
            {
                url = webhookUrl,
                event_types = eventTypes,
                auth_token = authToken
            };

            string jsonPayload = JsonConvert.SerializeObject(payload);

            var client = new RestClient("https://chatapi.viber.com/pa/set_webhook");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jsonPayload, ParameterType.RequestBody);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok("Webhook set successfully");
            }
            else
            {
                return BadRequest("Failed to set webhook");
            }
        }

        [HttpPost("handlewebhook")]
        public IActionResult HandleWebhook([FromBody] JObject payload)
        {
            var eventType = payload["event"].ToString();

            switch (eventType)
            {
                case "message":
                    HandleIncomingMessage(payload);
                    break;

                case "subscribed":
                    HandleSubscribedEvent(payload);
                    break;

                case "unsubscribed":
                    HandleUnsubscribedEvent(payload);
                    break;

                case "delivered":
                    HandleDeliveredEvent(payload);
                    break;

                case "seen":
                    HandleSeenEvent(payload);
                    break;

                case "conversation_started":
                    HandleConversationStartedEvent(payload);
                    break;

                case "error":
                    HandleErrorEvent(payload);
                    break;

                default:
                    break;
            }

            return Ok();
        }


        private void HandleIncomingMessage(JObject payload)
        {
            var message = payload["message"]["text"].ToString();
            var senderId = payload["sender"]["id"].ToString();

            var replyMessage = new TextMessage
            {
                Receiver = senderId,
                Text = "Received your message: " + message
            };

            _viberBotClient.SendTextMessageAsync(replyMessage).Wait();
        }

        private void HandleSubscribedEvent(JObject payload)
        {
            var senderId = payload["user"]["id"].ToString();
            SendReplyToUser(senderId, "Welcome to our Viber bot!");
        }

        private void HandleUnsubscribedEvent(JObject payload)
        {
            var userId = payload["user_id"].ToString();
            UpdateUserStatus(userId, "Unsubscribed");
        }

        private void HandleDeliveredEvent(JObject payload)
        {
            var messageId = payload["message_token"].ToString();
            UpdateMessageDeliveryStatus(messageId, "Delivered");
        }

        private void HandleSeenEvent(JObject payload)
        {
            var messageId = payload["message_token"].ToString();
            UpdateMessageSeenStatus(messageId, "Seen");
        }

        private void HandleConversationStartedEvent(JObject payload)
        {
            var userId = payload["user"]["id"].ToString();
            var subscribed = payload["subscribed"].ToString();
            StoreConversationInformation(userId, subscribed);
        }

        private void HandleErrorEvent(JObject payload)
        {
            var errorDescription = payload["desc"].ToString();
            LogError(errorDescription);
        }


        private void SendReplyToUser(string userId, string message)
        {
            var replyMessage = new TextMessage
            {
                Receiver = userId,
                Text = message
            };

            _viberBotClient.SendTextMessageAsync(replyMessage).Wait();
        }

        private void UpdateUserStatus(string userId, string status)
        {
            
        }

        private void UpdateMessageDeliveryStatus(string messageId, string status)
        {
            
        }

        private void UpdateMessageSeenStatus(string messageId, string status)
        {
           
        }

        private void StoreConversationInformation(string userId, string subscribed)
        {
            
        }

        private void LogError(string errorDescription)
        {
           
        }
    }
}
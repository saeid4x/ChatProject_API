using ChatProject.Application.ChatRooms.Commands;
using ChatProject.Domain.Entities;
using ChatProject.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ChatProject.API.Hubs
{
   [Authorize]
    public class ChatHub:Hub
    {
        //private readonly ApplicationDbContext _context;
        //private readonly ILogger<ChatHub> _logger; // Add logging
        private readonly IMediator _mediator;


        //public ChatHub(ApplicationDbContext context, ILogger<ChatHub> logger)
        //{
        //    _context = context;
        //    _logger = logger;
        //}

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }



        // Join a chat room using its ID (as string)
        public async Task JoinRoom(Guid ChatRoomId)
        {

            // Use chatRoomId.ToString() as group name
            await Groups.AddToGroupAsync(Context.ConnectionId, ChatRoomId.ToString());
            var username = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonymous";

            await Clients.Group(ChatRoomId.ToString()).SendAsync("ReceiveMessage", "System", $"{username} joined the group");
 
        }




        // Leave a chat room (group)
        public async Task LeaveRoom(Guid chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId.ToString());

            var username = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonymous";

           await  Clients.Group(chatRoomId.ToString()).SendAsync("ReceiveMessage", "System", $"{username} left the room");


        }




        // Send a message to a chat room (group chat)
        public async Task SendMessageToRoom(Guid chatRoomId, string message,string senderUsername)
        {

            // Regex to find mentions
            var mentions = Regex.Matches(message, @"@([A-Za-z0-9_]+)");

            foreach(Match mention in mentions)
            {
                var mentionedUsername = mention.Groups[1].Value;

                // logic to Notify the mentioned user
                await Clients.Group(chatRoomId.ToString())
                        .SendAsync("MentionNotification", mentionedUsername);
            }


            // Send message to the group
           // await Clients.Group(chatRoomId.ToString()).SendAsync("ReceiveMessage", senderUsername, message);

 
            var senderId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";

            var senderName = Context.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Anonymous";

            if (string.IsNullOrWhiteSpace(senderId)) return;

           
            var command = new CreateGroupMessageCommand
            {
                ChatRoomId = chatRoomId,
                SenderId = senderId,
                Content = message
            };


            var newMessage = await _mediator.Send(command);

          
            var messageDto = new
            {
                senderUserName = senderName,
                Content = newMessage.Content,
                CreatedAt = newMessage.CreatedAt
            };



            await Clients.Group(chatRoomId.ToString()).SendAsync("ReceiveMessage", messageDto);
        }


 
        public async Task TypingIndicator(Guid chatRoomId , string username)
        {
            await Clients.Group(chatRoomId.ToString())
                    .SendAsync("ReceiveTypingIndicator", username);
        }

        // Show online/offline status
        // Called when a client connects
        public override async Task OnConnectedAsync()
        {
            var username = Context.User?.Identity?.Name ?? "Anonymous";
            await Clients.All.SendAsync("UserOnline", username);
            await base.OnConnectedAsync();
        }


        // Called when a client disconnects
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var username = Context.User?.Identity?.Name ?? "Anonymous"; 
            await Clients.All.SendAsync("UserOffline" , username);
            await base.OnDisconnectedAsync(exception);
        }

        
       
        public async Task MarkAsDelivered(Guid messageId , string userId)
        {
            await Clients.Group(messageId.ToString())
                    .SendAsync("MessageDelivered", userId, messageId);
        }

       
        public async Task MarkAsSeen(Guid messageId , string userId)
        {
            await Clients.Group(messageId.ToString())
                    .SendAsync("MessageSeen", userId, messageId);
        }









        //// Send a private message (one-on-one chat)
        //public async Task SendPrivateMessage(string receiverUserId , string message)
        //{
        //    // For demonstration: create and store the message.
        //    var senderId = Context.UserIdentifier;

        //    var msg = new Message
        //    {
        //        Id = Guid.NewGuid(),
        //        SenderId = senderId,
        //        Content = message,
        //        CreatedAt = DateTime.UtcNow,
        //        IsEdited = false
        //        // For a one-on-one chat, you might not use ChatRoomId (or use a special room ID)
        //    };

        //    //_context.Messages.Add(msg);
        //    //await _context.SaveChangesAsync();

        //    // Send the message to the target user (SignalR maps users via their User Identifier)
        //    await Clients.User(receiverUserId)
        //            .SendAsync("ReceivePrivateMessage", Context.User.Identity.Name, message);
        //}

         


         

    }
}

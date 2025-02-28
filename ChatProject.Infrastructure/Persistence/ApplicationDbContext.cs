using ChatProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Infrastructure.Persistence
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<UserChatRoom> UserChatrooms { get; set; }
        public DbSet<MessageReaction> MessageReactions { get; set; }
        public DbSet<FileAttachment> FileAttachments { get; set; }
        public DbSet<Profile> Profiles { get; set; }
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Profile>()
              .HasOne(p => p.User)
              .WithOne(u => u.Profile)
              .HasForeignKey<Profile>(p => p.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            // Many-to-Many: users <->ChatRooms
            builder.Entity<UserChatRoom>()
                .HasKey(uc => new { uc.UserId, uc.ChatRoomId });

            builder.Entity<UserChatRoom>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChatRooms)
                .HasForeignKey(uc=> uc.UserId) ;

            builder.Entity<UserChatRoom>()
                .HasOne(uc => uc.ChatRoom)
                .WithMany(cr => cr.UserChatRooms)
                .HasForeignKey(uc => uc.ChatRoomId);


            // One-to-Many: ChatRoom <-> Messages
            builder.Entity<Message>()
                .HasOne(m => m.ChatRoom)
                .WithMany(m => m.Messages)
                .HasForeignKey(a => a.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);


            // One-to-Many:  User <-> Messages
            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(s => s.Messages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Cascade);


            // Many-to-Many: Messages <-> MessageReactions
            builder.Entity<MessageReaction>()
               .HasKey(mr => new { mr.MessageId, mr.UserId });

            builder.Entity<MessageReaction>()
                .HasOne(m=>m.Message)
                .WithMany(a =>a.MessageReactions)
                .HasForeignKey(a => a.MessageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<MessageReaction>()
                .HasOne(c => c.User)
                .WithMany(c => c.MessageReactions)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Message <-> FileAttachments
            builder.Entity<FileAttachment>()
                .HasOne(f => f.Message)
                .WithMany(m => m.FileAttachments)
                .HasForeignKey(f => f.MessageId)
                 .OnDelete(DeleteBehavior.Cascade);











        }
    }
}

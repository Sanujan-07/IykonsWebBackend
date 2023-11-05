using Iycons_web2._0.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace Iycons_web2._0.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> TagPosts { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostTag>()
           .HasKey(tp => new { tp.TagId, tp.PostId });

            modelBuilder.Entity<PostTag>()
                .HasOne(tp => tp.Tag)
                .WithMany(t => t.TagPosts)
                .HasForeignKey(tp => tp.TagId);

            modelBuilder.Entity<PostTag>()
                .HasOne(tp => tp.Post)
                .WithMany(p => p.TagPosts)
                .HasForeignKey(tp => tp.PostId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(b => b.User);

            modelBuilder.Entity<Posts>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Posts);

            modelBuilder.Entity<Posts>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.Posts);

            modelBuilder.Entity<Posts>()
                .HasMany(b => b.MediaItems)
                .WithOne(m => m.Post);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUI.Models;
namespace WebUI.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");

          //  builder.Entity<CommentReply>()
          //             .HasOne(cr => cr.User)
          //             .WithMany(u => u.CommentReplies)
          //             .HasForeignKey(cr => cr.UserId)
          //             .OnDelete(DeleteBehavior.Restrict);

          //  builder.Entity<CommentReply>()
          //         .HasOne(cr => cr.Article)
          //         .WithMany(a => a.CommentReplies)
          //         .HasForeignKey(cr => cr.ArticleId)
          //         .OnDelete(DeleteBehavior.Restrict);

          //  builder.Entity<CommentReply>()
          //         .HasOne(cr => cr.ArticleComment)
          //         .WithMany(ac => ac.CommentReplies)
          //         .HasForeignKey(cr => cr.ArticleCommentId)
          //         .OnDelete(DeleteBehavior.Restrict);

          //  builder.Entity<ArticleComment>()
          //       .HasOne(ac => ac.Article)
          //       .WithMany(a => a.ArticleComments)
          //       .HasForeignKey(ac => ac.ArticleId)
          //       .OnDelete(DeleteBehavior.Restrict);

          //  builder.Entity<ArticleComment>()
          //         .HasOne(ac => ac.User)
          //         .WithMany(u => u.ArticleComments)
          //         .HasForeignKey(ac => ac.UserId)
          //         .OnDelete(DeleteBehavior.Restrict);

          //builder.Entity<CommentReply>()
          //      .HasOne(b=>b.Article)
          //      .WithMany(b => b.CommentReplies)
          //      .HasForeignKey(c=>c.ArticleId)
          //      .OnDelete(DeleteBehavior.Restrict);


        }
    }
}

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
        public DbSet<ArticleCommentReply> ArticleCommentReplies { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ArticleCommentReply>()
        //        .HasRequired(acr => acr.ArticleComment)
        //        .WithMany(ac => ac.ArticleCommentReplies)
        //        .HasForeignKey(acr => acr.ArticleCommentId);

        //    modelBuilder.Entity<ArticleCommentReply>()
        //        .HasRequired(acr => acr.User)
        //        .WithMany(u => u.ArticleCommentReplies)
        //        .HasForeignKey(acr => acr.UserId);
        //}

    }
}

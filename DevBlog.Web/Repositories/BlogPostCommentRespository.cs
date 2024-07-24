using DevBlog.Web.Data;
using DevBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Web.Repositories
{
    public class BlogPostCommentRespository : IBlogPostCommentRespository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostCommentRespository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await bloggieDbContext.BlogPostComment.AddAsync(blogPostComment);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId)
             .ToListAsync();
        }
    }
}

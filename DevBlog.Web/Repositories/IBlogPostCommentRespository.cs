using DevBlog.Web.Models.Domain;

namespace DevBlog.Web.Repositories
{
    public interface IBlogPostCommentRespository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId);
    }
}

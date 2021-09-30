using Chatter.Core.ErrorCodes.V1;
using Chatter.Core.Exceptions;
using System;

namespace Chatter.Core.Entities
{
    public class Post
    {
        public int Id { get; private set; }
        public Guid UniqueId { get; private set; }

        public string Title { get; private set; }
        public string Content { get; private set; }

        public int AuthorId { get; private set; }
        public User Author { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected Post()
        {
            
        }

        public Post(string title, string content, User author)
        {
            SetTitle(title);
            SetContent(content);
            Author = author;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetTitle(string title)
        {

            if (string.IsNullOrEmpty(title))
                throw new DomainException(Error.IncorrectPostTitle, "Post title cannot be empty");

            Title = title;
            
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetContent(string content)
        {
            Content = content;

            UpdatedAt = DateTime.UtcNow;
        }


    }
}

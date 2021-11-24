using System;
using System.Collections.Generic;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;

namespace Checkers.Data.Repository.MSSqlImplementation;

internal class ForumRepository : Repository, IForumRepository
    {
        public bool CreatePost(Credential credential, PostCreationData post)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTitle(Credential credential, int postId, string title)
        {
            throw new NotImplementedException();
        }

        public bool UpdateContent(Credential credential, int postId, string content)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePicture(Credential credential, int postId, int imageId)
        {
            throw new NotImplementedException();
        }

        public bool DeletePost(Credential credential, int postId)
        {
            throw new NotImplementedException();
        }

        public Post GetPost(int postId)
        {
            throw new NotImplementedException();
        }

        public bool CommentPost(Credential credential, int postId, string comment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostInfo> GetPosts()
        {
            throw new NotImplementedException();
        }
    }

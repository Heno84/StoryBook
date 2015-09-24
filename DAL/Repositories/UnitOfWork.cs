using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private StoryBookEntities context = new StoryBookEntities();

        private IUserRepository userRepository;
        private IStoryRepository storyRepository;
        private IGroupRepository groupRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository();
                }
                return userRepository;
            }
        }

        public IStoryRepository StoryRepository
        {
            get
            {
                if (this.storyRepository == null)
                {
                    this.storyRepository = new StoryRepository();
                }
                return storyRepository;
            }
        }

        public IGroupRepository GroupRepository
        {
            get 
            {
                if (this.groupRepository==null)
                {
                    this.groupRepository = new GroupRepository();
                    
                }
                return groupRepository;
            }
        }

        public IAspNetUserRepository AspNetUserRepository
        {
            get { throw new NotImplementedException(); }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

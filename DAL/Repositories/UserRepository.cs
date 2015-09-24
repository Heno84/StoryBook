using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        StoryBookEntities dbSB;
        public UserRepository()
        {
            dbSB = new StoryBookEntities();
        }
        public void Add(string name)
        {
            //using (var dbSB = new StoryBookEntities())
            //{
            
                dbSB.Users.Add(new User { Name = name });
                dbSB.SaveChanges();
            //}

            
        }
    }
}

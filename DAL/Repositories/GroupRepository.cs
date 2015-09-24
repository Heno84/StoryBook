using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DataModels;
using DAL.DataContext;


namespace DAL.Repositories
{
    class GroupRepository : IGroupRepository
    {
       private StoryBookEntities _dbSB;
        public GroupRepository()
        {
            _dbSB = new StoryBookEntities();
        }
        public void Add(GroupModel groupModel)
        {
            
                Group group = new Group
                 {
                     Owner = _dbSB.Users.FirstOrDefault(u => u.Name == groupModel.OwnerName),
                     Description = groupModel.Description,
                     Name = groupModel.Name
                 };

                _dbSB.Groups.Add(group);
                _dbSB.SaveChanges();

                group.Users.Add(group.Owner);
                _dbSB.SaveChanges();
            
        }


        public void Delete(int ID)
        {
           
            
                Group group = _dbSB.Groups.FirstOrDefault(g => g.ID == ID);

                foreach (Story story in group.Stories)
                {
                    group.Stories.Remove(story);
                }

                foreach (User user in group.Users)
                {
                    group.Users.Remove(user);
                }

                _dbSB.Groups.Remove(group);

                _dbSB.SaveChanges();
           

        }

        public IEnumerable<GroupModel> GetAll()
        {
                List<GroupModel> groups = (from g in _dbSB.Groups
                                           select new GroupModel
                                           {
                                               ID = g.ID,
                                               Name = g.Name,
                                               Description = g.Description,
                                               OwnerName = g.Owner.Name,
                                               MembersCount = g.Users.Count,
                                               StoriesCount = g.Stories.Count,
                                               Members = g.Users.Select(u => u.Name)
                                           }).ToList();

                return groups;
           
        }

        public GroupModel GetByID(int ID)
        {
                GroupModel group = (from g in _dbSB.Groups
                                    where g.ID == ID
                                    select new GroupModel
                                    {
                                        ID = g.ID,
                                        Name = g.Name,
                                        Description = g.Description,
                                        OwnerName = g.Owner.Name,
                                        MembersCount = g.Users.Count,
                                        StoriesCount = g.Stories.Count
                                    }).SingleOrDefault();
                return group;
           
        }

        public void Join(int id, string username)
        {
           
                _dbSB.Groups.FirstOrDefault(g => g.ID == id).Users.Add(_dbSB.Users.FirstOrDefault(u => u.Name == username));

                _dbSB.SaveChanges();
            
        }


        public IEnumerable<GroupModel> GetByStoryID(int ID)
        {
                List<GroupModel> groups = (from g in _dbSB.Groups
                                           where g.Stories.Contains(_dbSB.Stories.FirstOrDefault(s => s.ID == ID))
                                           select new GroupModel
                                           {
                                               ID = g.ID,
                                               Name = g.Name,
                                               Description = g.Description,
                                               OwnerName = g.Owner.Name,
                                               MembersCount = g.Users.Count,
                                               StoriesCount = g.Stories.Count
                                           }).ToList();
                return groups;
            
        }


        public IEnumerable<GroupModel> GetByStoryIDInverted(int ID)
        {
            
                List<GroupModel> groups = (from g in _dbSB.Groups
                                           where !g.Stories.Contains(_dbSB.Stories.FirstOrDefault(s => s.ID == ID))
                                           select new GroupModel
                                           {
                                               ID = g.ID,
                                               Name = g.Name,
                                               Description = g.Description,
                                               OwnerName = g.Owner.Name,
                                               MembersCount = g.Users.Count,
                                               StoriesCount = g.Stories.Count
                                           }).ToList();
                return groups;
            
        }
    }
}

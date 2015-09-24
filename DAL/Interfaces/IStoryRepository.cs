using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;

namespace DAL.Interfaces
{
    public interface IStoryRepository
    {
        IEnumerable<StoryModel> GetAll();
        IEnumerable<StoryModel> GetByAuthor(string authorName);
        StoryModel GetByID(int ID);

        void Add(StoryModel storyModel);
        
        void Delete(int ID);

        void JoinToGroup(int groupID,int storyID);
        void ExitGroup(int groupID, int storyID);
    }
}

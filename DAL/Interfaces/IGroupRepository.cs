using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;


namespace DAL.Interfaces
{
    public interface IGroupRepository
    {
        IEnumerable<GroupModel> GetAll();
        GroupModel GetByID(int ID);
        void Add(GroupModel groupModel);
        void Delete(int ID);
        void Join(int id, string p);
        IEnumerable<GroupModel> GetByStoryID(int ID);
        IEnumerable<GroupModel> GetByStoryIDInverted(int ID);
    }
}

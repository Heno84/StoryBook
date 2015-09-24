using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Interfaces;
using DataModels;

namespace DAL.Repositories
{
    public class StoryRepository : IStoryRepository
    {

        private StoryBookEntities _dbSB;
        public StoryRepository()
        {
            _dbSB = new StoryBookEntities();
        }
        public void Add(StoryModel storyModel)
        {

            Story story = new Story
            {
                Author = _dbSB.Users.FirstOrDefault(u => u.Name == storyModel.AuthorName),
                Content = storyModel.Content,
                Description = storyModel.Description,
                Title = storyModel.Title,
                PostedOn = storyModel.PostedOn
            };

            _dbSB.Stories.Add(story);
            _dbSB.SaveChanges();

        }



        public IEnumerable<StoryModel> GetAll()
        {

            List<StoryModel> stories = (from st in _dbSB.Stories
                                        select new StoryModel
                                        {
                                            ID = st.ID,
                                            AuthorName = st.Author.Name,
                                            Content = st.Content,
                                            PostedOn = st.PostedOn,
                                            Title = st.Title,
                                            Description = st.Description,

                                        }).ToList();
            return stories;


        }

        public IEnumerable<StoryModel> GetByAuthor(string authorName)
        {

            List<StoryModel> stories = (from st in _dbSB.Stories
                                        where st.Author.Name == authorName
                                        select new StoryModel
                                        {
                                            ID = st.ID,
                                            AuthorName = st.Author.Name,
                                            Content = st.Content,
                                            PostedOn = st.PostedOn,
                                            Title = st.Title,
                                            Description = st.Description,

                                        }).ToList();
            return stories;

        }

        public StoryModel GetByID(int ID)
        {

            var story = (from st in _dbSB.Stories
                         where st.ID == ID
                         select
                         new StoryModel
                         {
                             ID = st.ID,
                             AuthorName = st.Author.Name,
                             Content = st.Content,
                             PostedOn = st.PostedOn,
                             Title = st.Title,
                             Description = st.Description,
                         }).SingleOrDefault();

            return story;

        }

        public void Delete(int ID)
        {

            Story story = _dbSB.Stories.FirstOrDefault(s => s.ID == ID);

            foreach (Group group in story.Groups)
            {
                group.Stories.Remove(story);
            }

            _dbSB.Stories.Remove(story);

            _dbSB.SaveChanges();

        }


        public void JoinToGroup(int groupID, int storyID)
        {

            _dbSB.Stories.FirstOrDefault(s => s.ID == storyID).Groups.Add(_dbSB.Groups.FirstOrDefault(g => g.ID == groupID));
            _dbSB.SaveChanges();


        }


        public void ExitGroup(int groupID, int storyID)
        {

            _dbSB.Stories.FirstOrDefault(s => s.ID == storyID).Groups.Remove(_dbSB.Groups.FirstOrDefault(g => g.ID == groupID));
            _dbSB.SaveChanges();

        }
    }
}

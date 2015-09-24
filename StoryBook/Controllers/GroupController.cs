using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfaces;
using DataModels;
using StoryBook.Helpers;

namespace StoryBook.Controllers
{
    public class GroupController : BaseController
    {
        public GroupController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ActionResult Groups()
        {
            IEnumerable<GroupModel> groups = _unitOfWork.GroupRepository.GetAll();

            foreach (GroupModel group in groups)
            {
                group.AmIOwnerOf = group.OwnerName == User.Identity.Name;
                group.ByLine = group.AmIOwnerOf ? "by YOU" : string.Format("by {0}", group.OwnerName);
                group.AmIMemberOf = group.Members.Contains(User.Identity.Name);
            }

            return View(groups);
        }

        public ActionResult Create()
        {
            return View(new GroupModel());
        }

        [HttpPost]
        public ActionResult Create(GroupModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.OwnerName = User.Identity.Name;

                    _unitOfWork.GroupRepository.Add(model);

                    return RedirectToAction("Groups");
                }
            }
            catch (Exception ex)
            {

                Logger.Log(LogType.error, ex.ToString());
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            return View(_unitOfWork.GroupRepository.GetByID(id));
        }

        [HttpPost]
        public ActionResult Delete(GroupModel model)
        {
            try
            {
                GroupModel group = _unitOfWork.GroupRepository.GetByID(model.ID);
                if (group.OwnerName == User.Identity.Name)
                {
                    _unitOfWork.GroupRepository.Delete(model.ID);

                }
            }
            catch (Exception ex)
            {

                Logger.Log(LogType.error, ex.ToString());
            }


            return RedirectToAction("Groups");
        }

        public ActionResult Join(int id)
        {
            try
            {
                GroupModel group = _unitOfWork.GroupRepository.GetByID(id);

                if (group.OwnerName == User.Identity.Name)
                {
                    _unitOfWork.GroupRepository.Join(id, User.Identity.Name);

                }
            }
            catch (Exception ex)
            {

                Logger.Log(LogType.error, ex.ToString());
            }

            return RedirectToAction("Groups");
        }

    }
}

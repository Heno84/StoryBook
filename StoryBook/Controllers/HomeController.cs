using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoryBook.Helpers;
using DAL;
using DAL.Interfaces;
using StoryBook.Models;
using DataModels;

namespace StoryBook.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ActionResult Index()
        {
            return View(_unitOfWork.StoryRepository.GetByAuthor(User.Identity.Name));
        }

        public ActionResult Create()
        {
            return View(new StoryModel());
        }

        [HttpPost]
        public ActionResult Create(StoryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.PostedOn = DateTime.Now;
                    model.AuthorName = User.Identity.Name;

                    _unitOfWork.StoryRepository.Add(model);

                    return RedirectToAction("Index");
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
            return View(_unitOfWork.StoryRepository.GetByID(id));
        }

        [HttpPost]
        public ActionResult Delete(StoryModel model)
        {
            try
            {
                _unitOfWork.StoryRepository.Delete(model.ID);

            }
            catch (Exception ex)
            {

                Logger.Log(LogType.error, ex.ToString());
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {

            StoryModel model = _unitOfWork.StoryRepository.GetByID(id);

            model.InGroups = _unitOfWork.GroupRepository.GetByStoryID(id);
            model.NotInGroups = _unitOfWork.GroupRepository.GetByStoryIDInverted(id);

            return View(model);


        }

        public ActionResult JoinToGroup(int? groupid, int? storyid)
        {
            try
            {
                if (groupid.HasValue && storyid.HasValue)
                {
                    _unitOfWork.StoryRepository.JoinToGroup(groupid.Value, storyid.Value);
                    return RedirectToAction("Details", new { id = storyid.Value });

                }
            }

            catch (Exception   ex)
            {
                Logger.Log(LogType.error, ex.ToString());
            }
            

            return RedirectToAction("Index");
        }

        public ActionResult ExitGroup(int? groupid, int? storyid)
        {
            try
            {
                if (groupid.HasValue && storyid.HasValue)
                {
                    _unitOfWork.StoryRepository.ExitGroup(groupid.Value, storyid.Value);
                    return RedirectToAction("Details", new { id = storyid.Value });
                }
            }

            catch (Exception ex)
            {

                Logger.Log(LogType.error, ex.ToString());
            }
            

            return RedirectToAction("Index");
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TouristPlace.Models;

namespace TouristPlace.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        #region Global Dashboard
        public ActionResult Index()
        {
            IList<ToristPlace> list = unitOfWork.ToristPlaceRepository.Get().ToList();
            return View(list);
        } 
        #endregion

        public ActionResult TouristPlaceList()
        {
            IList<ToristPlace> list = unitOfWork.ToristPlaceRepository.Get().ToList();
            return View(list);
        }

        public ActionResult TouristDashboard()
        {
            TouristRegistration obj = new TouristRegistration();
            if (Session["logUser"] != null)
            {
                obj = (TouristRegistration)Session["logUser"];
            }
            IList<ToristPlace> list = unitOfWork.ToristPlaceRepository.Get().ToList();
            return View(list);
        }

        #region Touristwise Place
        public ActionResult MyPlaces()
        {
            TouristRegistration obj = new TouristRegistration();
            if (Session["logUser"] != null)
            {
                obj = (TouristRegistration)Session["logUser"];
            }
            IList<ToristPlace> list = unitOfWork.ToristPlaceRepository.Get().Where(x => x.TouristRegistrationId == obj.TouristRegistrationId).ToList();
            return View(list);
        } 
        #endregion



        #region Login and Log Off
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            IList<TouristRegistration> list = unitOfWork.TouristRegistrationRepository.Get().Where(x => x.UserName == userName && x.Password == password).ToList();

            if (list.Count > 0)
            {
                Session["logUser"] = list.FirstOrDefault();
                return Json(new JsonResult() { Data = new { Status = "Success" } }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new JsonResult() { Data = new { Status = "Failed" } }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult LogOut()
        {
            Session["logUser"] = null;
            return RedirectToAction("Index");
        }
        #endregion





        #region Tourist Place Operation

        #region Add Tourist Place
        public ActionResult NewTouristPlace()
        {

            return View();
        }

        [HttpPost]
        public ActionResult NewTouristPlace(ToristPlace touristPlace)
        {
            TouristRegistration obj = new TouristRegistration();
            if (Session["logUser"] != null)
            {
                obj = (TouristRegistration)Session["logUser"];
            }
            touristPlace.TouristRegistrationId = obj.TouristRegistrationId;
            //create path to store in database
            touristPlace.ImageUrl = "/Content/TouristPlace/" + touristPlace.PlaceImage.FileName;
            //store image in folder
            touristPlace.PlaceImage.SaveAs(Server.MapPath("~/Content/TouristPlace") + "/" + touristPlace.PlaceImage.FileName);
            unitOfWork.ToristPlaceRepository.Insert(touristPlace);
            unitOfWork.Save();
            return RedirectToAction("MyPlaces");
        }
        #endregion

        #region Update Place

        public ActionResult EditTouristPlace(int id)
        {
            ToristPlace obj = unitOfWork.ToristPlaceRepository.Get().Where(x => x.ToristPlaceId == id).FirstOrDefault();
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditTouristPlace(ToristPlace touristPlace)
        {
            TouristRegistration obj = new TouristRegistration();
            if (Session["logUser"] != null)
            {
                obj = (TouristRegistration)Session["logUser"];
            }
            touristPlace.TouristRegistrationId = obj.TouristRegistrationId;
            //create path to store in database
            touristPlace.ImageUrl = "/Content/TouristPlace/" + touristPlace.PlaceImage.FileName;
            //store image in folder
            touristPlace.PlaceImage.SaveAs(Server.MapPath("~/Content/TouristPlace") + "/" + touristPlace.PlaceImage.FileName);
            unitOfWork.ToristPlaceRepository.Update(touristPlace);
            unitOfWork.Save();
            return RedirectToAction("MyPlaces");
        }
        #endregion

        #region Details Place
        public ActionResult ViewDetails(int id)
        {
            ToristPlace obj = unitOfWork.ToristPlaceRepository.Get().Where(x => x.ToristPlaceId == id).FirstOrDefault();
            return View(obj);

        }
        #endregion

        #region Delete Place
        public ActionResult DeleteTouristPlace(int id)
        {
            unitOfWork.ToristPlaceRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("MyPlaces");
        }
        [HttpPost]
        public ActionResult DeleteTouristPlace(ToristPlace touristPlace)
        {
            unitOfWork.ToristPlaceRepository.Delete(touristPlace.ToristPlaceId);
            unitOfWork.Save();
            return RedirectToAction("MyPlaces");
        }
        #endregion

        #endregion



        #region Comment Operation

        #region Get Comments
        public ActionResult GetCommentsByTouristPlaceId(int tId)
        {
            IList<TouristComment> list = unitOfWork.TouristCommentRepository.Get().Where(x => x.ToristPlaceId == tId).ToList();
            foreach (var item in list)
            {
                TouristRegistration obj = unitOfWork.TouristRegistrationRepository.GetByID(item.TouristRegistrationId);
                item.TouristName = obj.Name;
            }

            return Json(new JsonResult() { Data = new { Status = "Failed", data = list } }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Save Comments
        [HttpPost]
        public ActionResult SaveComments(TouristComment obj)
        {
            TouristRegistration reg = new TouristRegistration();
            if (Session["logUser"] != null)
            {
                reg = (TouristRegistration)Session["logUser"];
            }
            obj.TouristRegistrationId = reg.TouristRegistrationId;
            unitOfWork.TouristCommentRepository.Insert(obj);
            unitOfWork.Save();

            return Json(new JsonResult() { Data = new { Status = "Success" } }, JsonRequestBehavior.AllowGet);

        }
        #endregion 
        #endregion








        #region Registration Operation
        public ActionResult NewRegistration()
        {
            ViewBag.SuccessMessage = "";
            return View();
        }

        [HttpPost]
        public ActionResult NewRegistration(TouristRegistration obj)
        {
            unitOfWork.TouristRegistrationRepository.Insert(obj);
            unitOfWork.Save();
            ViewBag.SuccessMessage = "Registration has been done successfully! " + "User Name: " + obj.UserName + " and Password: " + obj.Password;
            ModelState.Clear();
            return View();
        }
        #endregion



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
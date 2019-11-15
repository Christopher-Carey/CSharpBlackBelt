using System.Collections.Generic;
using System.Linq;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers {
    [Route ("Belt")]
    public class BeltController : Controller {
        private MyContext dbContext;
        public BeltController (MyContext context) {
            dbContext = context;
        }
        // ---------------------------------------------
        [HttpGet ("Success")]
        public IActionResult Success () {
            if (HttpContext.Session.GetString ("Email") == null) 
            {
                return RedirectToAction ("Index");
            }
            User CurrentUser = dbContext.Users.FirstOrDefault (a => a.Email == HttpContext.Session.GetString ("Email"));
            List<Outing> AllOutings = dbContext.Outings.Include(a => a.OutingUsers).ThenInclude(b => b.User).Include(c => c.Creator).OrderByDescending(d =>d.OutingId).ToList();
            ViewBag.CurUser = CurrentUser;
            ViewBag.Name = HttpContext.Session.GetString ("Name");
            ViewBag.Email = HttpContext.Session.GetString ("Email");
            return View ("Success", AllOutings);
        }
        [HttpGet ("OutingInfoPage/{OutingId}")]
        public IActionResult OutingInfoPage (int OutingId) {
            if (HttpContext.Session.GetString ("Email") == null) {
                return RedirectToAction ("Index");
            }
            Outing CurrentOuting = dbContext.Outings.Include(a => a.Creator).Include(b => b.OutingUsers).FirstOrDefault(a => a.OutingId == OutingId);
            User CurrentUser = dbContext.Users.FirstOrDefault (a => a.Email == HttpContext.Session.GetString ("Email"));


            List<Outing> AllOutings = dbContext.Outings.Include(a => a.OutingUsers).ThenInclude(b => b.User).ToList();
            ViewBag.CurUser = CurrentUser;
            ViewBag.CurOuting = CurrentOuting;
            ViewBag.Name = HttpContext.Session.GetString ("Name");
            ViewBag.Email = HttpContext.Session.GetString ("Email");
            return View ("OutingInfoPage", CurrentOuting);
        }

        [HttpGet ("AddOutingPage")]
        public IActionResult AddOutingPage () {
            if (HttpContext.Session.GetString ("Email") == null) {
                return RedirectToAction ("Index", "Home");
            }
            User CurrentUser = dbContext.Users.FirstOrDefault (a => a.Email == HttpContext.Session.GetString ("Email"));

            ViewBag.CurUser = CurrentUser;
            ViewBag.Name = HttpContext.Session.GetString ("Name");
            ViewBag.Email = HttpContext.Session.GetString ("Email");
            return View ("AddOutingPage");
        }

        [HttpGet ("OutingAction/{userid}/{outingid}/{status}")]
        public IActionResult OutingAction (int userid,int outingid,string status)
        {
            if (HttpContext.Session.GetString ("Email") == null) {
                return RedirectToAction ("Index", "Home");
            }

            if(status == "join")
            {
                Association NewAssociation = new Association()
                    {
                        UserId = userid,
                        OutingId = outingid
                    };
                    dbContext.Add(NewAssociation);
                    dbContext.SaveChanges();
                    return RedirectToAction("Success");
            }
            if(status == "leave")
            {
                Association GetAssociation = dbContext.Associations.FirstOrDefault(w => w.OutingId == outingid);
                dbContext.Associations.Remove(GetAssociation);
                dbContext.SaveChanges();
                return RedirectToAction("Success");

            }
            if(status == "delete")
            {
                Outing GetOuting = dbContext.Outings.FirstOrDefault(w => w.OutingId == outingid);
                dbContext.Outings.Remove(GetOuting);
                dbContext.SaveChanges();
                return RedirectToAction("Success");
            }
            
            User CurrentUser = dbContext.Users.FirstOrDefault (a => a.Email == HttpContext.Session.GetString ("Email"));

            ViewBag.CurUser = CurrentUser;
            ViewBag.Name = HttpContext.Session.GetString ("Name");
            ViewBag.Email = HttpContext.Session.GetString ("Email");
            return View ("AddOutingPage");
        }

        [HttpPost ("AddOuting")]
        public IActionResult AddOuting (Outing NewOuting) {
            if (ModelState.IsValid) {

                dbContext.Outings.Add (NewOuting);
                dbContext.SaveChanges ();

                return RedirectToAction ("Success");
            }
            User CurrentUser = dbContext.Users.FirstOrDefault (a => a.Email == HttpContext.Session.GetString ("Email"));

            ViewBag.CurUser = CurrentUser;
            ViewBag.Name = HttpContext.Session.GetString ("Name");
            ViewBag.Email = HttpContext.Session.GetString ("Email");

            return View("AddOutingPage", NewOuting);
        }

    }
}
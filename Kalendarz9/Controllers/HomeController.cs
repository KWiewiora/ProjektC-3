using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kalendarz9.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public JsonResult GetMeetings()
        {
            using (MeetingsEntities dc = new MeetingsEntities())
            {
                var meetings = dc.Meetings.ToList();
                return new JsonResult { Data = meetings, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
        [HttpPost]
        public JsonResult SaveMeeting(Meeting m)
        {
            var status = false;
            using (MeetingsEntities dc = new MeetingsEntities())
            {
                var meetings = dc.Meetings.ToList();
                var lastID = meetings.Count;
                if (m.MeetingID > 0)
                {
                    

                    //aktualizowanie spotkania
                    var v = dc.Meetings.Where(a => a.MeetingID == m.MeetingID).FirstOrDefault();
                    if (v != null)
                    {
                        m.MeetingID = lastID + 1;
                        v.ClientName = m.ClientName;
                        v.MeetingType = m.MeetingType;
                        v.Start = m.Start;
                        v.End = m.End;
                        v.ThemeColor = m.ThemeColor;
                        v.IsFullDay = m.IsFullDay;
                    }

                }
                else
                {
                    m.MeetingID = lastID + 1;
                    dc.Meetings.Add(m);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult DeleteMeeting(int meetingID)
        {
            var status = false;
            using (MeetingsEntities dc = new MeetingsEntities())
            {
                var v = dc.Meetings.Where(a => a.MeetingID == meetingID).FirstOrDefault();    
                if (v != null)
                {
                    dc.Meetings.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using Common;
using Model.Custom;
using Model.Domain;
using NLog;
using Service;
using System.Linq;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class ExampleController : Controller
    {
        private readonly IStudentService _studentService = DependecyFactory.GetInstance<IStudentService>();
        private readonly ICourseService _courseService = DependecyFactory.GetInstance<ICourseService>();

        #region Students
        public ActionResult Index()
        {
            return View(_studentService.GetAll());
        }


        public ActionResult Crud(int id = 0)
        {
            if (id > 0)
            {
                ViewBag.Courses = _courseService.GetAllByUser(id);
            }

            return View(id == 0  ? new Student() : _studentService.Get(id));
        }

        [HttpPost]
        public ActionResult Crud(Student model)
        {
            if (ModelState.IsValid)
            {
                var rh = _studentService.InsertOrUpdate(model);
                if (rh.Response)
                {
                    return RedirectToAction("index");
                }
            }           

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _studentService.Delete(id);
            return RedirectToAction("index");
        }
        #endregion

        #region Courses
        public ActionResult Courses()
        {
            return View(_courseService.GetAll());
        }

        public ActionResult CourseCrud(int id = 0)
        {
            return View(id == 0 ? new Course() : _courseService.Get(id));
        }

        [HttpPost]
        public ActionResult CourseCrud(Course model)
        {
            if (ModelState.IsValid)
            {
                var rh = _courseService.InsertOrUpdate(model);
                if (rh.Response)
                {
                    return RedirectToAction("Courses");
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddOrRemoveCourse(StudentForCourseSuscribed model)
        {
            if (ModelState.IsValid)
            {
                var rh = _courseService.InsertOrRemoveCourse(new StudentPerCourse {
                    CourseId = model.CourseId,
                    StudentId = model.StudentId
                });
            }

            return Redirect("Crud/" + model.StudentId);
        }

        public ActionResult CourseDelete(int id)
        {
            _courseService.Delete(id);
            return RedirectToAction("Courses");
        }
        #endregion
    }
}
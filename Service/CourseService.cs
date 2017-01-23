using Common;
using Model.Auth;
using Model.Custom;
using Model.Domain;
using NLog;
using Persistence.DbContextScope;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public interface ICourseService
    {
        IEnumerable<CourseForGridView> GetAll();
        IEnumerable<StudentForCourseSuscribed> GetAllByUser(int studentId);
        Course Get(int id);
        ResponseHelper InsertOrUpdate(Course model);
        ResponseHelper Delete(int id);
        ResponseHelper InsertOrRemoveCourse(StudentPerCourse model);
    }

    public class CourseService : ICourseService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Course> _courseReppository;
        private readonly IRepository<Student> _studentReppository;
        private readonly IRepository<StudentPerCourse> _studentPerCourseReppository;

        public CourseService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Course> courseReppository,
            IRepository<Student> studentReppository,
            IRepository<StudentPerCourse> studentPerCourseReppository
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _courseReppository = courseReppository;
            _studentReppository = studentReppository;
            _studentPerCourseReppository = studentPerCourseReppository;
        }

        /// <summary>
        /// Get all courses that user doesn't have
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StudentForCourseSuscribed> GetAllByUser(int studentId)
        {
            var result = new List<StudentForCourseSuscribed>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var suscribedCourses = _studentPerCourseReppository.Find(
                        x => x.StudentId == studentId
                    ).AsQueryable();

                    result = _courseReppository.GetAll(x => x.StudentPerCourses)
                                               .OrderBy(x => x.Name)
                                               .Select(x => new StudentForCourseSuscribed {
                                                   CourseId = x.Id,
                                                   StudentId = studentId,
                                                   Name = x.Name,
                                                   Suscribed = suscribedCourses.Any(y => y.CourseId == x.Id)
                                               }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public ResponseHelper InsertOrRemoveCourse(StudentPerCourse model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var courseByStudent = _studentPerCourseReppository.Find(x =>
                        x.StudentId == model.StudentId
                        && x.CourseId == model.CourseId
                    ).SingleOrDefault();

                    if (courseByStudent == null)
                    {
                        model.SuscribedAt = DateTime.Now;
                        _studentPerCourseReppository.Insert(model);
                    }
                    else
                    {
                        _studentPerCourseReppository.Delete(courseByStudent);
                    }

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }

        public IEnumerable<CourseForGridView> GetAll()
        {
            var result = new List<CourseForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _courseReppository.GetAll(x => x.StudentPerCourses)
                        .Select(x => new CourseForGridView
                        {
                          Id = x.Id,
                          Name = x.Name,
                          Students = x.StudentPerCourses.Count()
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public Course Get(int id)
        {
            var result = new Course();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _courseReppository.SingleOrDefault(x => x.Id == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public ResponseHelper InsertOrUpdate(Course model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.Id == 0)
                    {
                        _courseReppository.Insert(model);
                    }
                    else
                    {
                        _courseReppository.Update(model);
                    }

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }

        public ResponseHelper Delete(int id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _courseReppository.SingleOrDefault(x => x.Id == id);
                    _courseReppository.Delete(model);

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }
    }
}

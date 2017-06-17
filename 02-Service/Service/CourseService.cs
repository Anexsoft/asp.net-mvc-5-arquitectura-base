using Common;
using Model.Custom;
using Model.Domain;
using NLog;
using Persistence.DbContextScope;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<StudentPerCourse> _studentPerCourseRepository;

        public CourseService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Course> courseRepository,
            IRepository<Student> studentRepository,
            IRepository<StudentPerCourse> studentPerCourseRepository
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _studentPerCourseRepository = studentPerCourseRepository;
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
                    var suscribedCourses = _studentPerCourseRepository.Find(
                        x => x.StudentId == studentId
                    ).AsQueryable();

                    result = _courseRepository.GetAll(x => x.StudentPerCourses)
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
                    var courseByStudent = _studentPerCourseRepository.Find(x =>
                        x.StudentId == model.StudentId
                        && x.CourseId == model.CourseId
                    ).SingleOrDefault();

                    if (courseByStudent == null)
                    {
                        model.SuscribedAt = DateTime.Now;
                        _studentPerCourseRepository.Insert(model);
                    }
                    else
                    {
                        _studentPerCourseRepository.Delete(courseByStudent);
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
                    result = _courseRepository.GetAll(x => x.StudentPerCourses)
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
                    result = _courseRepository.SingleOrDefault(x => x.Id == id);
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
                        _courseRepository.Insert(model);
                    }
                    else
                    {
                        _courseRepository.Update(model);
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
                    var model = _courseRepository.SingleOrDefault(x => x.Id == id);
                    _courseRepository.Delete(model);

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

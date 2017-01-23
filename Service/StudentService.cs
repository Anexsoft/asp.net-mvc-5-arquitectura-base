﻿using Common;
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
    public interface IStudentService
    {
        IEnumerable<StudentForGridView> GetAll();
        Student Get(int id);
        ResponseHelper InsertOrUpdate(Student model);
        ResponseHelper Delete(int id);
    }

    public class StudentService : IStudentService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Student> _studentRepository;

        public StudentService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Student> studentRepository
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _studentRepository = studentRepository;
        }

        public IEnumerable<StudentForGridView> GetAll()
        {
            var result = new List<StudentForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _studentRepository.GetAll(x => x.StudentPerCourses)
                        .Select(x => new StudentForGridView
                        {
                          Id = x.Id,
                          Name = x.Name,
                          Email = x.Email,
                          Birthday = x.Birthday,
                          CurrentStatus = x.CurrentStatus == Enums.Status.Enable ? "Activo" : "Inactivo",
                          NumberOfCourses = x.StudentPerCourses.Count()
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public Student Get(int id)
        {
            var result = new Student();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _studentRepository.SingleOrDefault(x => x.Id == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public ResponseHelper InsertOrUpdate(Student model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.Id == 0)
                    {
                        _studentRepository.Insert(model);
                    }
                    else
                    {
                        _studentRepository.Update(model);
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
                    var model = _studentRepository.SingleOrDefault(x => x.Id == id);
                    _studentRepository.Delete(model);

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

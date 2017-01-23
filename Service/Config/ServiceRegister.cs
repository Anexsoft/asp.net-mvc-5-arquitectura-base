﻿using LightInject;
using Model.Domain;
using Persistence.DbContextScope;
using Persistence.Repository;

namespace Service.Config
{
    public class ServiceRegister : ICompositionRoot
    {               
        public void Compose(IServiceRegistry container)
        {
            var ambientDbContextLocator = new AmbientDbContextLocator();

            container.Register<IDbContextScopeFactory>((x) => new DbContextScopeFactory(null));

            container.Register<IRepository<Student>>((x) => new Repository<Student>(ambientDbContextLocator));
            container.Register<IRepository<Course>>((x) => new Repository<Course>(ambientDbContextLocator));
            container.Register<IRepository<StudentPerCourse>>((x) => new Repository<StudentPerCourse>(ambientDbContextLocator));

            container.Register<IStudentService, StudentService>();
            container.Register<IStudentPerCourseService, StudentPerCourseService>();
            container.Register<ICourseService, CourseService>();
        }
    }
}

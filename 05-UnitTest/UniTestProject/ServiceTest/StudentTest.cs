using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace UniTestProject.ServiceTest
{
    [TestClass]
    public class StudentTest
    {
        private readonly IStudentService _studentService = DependecyFactory.GetInstance<IStudentService>();

        [TestMethod]
        public void CanGetStudent()
        {
            var student = _studentService.Get(4);

            Assert.IsTrue(
                student != null
            );
        }
    }
}

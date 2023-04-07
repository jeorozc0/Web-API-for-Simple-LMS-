using Microsoft.AspNetCore.Mvc;
using WebApi2;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        DomainContext db = new DomainContext();
        

        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetAllCourses()
        {
            db.Courses.RemoveRange(db.Courses);
            db.Add(new Course{ Name = "CS420", ID = 2});
            db.Add(new Course{ Name = "CS416", ID = 1});
            db.SaveChanges();
            var course = db.Courses.OrderBy(b => b.ID);
            return Ok(course);
            
        }

        [HttpGet("{id:int}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            db.Courses.RemoveRange(db.Courses);
            db.Add(new Course{ Name = "CS420", ID = 2});
            db.Add(new Course{ Name = "CS416", ID = 1});
            db.SaveChanges();
            var course = db.Courses.First(c => c.ID == id);
            return Ok(course);
        }

        [HttpPost]
        public ActionResult<Course> CreateCourse(Course course)
        {
            db.Add(course);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateCourse(int id, Course course)
        {
            var existingCourse = db.Courses.Find(id);

            if (existingCourse == null)
            {
                return NotFound();
            }

            existingCourse.Name = course.Name;
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteCourse(int id)
        {
            var courseToRemove = db.Courses.Find(id);

            if (courseToRemove == null)
            {
                return NotFound();
            }

           db.Courses.Remove(courseToRemove);
           db.SaveChanges();

            return Ok();
        }
    }
}
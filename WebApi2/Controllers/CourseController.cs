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
            db.Add(new Course{ Name = "CS420", ID = 1});
            db.SaveChanges();
            var course = db.Courses.OrderBy(b => b.ID).First();
            return Ok(course);
            
        }

        // [HttpGet("{courseId}")]
        // public ActionResult<Course> GetCourseById(int id)
        // {
        //     var course = _courses.Find(c => c.ID == id);

        //     if (course == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(course);
        // }

        // [HttpPost]
        // public ActionResult<Course> CreateCourse(Course course)
        // {
        //     _courses.Add(course);

        //     return CreatedAtAction(nameof(GetCourseById), new { id = course.ID }, course);
        // }

        // [HttpPut("{courseId}")]
        // public ActionResult UpdateCourse(int id, Course course)
        // {
        //     var existingCourse = _courses.Find(c => c.ID == id);

        //     if (existingCourse == null)
        //     {
        //         return NotFound();
        //     }

        //     existingCourse.Name = course.Name;

        //     return NoContent();
        // }

        // [HttpDelete("{courseId}")]
        // public ActionResult DeleteCourse(int id)
        // {
        //     var courseToRemove = _courses.Find(c => c.ID == id);

        //     if (courseToRemove == null)
        //     {
        //         return NotFound();
        //     }

        //     _courses.Remove(courseToRemove);

        //     return NoContent();
        // }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
public class DomainContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public string DbPath { get; }
    public DomainContext()
    {
        DbPath = System.IO.Path.Join("./", "lms.db");
    }

    public DomainContext(DbContextOptions options) : base(options)
    {
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite($"Data Source={DbPath}");
}
public class Course
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public List<Module>? Modules { get;} = new();
}
public class Module
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public int CourseID { get; set; }
    public Course? Course { get; set; }
    public List<Assignment>? Assignments { get; set; }

}
public class Assignment
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public int Grade { get; set; }
    public DateTime DueDate { get; set; }
    public int? ModuleID { get; set; }
    public Module? Module { get; set; }

}
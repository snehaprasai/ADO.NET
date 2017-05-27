using Leapfrog.Academy.ADOExample.DBUtil;
using Leapfrog.Academy.ADOExample.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Leapfrog.Academy.ADOExample.Repository
{
    public interface ICourseRepository
    {
        List<Course> GetAll();
        int Insert(Course course);
        int Update(Course course);
        Course Find(int id);
        int Delete(int id);
    }
    public class CourseRepository : ICourseRepository
    {
        private DbConnection db = new DbConnection();

        public Course Find(int id)
        {
            Course course = null;
            db.Open();
            string sql = "SELECT *FROM Courses WHERE Id=@Id ";
            db.InitCommand(sql, CommandType.Text);
            db.AddInputParameter(DbType.Int32, "Id", id);
            SqlDataReader reader = db.ExecuteReader();

            if (reader.Read())
            {
                course = MapData(reader);
            }
            db.Close();
            return course;
        }
        private Course MapData(SqlDataReader reader)
        {
            Course course = new Course();
            course.Id = Convert.ToInt32(reader["Id"]);
            course.Name = reader["Name"].ToString();
            course.Fees = Convert.ToInt32(reader["Fees"]);
            course.AddedDate = Convert.ToDateTime(reader["AddedDate"]);
            course.Status = Convert.ToBoolean(reader["status"]);
            return course;
        }

        public List<Course> GetAll()
        {
            List<Course> courses = new List<Course>();
            db.Open();
            string sql = "SELECT *FROM Courses";
            db.InitCommand(sql, CommandType.Text);
            SqlDataReader reader = db.ExecuteReader();

            while (reader.Read())
            {
                 Course course =course= MapData(reader);
                courses.Add(course);
            }
            db.Close();
            return courses;
        }

        public int Insert(Course course)
        {
            db.Open();
            string sql = "INSERT INTO Courses(Name,Fees,Status) VALUES(@Name,@Fees,@Status)";
            db.InitCommand(sql, CommandType.Text);
            db.AddInputParameter(DbType.String, "Name", course.Name);
            db.AddInputParameter(DbType.Int32, "Fees", course.Fees);
            db.AddInputParameter(DbType.Boolean, "Status", course.Status);
            int result = db.ExecuteNonQuery();
            db.Close();
            return result;

        }

        public int Update(Course course)
        {
            db.Open();
            string sql = "UPDATE Courses SET Name=@Name,Fees=@Fees,Status=@Status WHERE Id=@Id ";
            db.InitCommand(sql, CommandType.Text);
            db.AddInputParameter(DbType.Int32, "Id", course.Id);
            db.AddInputParameter(DbType.String, "Name", course.Name);
            db.AddInputParameter(DbType.Int32, "Fees", course.Fees);
            db.AddInputParameter(DbType.Boolean, "Status", course.Status);
            int result = db.ExecuteNonQuery();
            db.Close();
            return result;
        }

        public int Delete(int id)
        {
            db.Open();
            string sql = "DELETE FROM Courses WHERE Id=@Id ";
            db.InitCommand(sql, CommandType.Text);
            db.AddInputParameter(DbType.Int32, "Id", id);
            
            int result = db.ExecuteNonQuery();
            db.Close();
            return result;
        }
    }
}
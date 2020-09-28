using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections.Generic;

public class DatabaseController : MonoBehaviour
{
    IDbConnection dbcon;
    string connection;
    public DatabaseController()
    {
        connection = "URI=file: /Assets/ChooChooChicks.db";
        OpenSql();
    }
    public void OpenSql()
    {
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
     
    }

    public List<object> getNumbers() {
   
        List<object> numbers = new List<object>();
        string query = "SELECT singleDigitFirst FROM Questions";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                numbers.Add(reader.GetValue(i));
            }
        }

        return numbers;
    }

    public List<object> getStudents() {

        List<object> students = new List<object>();
        string query = "SELECT fname, lname FROM Student";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                students.Add(reader.GetValue(i));
            }
        }

        return students;
    }

    public List<object> getTeachers()
    {
        List<object> teachers = new List<object>();
        string query = "SELECT fname, lname FROM Teacher";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                teachers.Add(reader.GetValue(i));
            }
        }
 
        return teachers;
    }

    public object getTotalScore(string name) {

        object totalScore = 0;
        string[] temp = name.Split(null);
        string fname = temp[0];
        string lname = temp[1];
        string query = "SELECT totalScore FROM Student WHERE fname LIKE '%" + fname + "%' AND lname LIKE '%" + lname + "%'";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            totalScore = reader["totalScore"];
        }
  
        return totalScore;
    }


    public object updateScore(int points, string name) {

        object newScore = 0;
        string[] temp = name.Split(null);
        string fname = temp[0];
        string lname = temp[1];
        string query = "Update Student SET totalScore = totalScore + " + points + " WHERE fname LIKE '%" + fname + "%' AND lname LIKE '%" + lname + "%'";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        newScore = getTotalScore(name);
   
        return newScore;
    }

    public object getLevel(string name) {

        object level = -1;
        string[] temp = name.Split(null);
        string fname = temp[0];
        string lname = temp[1];
        string query = "SELECT levelId FROM Student WHERE fname LIKE '%" + fname + "%' AND lname LIKE '%" + lname + "%'";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            level = reader["levelId"];
        }

        return level;
    }

    public void updateLevel(string name)
    {

        object newScore = 0;
        string[] temp = name.Split(null);
        string fname = temp[0];
        string lname = temp[1];
        string query = "Update Student SET levelId = levelId + 1 WHERE fname LIKE '%" + fname + "%' AND lname LIKE '%" + lname + "%'";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

    }

    public void setLevel(int level, string name)
    {

        object newScore = 0;
        string[] temp = name.Split(null);
        string fname = temp[0];
        string lname = temp[1];
        string query = "Update Student SET levelId = " + level + " WHERE fname LIKE '%" + fname + "%' AND lname LIKE '%" + lname + "%'";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

    }

    public List<object> getClassroomStudents(string teacherName) {
        List<object> classroomStudents = new List<object>();
        string[] temp = teacherName.Split(null);
        string fname = temp[0];
        string lname = temp[1];
        string query = "SELECT id FROM Teacher WHERE fname LIKE '%" + fname + "%' AND lname LIKE '%" + lname + "%'";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        object id = 0;
        while (reader.Read())
        {
            id = reader["id"];
        }

        query = @"SELECT Student.fname, Student.lname 
        FROM Student
        INNER JOIN Classroom on Classroom.studentId = Student.id
        WHERE Classroom.teacherId = "+ id;
        cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                classroomStudents.Add(reader.GetValue(i));
            }
        }
        return classroomStudents;
    }

    public void addStudent(string fname, string lname, int totalScore, int levelId, string teacherName) {
        //insert student into student table
        string query = "INSERT INTO Student (fname, lname, totalScore, levelId) VALUES  ('"+fname+"','"+lname+"',"+totalScore+","+levelId+")";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = query;
        cmnd_read.ExecuteReader();
        //get teacher id
        string[] temp = teacherName.Split(null);
        string tFname = temp[0];
        string tLname = temp[1];
        query = "SELECT id FROM Teacher WHERE fname LIKE '%" + tFname + "%' AND lname LIKE '%" + tLname + "%'";
        cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        object teacherId = 0;
        while (reader.Read())
        {
            teacherId = reader["id"];
        }
        //get student id
        query = "SELECT id FROM Student WHERE fname LIKE '%" + fname + "%' AND lname LIKE '%" + lname + "%'";
        cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        object studentId = 0;
        while (reader.Read())
        {
            studentId = reader["id"];
        }
        //instert student and teacher id into classroom
        query = "INSERT INTO Classroom (teacherId, studentId) Values (" + teacherId + ", " + studentId + ")";
        cmnd_read = dbcon.CreateCommand();
        cmnd_read.CommandText = query;
        cmnd_read.ExecuteReader();
    }

    public void addAnimal(string name) {
        string[] temp = name.Split(null);
        string fname = temp[0];
        string lname = temp[1];
        string query = "Update Student SET animalCount = animalCount + 1 WHERE fname LIKE '%" + fname + "%' AND lname LIKE '%" + lname + "%'";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
    }

    public object getAnimalCount(string name) {
        object animalCount = 0;
        string[] temp = name.Split(null);
        string fname = temp[0];
        string lname = temp[1];
        string query = "SELECT animalCount FROM Student WHERE fname LIKE '%" + fname + "%' AND lname LIKE '%" + lname + "%'";
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        while (reader.Read())
        {
            animalCount = reader["animalCount"];
        }

        return animalCount;
    }
}

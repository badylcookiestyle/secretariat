using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
namespace secretary.dbHelper
{
   public static class DbHelper
    {
        static SQLiteConnection sqlConnection;
        static SQLiteCommand sqlQuery;
        static SQLiteDataAdapter dbAdapter;
        static DataSet dataSet = new DataSet();
        static DataTable dataTable = new DataTable();

        public static void connectToDb()
        {
            sqlConnection = new SQLiteConnection("Data Source=db.db;Version=3;New=False");
        }
        
        public static void sendQuery(string queryString)
        {
            connectToDb();

            sqlConnection.Open();
            sqlQuery = sqlConnection.CreateCommand();
            sqlQuery.CommandText = queryString;
            sqlQuery.ExecuteNonQuery();

            

            sqlConnection.Close();
        }

        public static DataTable reloadDbData()
        {
            connectToDb();

            sqlConnection.Open();
            sqlQuery = sqlConnection.CreateCommand();
            dbAdapter = new SQLiteDataAdapter("SELECT * FROM Persons", sqlConnection);
            dataSet.Reset();
            dbAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];

            sqlConnection.Close();
            return dataTable;
        }

        public static void insertStudent(Student newStudent)
        {
            string newStudentString = "INSERT INTO Persons(type,firstName,secondName,lastName,maidenName,fathersName,mothersName,birthDate,pesel,imagePath,gender,jobPosition,jobDesc,dateOfEmployment,currentClass,groups) " +
                "VALUES('s','"+newStudent.firstName+ "','" + newStudent.secondName+ "','" + newStudent.lastname+ "','" + newStudent.maidenName+ "','" + newStudent.fathersName+ "','" + newStudent.mothersName+ "','" + newStudent.birthDate.ToString()+ "','" + newStudent.pesel+ "','" + newStudent.imagePath+ "','g','jb','jd','d','cc','g');";

            sendQuery(newStudentString);
        }

        public static void insertTeacher(Teacher newTeacher)
        {

            string newTeacherString = "INSERT INTO Persons(type,firstName,secondName,lastName,maidenName,fathersName,mothersName,birthDate,pesel,imagePath,gender,jobPosition,jobDesc,dateOfEmployment,currentClass,groups) " +
                "VALUES('s','" + newTeacher.firstName + "','" + newTeacher.secondName + "','" + newTeacher.lastname + "','" + newTeacher.maidenName + "','" + newTeacher.fathersName + "','" + newTeacher.mothersName + "','" + newTeacher.birthDate.ToString() + "','" + newTeacher.pesel + "','" + newTeacher.imagePath + "','g','jb','" + newTeacher.dateOfEmployment + "','d','cc','g');";

            sendQuery(newTeacherString);
        }

        public static void insertEmployee(Employee newEmployee)
        {

            string newEmployeeString = "INSERT INTO Persons(type,firstName,secondName,lastName,maidenName,fathersName,mothersName,birthDate,pesel,imagePath,gender,jobPosition,jobDesc,dateOfEmployment,currentClass,groups) " +
                "VALUES('s','" + newEmployee.firstName + "','" + newEmployee.secondName + "','" + newEmployee.lastname + "','" + newEmployee.maidenName + "','" + newEmployee.fathersName + "','" + newEmployee.mothersName + "','" + newEmployee.birthDate.ToString() + "','" + newEmployee.pesel + "','" + newEmployee.imagePath + "','g','jb','" + newEmployee.dateOfEmployment + "','d','cc','g');";

            sendQuery(newEmployeeString);
        }
    }
}

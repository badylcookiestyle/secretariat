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
     
        public static DataTable basicSelect(string curTable)
        {
            connectToDb();

            sqlConnection.Open();
            sqlQuery = sqlConnection.CreateCommand();
            
            dbAdapter = new SQLiteDataAdapter("SELECT * FROM "+curTable, sqlConnection);
            dataSet.Reset();
            dbAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];

            sqlConnection.Close();

            return dataTable;
        }
        public static DataTable likeSelect(string curTable,string lText,string cField)
        {
            connectToDb();

            sqlConnection.Open();
            sqlQuery = sqlConnection.CreateCommand();

            dbAdapter = new SQLiteDataAdapter("SELECT * FROM " + curTable+" WHERE "+cField+" LIKE '%"+lText+"%' ;", sqlConnection);
            dataSet.Reset();
            dbAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];

            sqlConnection.Close();

            return dataTable;
        }
        public static DataTable idSelect(string curTable, string lText)
        {
            connectToDb();

            sqlConnection.Open();
            sqlQuery = sqlConnection.CreateCommand();

            dbAdapter = new SQLiteDataAdapter("SELECT * FROM " + curTable +"WHERE id ='"+lText+"';", sqlConnection);
            dataSet.Reset();
            dbAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];

            sqlConnection.Close();

            return dataTable;
        }
        public static void insertStudent(Student newStudent)
        {
      
            string newStudentString = "INSERT INTO students(first_name,second_name,last_name,maiden_name,fathers_name,mothers_name,birth_date,pesel,image_path,gender,current_class,groups) VALUES('" + newStudent.firstName + "','" + newStudent.secondName + "','" + newStudent.lastname + "','" + newStudent.maidenName + "','" + newStudent.fathersName + "','" + newStudent.mothersName + "','"+newStudent.birthDate.ToString()+"','" + newStudent.pesel + "','" + newStudent.imagePath + "','" + newStudent.gender + "','" + newStudent.currentClass + "','" + newStudent.groups + "');";

            sendQuery(newStudentString);
        }

        public static void insertTeacher(Teacher newTeacher)
        {
            string newTeacherString = "INSERT INTO teachers(first_name,second_name,last_name,maiden_name,fathers_name,mothers_name,birth_date,pesel,image_path,gender,date_of_employment,class_tutor,taught_subjects) VALUES('" + newTeacher.firstName + "','" + newTeacher.secondName + "','" + newTeacher.lastname + "','" + newTeacher.maidenName + "','" + newTeacher.fathersName + "','" + newTeacher.mothersName + "','" + newTeacher.birthDate.ToString() + "','" + newTeacher.pesel + "','" + newTeacher.imagePath + "','" + newTeacher.gender + "','" + newTeacher.dateOfEmployment + "','"+newTeacher.classTutor+"','"+newTeacher.taughtSubjects+"');";
            sendQuery(newTeacherString);
        }

        public static void insertEmployee(Employee newEmployee)
        {

            string newEmployeeString = "INSERT INTO employees(first_name,second_name,last_name,maiden_name,fathers_name,mothers_name,birth_date,pesel,image_path,gender,date_of_employment,job_description,job_position,tenure) VALUES('" + newEmployee.firstName + "','" + newEmployee.secondName + "','" + newEmployee.lastname + "','" + newEmployee.maidenName + "','" + newEmployee.fathersName + "','" + newEmployee.mothersName + "','" + newEmployee.birthDate.ToString() + "','" + newEmployee.pesel + "','" + newEmployee.imagePath + "','" + newEmployee.gender + "','" + newEmployee.dateOfEmployment + "','"+newEmployee.jobDescription+"','"+newEmployee.jobPosition+"','tenure');";

            sendQuery(newEmployeeString);
        }
        public static void basicDelete(string tableName)
        {
            string queryString = "DELETE FROM "+tableName+";";
            sendQuery(queryString);
        }
        public static string convertTableToString(DataTable table) {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }
        public static string dumpDbToJson()
        {
            string jsonDump;
            DataTable teachers = basicSelect("teachers");
            DataTable students = basicSelect("students");
            DataTable employees = basicSelect("employees");

            // jsonDump = convertTableToString(teachers);
           // jsonDump = convertTableToString(students);
            //jsonDump += convertTableToString(employees);
            jsonDump = convertTableToString(teachers);
            return jsonDump;
        }
    }
}

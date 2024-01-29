using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace DataSetADO.Model
{

    public interface IStudents
    {
        List<StudentDTO> GetData();
    }
    public class Students: IStudents
    {
       
        private readonly IConfiguration _configuration;
        private readonly string connectionstring;
        public Students(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionstring = configuration["ConnectionStrings:DefaultConnection"];
        }


        public List<StudentDTO> GetData()
        {
           
            using (SqlConnection connect =  new SqlConnection(connectionstring)) 
            {
                connect.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM students_list", connect))
                {
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "students");
                    List<StudentDTO> student_data = new List<StudentDTO>();
                    foreach (DataRow row in dataSet.Tables["students"].Rows)
                    {
                        StudentDTO student = new StudentDTO
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Name = row["Name"].ToString(),
                            Age = Convert.ToInt32(row["Age"])
                        };

                        student_data.Add(student);
                    }
                    return student_data;
                }
            }
            
        }



        public void deleteData(int id)
        {
            using(SqlConnection connect = new SqlConnection(connectionstring))
            {
                connect.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM students_list", connect))
                {
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "students");
                    DataRow rowToDelete = dataSet.Tables["Students"].Rows.Find(id);
                    if(rowToDelete != null)
                    {
                        rowToDelete.Delete();
                        dataAdapter.Update(dataSet, "Students");
                    }
                }
            }
        }




    }
}

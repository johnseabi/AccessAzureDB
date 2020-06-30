using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data.SqlClient;
using Android.Widget;

namespace AccessAzureDB
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        static List<PoorPerformingStudent> listOfStudents = new List<PoorPerformingStudent>();

        public MainPage()
        {
            InitializeComponent();

            listOfStudents = Getstudents();
            //Sample code
            list.FindByName<Xamarin.Forms.ListView>("list");

            list.ItemsSource = listOfStudents;
        }

        partial class PoorPerformingStudent
        {
            public PoorPerformingStudent(string studentNumber, string fullName, string module, string campus, string phone, string contactMode)
            {
                StudentNumber = studentNumber;
                FullName = fullName;
                Module = module;
                Campus = campus;
                Phone = phone;
                ContactMode = contactMode;
            }

            public string StudentNumber { get; set; }
            public string FullName { get; set; }
            public string Module { get; set; }
            public string Campus { get; set; }
            public string Phone { get; set; }
            public string ContactMode { get; set; }
        }
        static List<PoorPerformingStudent> Getstudents()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "prj381server.database.windows.net";
                builder.UserID = "john";
                builder.Password = "StudentAnalyser123";
                builder.InitialCatalog = "TestDB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT TOP (1000) [StudentNumber],[FullNames],[Modules],[Campus] ,[ContactNumber],[ContactMode]FROM[dbo].[PoorPerformingStudent]");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listOfStudents.Add(new PoorPerformingStudent(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {

            }
            return listOfStudents;
        }
    }
}

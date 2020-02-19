using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.IO;

namespace Trafficking_Intervention_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonyController : ControllerBase
    {
        // GET api/Testimony
        [HttpGet]
        public List<TestimonyEntity> GetTestimonies() {

            // Testimony will be populated with the result of the query.
            List<TestimonyEntity> Testimony = new List<TestimonyEntity>();

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                // create a string to hold the SQL command.
                string sql = $"select * from testimonies;";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    
                    // Reader allows you to read each value that comes back from the query and do something to it.
                    using(SqliteDataReader reader = command.ExecuteReader()) {
                        
                        // Loop through query exit when no more objects are left.
                        while (reader.Read()) {

                            // map the data to the Testimonys model.
                            TestimonyEntity newTestimony = new TestimonyEntity() {
                                AppUserID = reader.GetInt32(0),
                                firstName = reader.GetString(1),
                                lastName = reader.GetString(2),
                                testimony = reader.GetString(3),
                                date = reader.GetString(4),
                                site = reader.GetString(5)
                            };

                            // Add one to the list.
                            Testimony.Add(newTestimony);
                        }
                    }
                }
                // close the connection
                conn.Close();
            }
            return Testimony;
        }

        // // GET api/Testimony/user
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // POST api/Testimony
        [HttpPost]
        public void PostTestimony([FromBody] TestimonyEntity postTestimony)
        {

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"insert into testimonies (FirstName, LastName, Testimony, Date, Sites) values (\"{postTestimony.firstName}\", \"{postTestimony.lastName}\", \"{postTestimony.testimony}\", \"{postTestimony.date}\", \"{postTestimony.site}\");";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    command.ExecuteNonQuery();
                }
                // close the connection
                conn.Close();
            }
            return;           
        }

        // PUT api/Testimony/"named-put"
        [HttpPut]
        public void Put([FromBody] TestimonyEntity putTestimony)
        {

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"update testimonies set FirstName = \"{putTestimony.firstName}\", LastName = \"{putTestimony.lastName}\", Testimony = \"{putTestimony.testimony}\", Date = \"{putTestimony.date}\", Sites = \"{putTestimony.site}\"  where LastName = \"{putTestimony.lastName}\";";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    command.ExecuteNonQuery();
                }
                // close the connection
                conn.Close();
            }
            return;
        }

        // DELETE api/Testimony/"named-delete"
        [HttpDelete]
        public void Delete([FromBody] TestimonyEntity dropTestimony)
        {
            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"delete from testimonies where FirstName = \"{dropTestimony.firstName}\" and LastName = \"{dropTestimony.lastName}\";";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    command.ExecuteNonQuery();
                }
                // close the connection
                conn.Close();
            }
            return;
        }
    }
}

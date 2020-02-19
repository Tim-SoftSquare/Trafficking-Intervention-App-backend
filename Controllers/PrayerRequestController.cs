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
    public class PrayerRequestController : ControllerBase
    {
        // GET api/PrayerRequest
        [HttpGet]
        public List<PrayerRequestEntity> GetPrayerRequests() {

            // PrayerRequest will be populated with the result of the query.
            List<PrayerRequestEntity> prayerRequest = new List<PrayerRequestEntity>();

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                // create a string to hold the SQL command.
                string sql = $"select * from prayer_requests;";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    
                    // Reader allows you to read each value that comes back from the query and do something to it.
                    using(SqliteDataReader reader = command.ExecuteReader()) {
                        
                        // Loop through query exit when no more objects are left.
                        while (reader.Read()) {

                            // map the data to the prayerRequests model.
                            PrayerRequestEntity newPrayerRequest = new PrayerRequestEntity() {
                                AppUserID = reader.GetInt32(0),
                                firstName = reader.GetString(1),
                                lastName = reader.GetString(2),
                                prayer = reader.GetString(3),
                                date = reader.GetString(4),
                                site = reader.GetString(5)
                            };

                            // Add one to the list.
                            prayerRequest.Add(newPrayerRequest);
                        }
                    }
                }
                // close the connection
                conn.Close();
            }
            return prayerRequest;
        }

        // // GET api/PrayerRequest/user
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // POST api/PrayerRequest
        [HttpPost]
        public void PostPrayerRequest([FromBody] PrayerRequestEntity postPrayerRequest)
        {

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"insert into prayer_requests (FirstName, LastName, PrayerRequest, Date, Sites) values (\"{postPrayerRequest.firstName}\", \"{postPrayerRequest.lastName}\", \"{postPrayerRequest.prayer}\", \"{postPrayerRequest.date}\", \"{postPrayerRequest.site}\");";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    command.ExecuteNonQuery();
                }
                // close the connection
                conn.Close();
            }
            return;           
        }

        // PUT api/PrayerRequest/"named-put"
        [HttpPut]
        public void Put([FromBody] PrayerRequestEntity putPrayerRequest)
        {

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"update prayer_requests set FirstName = \"{putPrayerRequest.firstName}\", LastName = \"{putPrayerRequest.lastName}\", PrayerRequest = \"{putPrayerRequest.prayer}\", Date = \"{putPrayerRequest.date}\", Sites = \"{putPrayerRequest.site}\"  where LastName = \"{putPrayerRequest.lastName}\";";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    command.ExecuteNonQuery();
                }
                // close the connection
                conn.Close();
            }
            return;
        }

        // DELETE api/PrayerRequest/"named-delete"
        [HttpDelete]
        public void Delete([FromBody] PrayerRequestEntity dropPrayerRequest)
        {
            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"delete from prayer_requests where FirstName = \"{dropPrayerRequest.firstName}\" and LastName = \"{dropPrayerRequest.lastName}\";";

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

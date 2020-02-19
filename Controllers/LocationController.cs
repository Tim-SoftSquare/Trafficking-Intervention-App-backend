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
    public class LocationController : ControllerBase
    {
        // GET api/Location
        [HttpGet]
        public List<LocationEntity> GetLocations() {

            // Location will be populated with the result of the query.
            List<LocationEntity> Location = new List<LocationEntity>();

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                // create a string to hold the SQL command.
                string sql = $"select * from locations;";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    
                    // Reader allows you to read each value that comes back from the query and do something to it.
                    using(SqliteDataReader reader = command.ExecuteReader()) {
                        
                        // Loop through query exit when no more objects are left.
                        while (reader.Read()) {

                            // map the data to the Locations model.
                            LocationEntity newLocation = new LocationEntity() {
                                locationID = reader.GetInt32(0),
                                name = reader.GetString(1),
                                address = reader.GetString(2),
                                city = reader.GetString(3),
                                state = reader.GetString(4),
                                zipCode = reader.GetString(5),
                                locationType = reader.GetString(6)
                            };

                            // Add one to the list.
                            Location.Add(newLocation);
                        }
                    }
                }
                // close the connection
                conn.Close();
            }
            return Location;
        }

        // // GET api/Location/user
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // POST api/Location
        [HttpPost]
        public void PostLocation([FromBody] LocationEntity postLocation)
        {

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"insert into locations (Name, Address, City, State, ZipCode, LocationType) values (\"{postLocation.name}\", \"{postLocation.address}\", \"{postLocation.city}\", \"{postLocation.state}\", \"{postLocation.zipCode}\", \"{postLocation.locationType}\");";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    command.ExecuteNonQuery();
                }
                // close the connection
                conn.Close();
            }
            return;           
        }

        // PUT api/Location/"named-put"
        [HttpPut]
        public void Put([FromBody] LocationEntity putLocation)
        {

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"update locations set Name = \"{putLocation.name}\", Address = \"{putLocation.address}\", City = \"{putLocation.city}\", State = \"{putLocation.state}\", ZipCode = \"{putLocation.zipCode}\", LocatioType = \"{putLocation.locationType}\"  where Name = \"{putLocation.name}\";";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    command.ExecuteNonQuery();
                }
                // close the connection
                conn.Close();
            }
            return;
        }

        // DELETE api/Location/"named-delete"
        [HttpDelete]
        public void Delete([FromBody] LocationEntity dropLocation)
        {
            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"delete from locations where Name = \"{dropLocation.name}\";";

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

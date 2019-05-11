using System;
using REST.Model;
using System.Collections;
using System.Data.SqlClient;

namespace REST.Manager
{
    //establish connection with DB
    public class DataBaseManager
    {
        private SqlConnection conn;
        public DataBaseManager()
        {
            string myConnection;
            myConnection = "Server=tcp:test-db-1.database.windows.net,1433;" +
            	"Initial Catalog=test;Persist Security Info=False;" +
            	"User ID=Anton;Password=Ghbdtn123;" +
            	"MultipleActiveResultSets=False;Encrypt=True;" +
            	"TrustServerCertificate=False;Connection Timeout=30;";
            try
            {
                conn = new SqlConnection
                {
                    ConnectionString = myConnection
                };
                conn.Open();
            }
            catch (SqlException err)
            {
                throw err;
            }

        }

        //GET all persons
        public ArrayList getAllPersons()
        {

            ArrayList peopleArray = new ArrayList();

            SqlDataReader mySQLReader = null;
            try
            {
                String sqlQuery = "SELECT * FROM people";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                mySQLReader = cmd.ExecuteReader();
                while (mySQLReader.Read())
                {
                    PersonClass person = new PersonClass
                    {
                        ID = mySQLReader.GetInt32(0),
                        FirstName = mySQLReader.GetString(1),
                        LastName = mySQLReader.GetString(2),
                        Age = mySQLReader.GetInt32(3)
                    };
                    peopleArray.Add(person);
                }
                return peopleArray;
            }
            catch (SqlException err)
            {
                throw err;
            }
            finally
            {
                conn.Close();
            }
        }

        //GET person by ID
        public PersonClass getPersonById(long ID)
        {
            PersonClass person = new PersonClass();
            SqlDataReader mySQLReader = null;
            try
            {
                String sqlQuery = "SELECT * FROM people WHERE ID = " + ID.ToString();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    person.ID = mySQLReader.GetInt32(0);
                    person.FirstName = mySQLReader.GetString(1);
                    person.LastName = mySQLReader.GetString(2);
                    person.Age = mySQLReader.GetInt32(3);
                    return person;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException err)
            {
                throw err;
            }
            finally
            {
                conn.Close();
            }
        }

        //POST new person
        public long savePerson(PersonClass personToSave)
        {
            String sqlQuery = "INSERT INTO people (FirstName, LastName, Age) VALUES ('" + personToSave.FirstName
                                                                                        + "','" + personToSave.LastName
                                                                                        + "'," + Convert.ToInt32(personToSave.Age) + ");"
                                                                                        + "SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            decimal id = (decimal)cmd.ExecuteScalar();
            return (long)id;
        }

        //Update person by id
        public bool updatePerson(long ID, PersonClass personToUpdate)
        {
            SqlDataReader mySQLReader = null;
            try
            {

                String sqlQuery = "SELECT * FROM people WHERE ID = " + ID.ToString();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mySQLReader.Close();
                    sqlQuery = "UPDATE people SET FirstName='" + personToUpdate.FirstName + "', " +
                        "LastName='" + personToUpdate.LastName + "', " +
                        "Age=" + Convert.ToInt32(personToUpdate.Age) + " " +
                        "WHERE ID =" + ID.ToString();

                    cmd = new SqlCommand(sqlQuery, conn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException err)
            {
                throw err;
            }
            finally
            {
                conn.Close();
            }
        }

        //Delete person by id
        public bool deletePerson(long ID)
        {
            PersonClass person = new PersonClass();
            SqlDataReader mySQLReader = null;
            try
            {

                String sqlQuery = "SELECT * FROM people WHERE ID = " + ID.ToString();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                mySQLReader = cmd.ExecuteReader();
                if (mySQLReader.Read())
                {
                    mySQLReader.Close();
                    sqlQuery = "DELETE FROM people WHERE ID = " + ID.ToString();
                    cmd = new SqlCommand(sqlQuery, conn);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException err)
            {
                throw err;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
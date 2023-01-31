﻿using System.Data.SqlClient;
using System.Data;

namespace NotBlocket2.Models {
    public class Location {

        //Konstruktor
        public Location() { }

        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class LocationMethods { 

        public LocationMethods() { }

        public List<Location> GetLocationsWithDataSet(out string errormsg) {
            //Skapa Sql connection
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabasLab3;Integrated Security=True";

            String sqlstring = "SELECT * FROM [NotBlocket].[dbo].[Locations]";
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();
            List<Location> PersonList = new List<Location>();

            try {
                dbConnection.Open();

                myAdapter.Fill(myDS, "myPerson");

                int count = 0;
                int i = 0;
                count = myDS.Tables["myPerson"].Rows.Count;


                if (count > 0) {
                    while (i < count) {
                        Location pd = new Location();

                        pd.Name = myDS.Tables["myPerson"].Rows[i]["Name"].ToString();
                        pd.Id = Convert.ToInt16(myDS.Tables["myPerson"].Rows[i]["Id"]);
                        i++;
                        PersonList.Add(pd);
                    }
                    errormsg = "";
                    return PersonList;
                }
                else { errormsg = "Det hämtas Ingen Location"; }
                return PersonList;
            }

            catch (Exception e) {
                errormsg = e.Message;
                return null;
            }

            finally { dbConnection.Close(); }
        }
    }

    public class ViewModelProfileAdsLocation {
            public IEnumerable<Profile> ProfileList { get; set; }
            public IEnumerable<Location> LocationList { get; set; }
            public IEnumerable<Ad> AdList { get; set; }
        }
    }

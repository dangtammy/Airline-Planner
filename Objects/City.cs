using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AirlinePlannerApp
{
  public class City
  {
    private int _id;
    private string _name;


    public City(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public override bool Equals(System.Object otherCity)
    {
      if(!(otherCity is City))
      {
        return false;
      }
      else
      {
        City newCity = (City) otherCity;
        bool idEquality = this.GetId() == newCity.GetId();
        bool nameEquality = this.GetName() == newCity.GetName();
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public void Save()
{
  SqlConnection conn = DB.Connection();
  conn.Open();

  SqlCommand cmd = new SqlCommand("INSERT INTO cities (name) OUTPUT INSERTED.id VALUES (@CityName);", conn);

  SqlParameter nameParameter = new SqlParameter();
  nameParameter.ParameterName = "@CityName";
  nameParameter.Value = this.GetName();
  cmd.Parameters.Add(nameParameter);
  SqlDataReader rdr = cmd.ExecuteReader();

  while(rdr.Read())
  {
    this._id = rdr.GetInt32(0);
  }
  if (rdr != null)
  {
    rdr.Close();
  }
  if(conn != null)
  {
    conn.Close();
  }
}

public static List<City> GetAll()
{
  List<City> allCities = new List<City>{};

  SqlConnection conn = DB.Connection();
  conn.Open();

  SqlCommand cmd = new SqlCommand("SELECT * FROM cities;", conn);
  SqlDataReader rdr = cmd.ExecuteReader();

  while(rdr.Read())
  {
    int Id = rdr.GetInt32(1);
    string cityName = rdr.GetString(0);
    City newCity = new City(cityName, Id);
    allCities.Add(newCity);
  }

  if (rdr != null)
  {
    rdr.Close();
  }
  if (conn != null)
  {
    conn.Close();
  }
  return allCities;
}

public static City Find(int id)
{
  SqlConnection conn = DB.Connection();
conn.Open();

SqlCommand cmd = new SqlCommand("SELECT * FROM cities WHERE id = @CityId;", conn);
SqlParameter cityIdParameter = new SqlParameter();
cityIdParameter.ParameterName = "@CityId";
cityIdParameter.Value = id.ToString();
cmd.Parameters.Add(cityIdParameter);
SqlDataReader rdr = cmd.ExecuteReader();

int foundCityId = 0;
string foundCityName = null;

while(rdr.Read())
{
  foundCityId = rdr.GetInt32(1);
  foundCityName = rdr.GetString(0);
}
City foundCity = new City(foundCityName, foundCityId);

if (rdr != null)
{
  rdr.Close();
}
if (conn != null)
{
  conn.Close();
}

return foundCity;

}
public static void DeleteAll()
{
  SqlConnection conn = DB.Connection();
  conn.Open();
  SqlCommand cmd = new SqlCommand("DELETE FROM cities;", conn);
  cmd.ExecuteNonQuery();
  conn.Close();
}

public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cities WHERE id = @CityId;", conn);

      SqlParameter CityIdParameter = new SqlParameter();
      CityIdParameter.ParameterName = "@CityId";
      CityIdParameter.Value = this.GetId();

      cmd.Parameters.Add(CityIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

  }
}

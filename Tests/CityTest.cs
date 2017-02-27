using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AirlinePlannerApp
{
  public class CityTest: IDisposable
  {
    public CityTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=airline_planner_test;Integrated Security=SSPI;";
    }

   [Fact]
   public void Test_Equal_ReturnsTrueForSameName()
   {
     //Arrange,Act
     City firstCity = new City("Seattle");
     City secondCity = new City ("Seattle");

     //Assert
     Assert.Equal(firstCity,secondCity);
   }

   [Fact]
   public void Test_Save_SavesCategoryToDatabase()
   {
     //Arrange
     City testCity = new City("Spokane");
     testCity.Save();

     //Act
     List<City> result = City.GetAll();
     List<City> testList = new List<City>{testCity};

     //Assert
     Assert.Equal(testList, result);
   }
   public void Dispose()
   {
     City.DeleteAll();
   }

   [Fact]
   public void Test_AssignsIdToCityObject_Assign()
   {
     City testCity = new City("Spokane");
     testCity.Save();

     City savedCity = City.GetAll()[0];

     int result = savedCity.GetId();
     int testId = testCity.GetId();

     Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindCityInDatabase()
    {
      City testCity = new City("Portland");
      testCity.Save(); 

      City foundCity = City.Find(testCity.GetId());

      Assert.Equal(testCity, foundCity);
    }
 }
}

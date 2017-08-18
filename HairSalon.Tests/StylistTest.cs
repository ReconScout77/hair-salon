using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hair_salon_test;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void GetAll_StylistsEmptyAtFirst_0()
    {
     //Arrange, Act
      int result = Stylist.GetAll().Count;
     //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameName_Stylist()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Juan");
      Stylist secondStylist = new Stylist("Juan");

      //Assert
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("Juan");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Juan");
      testStylist.Save();
      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> expectedList = new List<Stylist>{testStylist};
      //Assert
      CollectionAssert.AreEqual(expectedList, result);
    }

    [TestMethod]
    public void GetAll_ListAllStylists_StylistList()
    {
      Stylist newStylist = new Stylist("Lisa");
      newStylist.Save();
      Stylist newStylist2 = new Stylist("Juan");
      newStylist2.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      List<Stylist> expectedList = new List<Stylist>{newStylist, newStylist2};
      CollectionAssert.AreEqual(allStylists, expectedList);
    }

    [TestMethod]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("Juan");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void GetClients_RetrievesAllClientsOfStylist_ClientList()
    {
      Stylist testStylist = new Stylist("Juan");
      testStylist.Save();

      Client firstClient = new Client("Tristan", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Helga", testStylist.GetId());
      secondClient.Save();

      List<Client> expectedClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      CollectionAssert.AreEqual(expectedClientList, resultClientList);
    }

    [TestMethod]
    public void UpdateStylistName_UpdatesStylistName_StylistName()
    {
      Stylist testStylist = new Stylist("Juan");
      testStylist.Save();
      string newStylist = "Juanathan";
      testStylist.UpdateStylistName(newStylist);
      string result = testStylist.GetName();
      Assert.AreEqual(newStylist, result);
    }

    [TestMethod]
    public void Delete_DeleteStylistNameFromDatabase_StylistList()
    {
      Stylist newStylist = new Stylist("Juan");
      newStylist.Save();
      Stylist newStylist2 = new Stylist("Lisa");
      newStylist2.Save();

      newStylist.DeleteStylist();
      List<Stylist> allStylists = Stylist.GetAll();
      List<Stylist> expectedList = new List<Stylist>{newStylist2};

      CollectionAssert.AreEqual(allStylists, expectedList);
    }

    [TestMethod]
    public void DeleteStylistClients_DeleteAllClientsForStylist_ClientList()
    {
      Stylist newStylist = new Stylist("Juan");
      newStylist.Save();

      Client firstClient = new Client("Tristan", newStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Helga", newStylist.GetId());
      secondClient.Save();

      Stylist.DeleteStylistClients(newStylist.GetId());
      List<Client> expected = new List<Client> {};
      List<Client> result = newStylist.GetClients();

      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Stylist_DeleteStylistAndAllRelatedClientsFromDatabase_ClientList()
    {
      Stylist newStylist = new Stylist("Juan");
      newStylist.Save();
      Stylist newStylist2 = new Stylist("Lisa");
      newStylist2.Save();

      Client firstClient = new Client("Tristan", newStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Helga", newStylist.GetId());
      secondClient.Save();
      Client thirdClient = new Client("Kelsey", newStylist2.GetId());
      thirdClient.Save();
      Client fourthClient = new Client("Logan", newStylist2.GetId());
      fourthClient.Save();

      newStylist.DeleteStylist();
      // List<Stylist> allStylists = Stylist.GetAll();
      // List<Stylist> expectedList = new List<Stylist>{newStylist2};
      List<Client> expected = new List<Client>{thirdClient, fourthClient};
      List<Client> result = Client.GetAll();

      CollectionAssert.AreEqual(expected, result);
    }
  }
}

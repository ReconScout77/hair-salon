using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hair_salon_test;";
    }

    [TestMethod]
    public void GetAll_ClientsEmptyAtFirst_0()
    {
     //Arrange, Act
      int result = Client.GetAll().Count;
     //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameName_Client()
    {
      Client newClient = new Client("Tristan", 1);
      Client newClient2 = new Client("Tristan", 1);
      Assert.AreEqual(newClient, newClient2);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Client testClient = new Client("Tristan", 1);

      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      //Arrange
      Client newClient = new Client("Tristan", 1);

      //Act
      newClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> expectedList = new List<Client>{newClient};

      //Assert
      CollectionAssert.AreEqual(expectedList, result);
    }

    [TestMethod]
    public void GetAll_ListofClients_ClientList()
    {
      Client newClient = new Client("Tristan", 1);
      newClient.Save();
      Client newClient2 = new Client("Helga", 2);
      newClient2.Save();
      List<Client> allClients = Client.GetAll();
      List<Client> expectedList = new List<Client>{newClient, newClient2};
      CollectionAssert.AreEqual(allClients, expectedList);
    }


    [TestMethod]
    public void Find_FindsClientInDatabase_Client()
    {
      //Arrange
      Client testClient = new Client("Tristan", 1);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Update_UpdatesClientNameInDatabase_String()
    {
      //Arrange
      Client testClient = new Client("Tristan", 1);
      testClient.Save();
      string newName = "Tristiano";

      //Act
      testClient.UpdateName(newName);

      string result = testClient.GetName();

      //Assert
      Assert.AreEqual(newName, result);
    }

    [TestMethod]
    public void Delete_DeletesClientNameFromDatabase_ClientList()
    {
      Client newClient = new Client("Tristan", 1);
      newClient.Save();
      Client newClient2 = new Client("Helga", 2);
      newClient2.Save();

      Client.DeleteClient(newClient.GetId());
      List<Client> allClients = Client.GetAll();
      List<Client> expectedList = new List<Client>{newClient2};

      CollectionAssert.AreEqual(allClients, expectedList);
    }
  }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/form")]
    public ActionResult StylistForm()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult StylistList()
    {
      Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/{id}/{name}-clients")]
    public ActionResult Clients(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      List<Client> allClients = thisStylist.GetClients();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylist", thisStylist);
      model.Add("clients", allClients);
      return View(model);
    }

    [HttpGet("/stylists/{id}/{name}-clients/form")]
    public ActionResult ClientForm(int id)
    {
      return View(Stylist.Find(id));
    }

    [HttpPost("/stylists/{id}/{name}-clients/new")]
    public ActionResult NewClient(int id)
    {
      Client newClient = new Client(Request.Form["client-name"], id);
      newClient.Save();
      Stylist thisStylist = Stylist.Find(id);
      List<Client> allClients = thisStylist.GetClients();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylist", thisStylist);
      model.Add("clients", allClients);
      return View("Clients", model);
    }

    [HttpGet("/stylists/{id}/{name}-clients/edit")]
    public ActionResult StylistEdit(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      return View(thisStylist);
    }

    [HttpPost("/stylists/{id}/{name}-clients/update")]
    public ActionResult StylistUpdate(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.UpdateStylistName(Request.Form["stylist-name"]);
      return RedirectToAction("Clients");
    }

    [HttpGet("/stylists/{id}/{name}-clients/{id2}")]
    public ActionResult ClientDetails(int id, int id2)
    {
      Stylist thisStylist = Stylist.Find(id);
      Client thisClient = Client.Find(id2);
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylist", thisStylist);
      model.Add("client", thisClient);
      return View(model);
    }

    [HttpGet("/stylists/{id}/{name}-clients/{id2}/edit")]
    public ActionResult ClientEdit(int id, int id2)
    {
      Stylist thisStylist = Stylist.Find(id);
      Client thisClient = Client.Find(id2);
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylist", thisStylist);
      model.Add("client", thisClient);
      return View(model);
    }

    [HttpPost("/stylists/{id}/{name}-clients/{id2}/update")]
    public ActionResult ClientUpdate(int id2)
    {
      Client thisClient = Client.Find(id2);
      thisClient.UpdateName(Request.Form["client-name"]);
      return RedirectToAction("ClientDetails");
    }

    [HttpGet("/stylists/{id}/{name}-clients/{id2}/deleted")]
    public ActionResult ClientDelete(int id, int id2)
    {
      Client.DeleteClient(id2);
      Stylist thisStylist = Stylist.Find(id);
      List<Client> allClients = thisStylist.GetClients();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylist", thisStylist);
      model.Add("clients", allClients);
      return View("Clients", model);
    }

    [HttpGet("/stylists/{id}/deleted")]
    public ActionResult StylistDelete(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.DeleteStylist();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }
  }
}

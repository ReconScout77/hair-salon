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
  }
}

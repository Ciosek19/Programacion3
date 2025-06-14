using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Services;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
   public class HomeController : Controller
   {
      private readonly ProgramacionService _programacionService;
      private readonly PatrocinadorService _patrocinadorService;

      public HomeController()
      {
         var contexto = new VozDelEsteContext();
         _programacionService = new ProgramacionService(contexto);
         _patrocinadorService = new PatrocinadorService(contexto);
      }

      public ActionResult Index()
      {
         var siguientesProgrmas = _programacionService.ObtenerResumen(3);
         var programacionDiaria = _programacionService.ObtenerProgramasDelDia();
         ViewBag.Message = "Your application description page.";
         var programaEnVivo = programacionDiaria.Find(e => e.EstaEnVivo);
         if (programaEnVivo != null) ViewBag.ProgramaEnVivo = programaEnVivo;
         var patrocinadores = _patrocinadorService.ObtenerPatrocinadores();

         var dashboard = new HomeIndexViewModel()
         {
            siguientesProgramas = siguientesProgrmas,
            programacionDiaria = programacionDiaria,
            patrocinadores = patrocinadores
         };
         return View(dashboard);
      }

      [TienePermiso("Gestion Noticias")]
      public ActionResult About()
      {
         
         return View();
      }

      public ActionResult Contact()
      {
         ViewBag.Message = "Your contact page.";

         return View();
      }
   }
}
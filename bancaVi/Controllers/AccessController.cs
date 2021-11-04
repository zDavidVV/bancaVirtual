using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bancaVi.Models;
using bancaVi.ViewModels.Access;

namespace bancaVi.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        private Db1 db = new Db1();
        public ActionResult Index(AccessViewModel accessViewModel)
        {
            if (TempData["sms"] != null)
            {
                ViewBag.sms = TempData["sms"].ToString();
            }
            try
            {
                if (accessViewModel.Usuario != null)
                {
                    accessViewModel.Error = false;
                    cliente autenticacion = db.cliente.Where(a => a.correo_cliente == accessViewModel.Usuario && a.contraseña_cliente == accessViewModel.Contraseña).FirstOrDefault();

                    if (autenticacion != null)
                    {
                        Session["user"] = autenticacion;
                        Session["saldo"] = autenticacion.cuenta.First().saldo_cuenta;

                        return Redirect("~/home/index");

                    }
                    else
                    {
                        Session["user"] = null;
                        accessViewModel.Error = true;
                        accessViewModel.Mensaje = "Usuario incorrecto";
                        return View(accessViewModel);

                    }
                }

            }
            catch (Exception ex)
            {
                return Content("Ocurrio un error :( " + ex.Message);
            }
            return View(accessViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cliente,nombre_cliente,telefono_cliente,correo_cliente,contraseña_cliente")] cliente cliente)
        {
            

            if (ModelState.IsValid)
            {
                if (db.cliente.Where(c => c.correo_cliente == cliente.correo_cliente).FirstOrDefault() == null) {
                    if(db.cliente.Where(c => c.id_cliente == cliente.id_cliente).FirstOrDefault() == null)
                    {
                        db.cliente.Add(cliente);
                        db.SaveChanges();
                        db.cuenta.Add(new cuenta { id_cliente = cliente.id_cliente, saldo_cuenta = 0 });
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["sms"] = "El usuario ya tiene una cuenta";
                        ViewBag.sms = TempData["sms"];
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["sms"] = "El usuario ya existe ";
                    ViewBag.sms = TempData["sms"];
                    return RedirectToAction("Index");
                }
            }
            
            return View(cliente);
        }


        [Route("Logout")]
        public ActionResult Logout()
        {
            Session["user"] = null;

            return RedirectToAction("Index");
        }
    }
}
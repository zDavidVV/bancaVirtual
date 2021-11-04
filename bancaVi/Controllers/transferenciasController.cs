using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bancaVi.Models;
using bancaVi.ViewModels;
using bancaVi.ViewModels.Access;

namespace bancaVi.Controllers
{
    public class transferenciasController : Controller
    {
        private Db1 db = new Db1();

        // GET: transferencias
        public ActionResult Index(AccessController accessViewModel)
        {
            if (TempData["smserror"] != null)
            {
                ViewBag.smserror = TempData["smserror"].ToString();
            }

            if (TempData["sms"] != null)
            {
                ViewBag.sms = TempData["sms"].ToString();
            }
            cliente cliente = System.Web.HttpContext.Current.Session["user"] as cliente;
            cuenta cuenta = cliente.cuenta.FirstOrDefault();
            var transferencia = db.transferencia.Where(t => t.id_cuenta_cliente == cuenta.id_cuenta_cliente || t.id_cuenta_user == cuenta.id_cuenta_cliente).Include(t => t.cuenta).Include(t => t.cuenta1);
            return View(transferencia.ToList());
        }

        // GET: transferencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transferencia transferencia = db.transferencia.Find(id);
            if (transferencia == null)
            {
                return HttpNotFound();
            }
            return View(transferencia);
        }

        // GET: transferencias/Create
        public ActionResult Create()
        {
            ViewBag.id_cuenta_cliente = new SelectList(db.cuenta, "id_cuenta_cliente", "id_cliente");
            ViewBag.id_cuenta_user = new SelectList(db.cuenta, "id_cuenta_cliente", "id_cliente");
            return View();
        }

        // POST: transferencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_transferencia,id_cuenta_cliente,monto_d,id_cuenta_user")] transferencia transferencia)
        {
            cliente cliente = System.Web.HttpContext.Current.Session["user"] as cliente;
            cuenta cuenta = db.cuenta.Where(c => c.id_cliente == cliente.id_cliente).FirstOrDefault();
            if (ModelState.IsValid)
            {

                if (cuenta.saldo_cuenta >= transferencia.monto_d)
                {
                    if (transferencia.id_cuenta_user == cuenta.id_cuenta_cliente) {
                        transferencia.id_cuenta_cliente = cuenta.id_cuenta_cliente;
                        db.transferencia.Add(transferencia);
                        cuenta.saldo_cuenta -= transferencia.monto_d;
                        db.Entry(cuenta).State = EntityState.Modified;
                        db.SaveChanges();
                        Session["saldo"] = cuenta.saldo_cuenta;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["smserror"] = "La cuenta que ingreso de destino no existe";
                        ViewBag.smserror = TempData["smserror"];
                        return RedirectToAction("Index");

                    }

                }
                else
                {
                    TempData["sms"] = "El monto de la transferencia es mas alto que su saldo ";
                    ViewBag.sms = TempData["sms"];
                    return RedirectToAction("Index");
                }

            }


            //ewBag.id_cuenta_cliente = new SelectList(db.cuenta, "id_cuenta_cliente", "id_cliente", transferencia.id_cuenta_cliente);
            //ViewBag.id_cuenta_user = new SelectList(db.cuenta, "id_cuenta_cliente", "id_cliente", transferencia.id_cuenta_user);
            ViewBag.id_cuenta_user = new SelectList(db.cuenta.Where(c => c.id_cliente != cliente.id_cliente), "id_cuenta_cliente", "id_cliente", transferencia.id_cuenta_user);
            return View(transferencia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bancaVi.Models;

namespace bancaVi.Controllers
{
    public class pagosController : Controller
    {
        private Db1 db = new Db1();

        // GET: pagos
        public ActionResult Index()
        {
            if (TempData["sms"] != null)
            {
                ViewBag.sms = TempData["sms"].ToString();
            }
            cliente cliente = System.Web.HttpContext.Current.Session["user"] as cliente;
            cuenta cuenta = cliente.cuenta.FirstOrDefault();
            var pago = db.pago.Where(t => t.id_cuenta_cliente == cuenta.id_cuenta_cliente).Include(p => p.cuenta).Include(p => p.servicio);
            return View(pago.ToList());
        }

        // GET: pagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pago pago = db.pago.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // GET: pagos/Create
        public ActionResult Create()
        {
            cliente cliente = System.Web.HttpContext.Current.Session["user"] as cliente;
            ViewBag.id_cuenta_cliente = new SelectList(db.cuenta.Where(c => c.id_cliente == cliente.id_cliente), "id_cuenta_cliente", "id_cliente");
            ViewBag.id_servicio = new SelectList(db.servicio, "id_servicio", "nombre_servicio");
            return View();
        }

        // POST: pagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pago,id_servicio,id_cuenta_cliente")] pago pago)
        {
            cliente cliente = System.Web.HttpContext.Current.Session["user"] as cliente;
            cuenta cuenta =  db.cuenta.Where(c => c.id_cliente == cliente.id_cliente).FirstOrDefault();
            servicio servicio = db.servicio.Find(pago.id_servicio);
            pago.id_cuenta_cliente = cuenta.id_cuenta_cliente;

            if (ModelState.IsValid)
            {
                if(cuenta.saldo_cuenta >= servicio.valor_servicio)
                {
                    db.pago.Add(pago);
                    db.SaveChanges();
                    cuenta.saldo_cuenta -= servicio.valor_servicio;
                    db.Entry(cuenta).State = EntityState.Modified;
                    db.SaveChanges();
                    Session["saldo"] = cuenta.saldo_cuenta;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["sms"] = "No tiene el saldo suficiente para pagar el servicio ";
                    ViewBag.sms = TempData["sms"];
                    return RedirectToAction("Index");
                }
            }       

            ViewBag.id_cuenta_cliente = new SelectList(db.cuenta, "id_cuenta_cliente", "id_cliente", pago.id_cuenta_cliente);
            ViewBag.id_servicio = new SelectList(db.servicio, "id_servicio", "nombre_servicio", pago.id_servicio);
            return View(pago);
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

using CreeGuanajuato.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CreeGuanajuato.Utilidades
{
    public class ThreadsInegi
    {
        public class ThreadEstado
        {
            public static Servicios.ServiceManager oServiceManager { get; private set; }
            private readonly CreeGuanajuatoContext _context;

            // The constructor obtains the state information.
            public ThreadEstado(CreeGuanajuatoContext context)
            {
                oServiceManager = new Servicios.ServiceManager(new Servicios.RestService());
                _context = context;
            }

            // The thread procedure performs the task, such as formatting
            // and printing a document.
            public async Task ThreadProc()
            {
                Models.inegi.Estado estados = await oServiceManager.ObtieneEstados();

                int id_estado = await GuardaEstado(estados.datos);
                ThreadMunicipio threadMunicipio = new ThreadMunicipio(_context, estados.datos.cve_agee, id_estado);
                await threadMunicipio.ThreadProc();

                //foreach (Models.inegi.EstadoDatos estado in estados.datos)
                //{
                //    int id_estado = await GuardaEstado(estado);
                //    ThreadMunicipio threadMunicipio = new ThreadMunicipio(_context, estado.cve_agee, id_estado);
                //    await threadMunicipio.ThreadProc();
                //}
            }

            private async Task<int> GuardaEstado(Models.inegi.EstadoDatos estado) {
                Models.Estado ctEstado = new Models.Estado();

                if (!EstadoExists(estado.cve_agee))
                {    
                    ctEstado.nombre_estado = estado.nom_agee;
                    ctEstado.cve_agee = estado.cve_agee;

                    _context.Estado.Add(ctEstado);
                }
                else {
                    ctEstado = _context.Estado.Where(e => e.cve_agee == estado.cve_agee).FirstOrDefault();
                    ctEstado.nombre_estado = estado.nom_agee;
                    ctEstado.cve_agee = estado.cve_agee;

                    _context.Entry(ctEstado).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();

                return ctEstado.id_estado;
            }

            private bool EstadoExists(string cve_agee)
            {
                return _context.Estado.Any(e => e.cve_agee == cve_agee); ;
            }
        }

        public class ThreadMunicipio
        {
            public static Servicios.ServiceManager oServiceManager { get; private set; }
            private readonly CreeGuanajuatoContext _context;
            // State information used in the task.
            private string cve_agee;
            private int id_estado;

            // The constructor obtains the state information.
            public ThreadMunicipio(CreeGuanajuatoContext context, string cve_agee, int id_estado)
            {
                oServiceManager = new Servicios.ServiceManager(new Servicios.RestService());
                _context = context;
                this.cve_agee = cve_agee;
                this.id_estado = id_estado;
            }

            // The thread procedure performs the task, such as formatting
            // and printing a document.
            public async Task ThreadProc()
            {
                Models.inegi.Municipio municipios = await oServiceManager.ObtieneMunicipios(cve_agee);

                foreach (Models.inegi.MunicipioDatos municipio in municipios.datos)
                {
                    int id_municipio = await GuardaMunicipio(municipio, id_estado);

                    //ThreadColonia threadColonia = new ThreadColonia(_context, cve_agee, municipio.cve_agem, id_municipio);
                    //Thread t = new Thread(new ThreadStart(threadColonia.ThreadProc));
                    //t.Start();
                    //Console.WriteLine("Main thread does some work, then waits.");
                    //t.Join();
                    //Console.WriteLine("Independent task has completed; main thread ends.");
                    ThreadColonia threadColonia = new ThreadColonia(_context, cve_agee, municipio.cve_agem, id_municipio);
                    await threadColonia.ThreadProc();
                }
            }

            private async Task<int> GuardaMunicipio(Models.inegi.MunicipioDatos municipio, int id_estado)
            {
                Models.Municipio ctMunicipio = new Models.Municipio();

                if (!MunicipioExists(municipio.cve_agem, id_estado))
                {
                    ctMunicipio.id_estado = id_estado;
                    ctMunicipio.cve_agem = municipio.cve_agem;
                    ctMunicipio.nombre_municipio = municipio.nom_agem;

                    _context.Municipio.Add(ctMunicipio);
                }
                else
                {
                    ctMunicipio = _context.Municipio.Where(e => e.cve_agem == municipio.cve_agem && e.id_estado == id_estado).FirstOrDefault();
                    ctMunicipio.cve_agem = municipio.cve_agem;
                    ctMunicipio.nombre_municipio = municipio.nom_agem;

                    _context.Entry(ctMunicipio).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();

                return ctMunicipio.id_municipio;
            }

            private bool MunicipioExists(string cve_agem, int id_estado)
            {
                return _context.Municipio.Any(e => e.cve_agem == cve_agem && e.id_estado == id_estado); ;
            }
        }

        public class ThreadColonia
        {
            public Servicios.ServiceManager oServiceManager { get; private set; }
            private readonly CreeGuanajuatoContext _context;
            // State information used in the task.
            private string cve_agee;
            private string cve_agem;
            private int id_municipio;

            // The constructor obtains the state information.
            public ThreadColonia(CreeGuanajuatoContext context, string cve_agee, string cve_agem, int id_municipio)
            {
                oServiceManager = new Servicios.ServiceManager(new Servicios.RestService());
                _context = context;
                this.cve_agee = cve_agee;
                this.cve_agem = cve_agem;
                this.id_municipio = id_municipio;
            }

            // The thread procedure performs the task, such as formatting
            // and printing a document.
            public async Task ThreadProc()
            {
                Models.inegi.Colonia colonias = await oServiceManager.ObtieneColonias(cve_agee, cve_agem);
                await GuardaColonia(colonias, id_municipio);
            }

            private async Task GuardaColonia(Models.inegi.Colonia colonias, int id_municipio)
            {
                foreach (Models.inegi.ColoniaDatos colonia in colonias.datos) {

                    Models.Colonia ctColonia = new Models.Colonia();

                    if (!ColoniaExists(colonia.cve_loc, id_municipio))
                    {
                        ctColonia.cve_loc = colonia.cve_loc;
                        ctColonia.nombre_colonia = colonia.nom_loc;
                        ctColonia.id_municipio = id_municipio;

                        _context.Colonia.Add(ctColonia);
                    }
                    else
                    {
                        ctColonia = _context.Colonia.Where(e => e.cve_loc == colonia.cve_loc && e.id_municipio == id_municipio).FirstOrDefault();
                        ctColonia.cve_loc = colonia.cve_loc;
                        ctColonia.nombre_colonia = colonia.nom_loc;

                        _context.Entry(ctColonia).State = EntityState.Modified;
                    }

                    await _context.SaveChangesAsync();
                }
            }

            private bool ColoniaExists(string cve_loc, int id_municipio)
            {
                return _context.Colonia.Any(e => e.cve_loc == cve_loc && e.id_municipio == id_municipio); ;
            }
        }
    }
}

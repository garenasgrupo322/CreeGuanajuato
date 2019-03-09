using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreeGuanajuato.Models;
using System.Diagnostics;

namespace CreeGuanajuato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroesController : ControllerBase
    {
        private readonly CreeGuanajuatoContext _context;
        public static Services.ServiceManager oServiceManager { get; private set; }


        public RegistroesController(CreeGuanajuatoContext context)
        {
            _context = context;
            oServiceManager = new Services.ServiceManager(new Services.RestService());
        }

        // GET: api/Registroes
        [HttpGet]
        public IEnumerable<Registro> GetRegistro()
        {
            return _context.Registro;
        }

        // GET: api/Registroes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registro = await _context.Registro.FindAsync(id);

            if (registro == null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        [Route("GetReport")]
        [HttpPost]
        public IEnumerable<Registro> GetRegistroFitros([FromBody] paramsFiltro registro)
        {
            IQueryable<Registro> registroIQ = from s in _context.Registro
                .Include(r => r.Colonia)
                .Include(r => r.Direccion)
                .Include(r => r.Escolaridad)
                .Include(r => r.Estado)
                .Include(r => r.Municipio)
                .Include(r => r.Necesidad)
                                              select s;

            if (registro.id_estado != 0) {
                registroIQ = registroIQ.Where(s => s.id_estado == registro.id_estado);
            }

            if (registro.id_municipio != 0) {
                registroIQ = registroIQ.Where(s => s.id_municipio == registro.id_municipio);
            }

            if (registro.id_colonia != 0) {
                registroIQ = registroIQ.Where(s => s.id_colonia == registro.id_colonia);
            }

            if (registro.id_escolaridad != 0) {
                registroIQ = registroIQ.Where(s => s.id_escolaridad == registro.id_escolaridad);
            }

            if (registro.id_necesidad != 0)
            {
                registroIQ = registroIQ.Where(s => s.id_necesidad == registro.id_necesidad);
            }

            if (!string.IsNullOrEmpty(registro.busqueda)) {
                registroIQ = registroIQ.Where(s => s.nombre.Contains(registro.busqueda) || s.apellido_materno.Contains(registro.busqueda) || s.apellido_paterno.Contains(registro.busqueda) || 
                    s.INE.Contains(registro.busqueda));
            }


            return registroIQ.ToList();
        }

        public class paramsFiltro
        {
            public int id_estado { get; set; }
            public int id_municipio { get; set; }
            public int id_colonia { get; set; }
            public int id_escolaridad { get; set; }
            public int id_necesidad { get; set; }
            public string busqueda { get; set; }
        }

        // POST: api/Registroes
        [HttpPost]
        public async Task<IActionResult> PostRegistro([FromBody] Registro registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Estado.Any(i => i.nombre_estado.Equals(registro.Estado.nombre_estado)))
            {
                registro.Estado = _context.Estado.Where(i => i.nombre_estado.Equals(registro.Estado.nombre_estado)).First();
            }
            else
            {
                registro.id_estado = 0;
                _context.Estado.Add(registro.Estado);
                await _context.SaveChangesAsync();
            }

            if (_context.Municipio.Any(i => i.id_estado.Equals(registro.Estado.id_estado) && i.nombre_municipio.Equals(registro.Municipio.nombre_municipio)))
            {
                registro.Municipio = _context.Municipio.Where(i => i.id_estado.Equals(registro.Estado.id_estado) && i.nombre_municipio.Equals(registro.Municipio.nombre_municipio)).First();
            }
            else
            {
                registro.Municipio.id_estado = registro.Estado.id_estado;
                registro.Municipio.id_municipio = 0;
                _context.Municipio.Add(registro.Municipio);
                await _context.SaveChangesAsync();
            }

            if (_context.Colonia.Any(i => i.id_municipio.Equals(registro.Municipio.id_municipio) &&
                    i.nombre_colonia.Equals(registro.Colonia.nombre_colonia) && i.codigo_postal.Equals(registro.Colonia.codigo_postal)))
            {
                registro.Colonia = _context.Colonia.Where(i => i.id_municipio.Equals(registro.Municipio.id_municipio) &&
                    i.nombre_colonia.Equals(registro.Colonia.nombre_colonia) && i.codigo_postal.Equals(registro.Colonia.codigo_postal)).First();
            }
            else
            {
                registro.Colonia.id_colonia = 0;
                registro.Colonia.id_municipio = registro.Municipio.id_municipio;
                _context.Colonia.Add(registro.Colonia);
                await _context.SaveChangesAsync();
            }

            if (_context.Direccion.Any(i => i.calle.Equals(registro.Direccion.calle) && i.numero.Equals(registro.Direccion.numero) 
                && i.id_colonia == registro.Colonia.id_colonia))
            {
                registro.Direccion = _context.Direccion.Where(i => i.calle.Equals(registro.Direccion.calle) && i.numero.Equals(registro.Direccion.numero)
                    && i.id_colonia == registro.Colonia.id_colonia).First();
            } 
            else 
            {
                registro.Direccion.id_direccion = 0;
                registro.Direccion.id_colonia = registro.Colonia.id_colonia;
                _context.Direccion.Add(registro.Direccion);
                await _context.SaveChangesAsync();
            }

            if (_context.Escolaridad.Any(i => i.nombre.Equals(registro.Escolaridad.nombre)))
            {
                registro.Escolaridad = _context.Escolaridad.Where(i => i.nombre.Equals(registro.Escolaridad.nombre)).First();
            }
            else
            {
                registro.Escolaridad.id_escolaridad = 0;
                _context.Escolaridad.Add(registro.Escolaridad);
                await _context.SaveChangesAsync();
            }

            if (_context.EstadoCivil.Any(i => i.nombre.Equals(registro.EstadoCivil.nombre)))
            {
                registro.EstadoCivil = _context.EstadoCivil.Where(i => i.nombre.Equals(registro.EstadoCivil.nombre)).First();
            }
            else
            {
                registro.EstadoCivil.id_estado_civil = 0;
                _context.EstadoCivil.Add(registro.EstadoCivil);
                await _context.SaveChangesAsync();
            }

            if (_context.Necesidad.Any(i => i.descripcion.Equals(registro.Necesidad.descripcion)))
            {
                registro.Necesidad = _context.Necesidad.Where(i => i.descripcion.Equals(registro.Necesidad.descripcion)).First();
            }
            else
            {
                registro.Necesidad.id_necesidad = 0;
                _context.Necesidad.Add(registro.Necesidad);
                await _context.SaveChangesAsync();
            }

            if (_context.Seccion.Any(i => i.nombre.Equals(registro.Seccion.nombre))) {
                registro.Seccion = _context.Seccion.Where(i => i.nombre.Equals(registro.Seccion.nombre)).First();
            }
            else
            {
                registro.Seccion.id_seccion = 0;
                _context.Seccion.Add(registro.Seccion);
                await _context.SaveChangesAsync();
            }

            registro.id_estado = registro.Estado.id_estado;
            registro.id_municipio = registro.Municipio.id_municipio;
            registro.id_colonia = registro.Colonia.id_colonia;
            registro.id_direccion = registro.Direccion.id_direccion;
            registro.id_escolaridad = registro.Escolaridad.id_escolaridad;
            registro.id_estado_civil = registro.EstadoCivil.id_estado_civil;
            registro.id_necesidad = registro.Necesidad.id_necesidad;
            registro.id_seccion = registro.Seccion.id_seccion;

            string sDireccion = registro.Direccion.numero + ", " + registro.Direccion.calle + " " + registro.Colonia.nombre_colonia + ", " +
                registro.Municipio.nombre_municipio + ", " + registro.Estado.nombre_estado + " " + registro.Colonia.codigo_postal;

            registro.Estado = null;
            registro.Municipio = null;
            registro.Colonia = null;
            registro.Direccion = null;
            registro.Escolaridad = null;
            registro.EstadoCivil = null;
            registro.Necesidad = null;
            registro.Seccion = null;

            try
            {
                //Obtenemos las coordenadas 
                GoogleAPI coordenadas = await oServiceManager.ObtieneCoordenadas(sDireccion);

                registro.latitud = coordenadas.results[0].geometry.location.lat;
                registro.longitud = coordenadas.results[0].geometry.location.lng;
            } catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            registro.NormalizedNombre = registro.nombre + " " + registro.apellido_paterno + " " + registro.apellido_materno;

            _context.Registro.Add(registro);
            await _context.SaveChangesAsync();  



            return CreatedAtAction("GetRegistro", new { id = registro.id_registro }, registro);
        }
    }
}
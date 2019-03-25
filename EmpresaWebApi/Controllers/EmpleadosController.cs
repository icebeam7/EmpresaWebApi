using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpresaWebApi.Context;
using EmpresaWebApi.Models;
using EmpresaWebApi.DTOs;
using AutoMapper;

namespace EmpresaWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Empleados")]
    public class EmpleadosController : Controller
    {
        private readonly EmpresaContext _context;

        public EmpleadosController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: api/Empleados
        [HttpGet]
        public IEnumerable<EmpleadoDTO> GetEmpleados()
        {
            return Mapper.Map<IEnumerable<EmpleadoDTO>>(_context.Empleados.Include(x => x.FK_DepartamentoEmpleado));
        }

        // GET: api/Empleados/Departamento/2
        [HttpGet("Departamento/{idDepartamento}")]
        public IEnumerable<EmpleadoDTO> GetEmpleadosByDepartamento([FromRoute] int idDepartamento)
        {
            return Mapper.Map<IEnumerable<EmpleadoDTO>>
                (_context.Empleados
                .Include(x => x.FK_DepartamentoEmpleado)
                .Where(x => x.IdDepartamento == idDepartamento));
        }

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpleado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empleado = await _context.Empleados.SingleOrDefaultAsync(m => m.Id == id);

            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EmpleadoDTO>(empleado));
        }

        // PUT: api/Empleados/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado([FromRoute] int id, [FromBody] EmpleadoDTO empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empleado.Id)
            {
                return BadRequest();
            }

            _context.Entry(Mapper.Map<Empleado>(empleado)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Empleados
        [HttpPost]
        public async Task<IActionResult> PostEmpleado([FromBody] EmpleadoDTO empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emp = Mapper.Map<Empleado>(empleado);
            _context.Empleados.Add(emp);
            await _context.SaveChangesAsync();
            empleado.Id = emp.Id;

            return CreatedAtAction("GetEmpleado", new { id = empleado.Id }, empleado);
        }

        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empleado = await _context.Empleados.SingleOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return Ok(Mapper.Map<EmpleadoDTO>(empleado));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
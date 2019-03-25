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
    [Route("api/Departamentos")]
    public class DepartamentosController : Controller
    {
        private readonly EmpresaContext _context;

        public DepartamentosController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: api/Departamentos
        [HttpGet]
        public IEnumerable<DepartamentoDTO> GetDepartamentos()
        {
            return Mapper.Map<IEnumerable<DepartamentoDTO>>(
                _context.Departamentos);
        }

        // GET: api/Departamentos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartamento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.Id == id);

            if (departamento == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<DepartamentoDTO>(departamento));
        }

        // PUT: api/Departamentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento([FromRoute] int id, [FromBody] DepartamentoDTO departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(Mapper.Map<Departamento>(departamento)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
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

        // POST: api/Departamentos
        [HttpPost]
        public async Task<IActionResult> PostDepartamento([FromBody] DepartamentoDTO departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var depto = Mapper.Map<Departamento>(departamento);
            _context.Departamentos.Add(depto);
            await _context.SaveChangesAsync();
            departamento.Id = depto.Id;

            return CreatedAtAction("GetDepartamento", new { id = departamento.Id }, departamento);
        }

        // DELETE: api/Departamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();

            return Ok(Mapper.Map<DepartamentoDTO>(departamento));
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamentos.Any(e => e.Id == id);
        }
    }
}
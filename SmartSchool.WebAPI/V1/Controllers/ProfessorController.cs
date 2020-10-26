using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

    [HttpGet]
    public IActionResult Get()
    {
        var professores = _repo.GetAllProfessores(true);
        return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
    }

    // api/professor/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var professor = _repo.GetProfessorById(id, false);
        if (professor == null) return BadRequest("Professor não encontrado.");
        
        var professorDto = _mapper.Map<ProfessorDto>(professor);
        
        return Ok(professorDto);
    }

    // api/professor/1
    // [HttpGet("{id:int}")]
    // public IActionResult GetById(int id)
    // {
    //     var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
    //     if (professor == null) return BadRequest("Professor não encontrado.");
    //     return Ok(professor);
    // }

    // // api/professor/byId?id=1
    // [HttpGet("byId")]
    // public IActionResult GetByIdB(int id)
    // {
    //     var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
    //     if (professor == null) return BadRequest("Professor não encontrado.");
    //     return Ok(professor);
    // }

    // // api/professor/byId/1
    // [HttpGet("byId/{id}")]
    // public IActionResult GetByIdC(int id)
    // {
    //     var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
    //     if (professor == null) return BadRequest("Professor não encontrado.");
    //     return Ok(professor);
    // }

    // // api/professor/Lauro
    // [HttpGet("{name}")]
    // public IActionResult GetByName(string name)
    // {
    //     var professor = _context.Professores.FirstOrDefault(p => p.Nome.Contains(name));
    //     if (professor == null) return BadRequest("Professor não encontrado.");
    //     return Ok(professor);
    // }

    // // api/professor/byName?nome=Lauro
    // [HttpGet("byName")]
    // public IActionResult GetByNameB(string nome)
    // {
    //     var professor = _context.Professores.FirstOrDefault(p => p.Nome.Contains(nome));
    //     if (professor == null) return BadRequest("Professor não encontrado.");
    //     return Ok(professor);
    // }

    // api/professor
    [HttpPost]
    public IActionResult Post(ProfessorRegistrarDto model)
    {
        var professor = _mapper.Map<Aluno>(model);

        _repo.Add(professor);
        if (_repo.SaveChanges())
        {
            return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
        }

        return BadRequest("Professor não cadastrado");
    }

    // api/professor
    [HttpPut("{id}")]
    public IActionResult Put(int id, ProfessorRegistrarDto model)
    {
        var professor = _repo.GetProfessorById(id, false);
        if (professor == null) return BadRequest("Professor não encontrado.");

        _mapper.Map(model, professor);
        
        _repo.Update(professor);
        if (_repo.SaveChanges())
        {
            return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
        }

        return BadRequest("Professor não atualizado.");
    }

    // api/professor
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, ProfessorRegistrarDto model)
    {
        var professor = _repo.GetProfessorById(id, false);
        if (professor == null) return BadRequest("Professor não encontrado.");

        _mapper.Map(model, professor);
        
        _repo.Update(professor);
        if (_repo.SaveChanges())
        {
            return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
        }

        return BadRequest("Professor não atualizado.");
    }
    // api/professor
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var professor = _repo.GetProfessorById(id, false);
        if (professor == null) return BadRequest("Professor não encontrado.");
        
        _repo.Delete(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor deletado");
            }

            return BadRequest("Professor não deletado");
    }
}
}

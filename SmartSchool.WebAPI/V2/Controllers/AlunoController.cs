using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V2.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.V2.Controllers
{
    /// <sumary>
    ///
    /// </sumary>
        
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase

    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <sumary>
        ///
        /// </sumary>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // public List<Aluno> Alunos = new List<Aluno>(){
        //     new Aluno(){
        //         Id = 1,
        //         Nome = "Marcos",
        //         Sobrenome = "Almeida",
        //         Telefone = "1234556"
        //     },
        //     new Aluno(){
        //         Id = 2,
        //         Nome = "Marta",
        //         Sobrenome = "Kent",
        //         Telefone = "34212"
        //     },
        //     new Aluno(){
        //         Id = 3,
        //         Nome = "Laura",
        //         Sobrenome = "Maria",
        //         Telefone = "5456546"
        //     },
        // };

        /// <summary>
        /// Método responsável para retornar todos os meus alunos
        /// </summary>
        /// <returns></returns>       
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);        

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }
       
        /// <summary>
        /// Método responsável por retonar apenas um Aluno por meio do Código ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // api/aluno/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            
            return Ok(alunoDto);
        }

        // // api/aluno/1
        // [HttpGet("{id:int}")]
        // public IActionResult GetById(int Id)
        // {
        //     var aluno = _repo.GetAlunoById(Id, false);
        //     if (aluno == null) return BadRequest("O aluno não foi encontrado");

        //     return Ok(aluno);
        // }

        // // api/aluno/byId?id=1
        // [HttpGet("byId")]
        // public IActionResult GetByIdB(int Id)
        // {
        //     var aluno = _repo.GetAlunoById(Id, false);
        //     if (aluno == null) return BadRequest("O aluno não foi encontrado");

        //     return Ok(aluno);
        // }

        // // api/aluno/byId/1
        // [HttpGet("byId/{id}")]
        // public IActionResult GetByIdC(int Id)
        // {
        //     var aluno = _repo.GetAlunoById(Id, false);
        //     if (aluno == null) return BadRequest("O aluno não foi encontrado");

        //     return Ok(aluno);
        // }

        // api/aluno/laura
        // [HttpGet("{nome}")]
        // public IActionResult GetByName(string nome)
        // {
        //     var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome));
        //     if (aluno == null) return BadRequest("O aluno não foi encontrado");

        //     return Ok(aluno);
        // }

        // api/aluno/byName?nome=Marta&sobrenome=Kent
        // [HttpGet("byName")]
        // public IActionResult GetByNameB(string nome, string sobrenome)
        // {
        //     var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
        //     if (aluno == null) return BadRequest("O aluno não foi encontrado");

        //     return Ok(aluno);
        // }

        // api/aluno
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado");     
        }

        // api/aluno
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            _mapper.Map(model, aluno);
            
            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));                
            }

            return BadRequest("Aluno não atualizado");    
        }
        // api/aluno
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O aluno não foi encontrado");

            _mapper.Map(model, aluno);
            
            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));                
            }

            return BadRequest("Aluno não atualizado"); 
        }

        // api/aluno
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O aluno não foi encontrado");
           
            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno deletado");
            }

            return BadRequest("Aluno não deletado");
        }
    }
}

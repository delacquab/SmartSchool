using System;

namespace SmartSchool.WebAPI.V2.Dtos
{
    /// <sumary>
    /// Este é o DTO de Aluno para registrar
    /// </sumary>
    /// <returns></returns>
    public class AlunoRegistrarDto
    {
        /// <sumary>
        /// Identificador e chave do banco
        /// </sumary>
        /// <returns></returns>
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }      
        public DateTime DataIni { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
    }
}

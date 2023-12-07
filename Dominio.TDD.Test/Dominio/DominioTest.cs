using Bogus;
using Dominio.TDD.Cursos;
using Dominio.TDD.Enums;
using Dominio.TDD.Test._Builders;
using Dominio.TDD.Test._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Dominio.TDD.Test.Dominio
{
    public class DominioTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly string _descricao;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;

        public DominioTest(ITestOutputHelper output)
        {
            _output = output;
            var faker = new Faker();

            _nome = faker.Random.Word();
            _descricao = faker.Lorem.Paragraph();
            _cargaHoraria = faker.Random.Double(50, 100);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(50, 100);
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            //Organização
            string nome = _nome;
            string descricao = _descricao;
            double cargaHoraria = _cargaHoraria;
            var publicoAlvo = _publicoAlvo;
            double valor = _valor;

            //Ação
            var curso = new Curso(nome, descricao, cargaHoraria, publicoAlvo, valor);

            //Assert
            Assert.Equal(nome, curso.Nome);
            Assert.Equal(descricao, curso.Descricao);
            Assert.Equal(cargaHoraria, curso.CargaHoraria);
            Assert.Equal(PublicoAlvo.Estudante, publicoAlvo);
            Assert.Equal(valor, curso.Valor);
        }

        [Fact]
        public void DeveCriarCursoObjeto()
        {
            //Organização
            var cursoEsperado = new
            {
                Nome = _nome,
                Descricao =  _descricao,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            //Ação
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            //Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
        {
           Assert.Throws<ArgumentException>(() =>
               CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem("Nome é inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerCargaHorariaMenorQueZero(double cargaHoraria)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComCargaHoraria(cargaHoraria).Build())
                .ComMensagem("Carga Horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQueUm(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem("Valor Inválido");
        }
    }
}

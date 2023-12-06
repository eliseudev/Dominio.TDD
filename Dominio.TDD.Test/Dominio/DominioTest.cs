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
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;

        public DominioTest(ITestOutputHelper output)
        {
            _output = output;
            _nome = "Mundo C#/.Net";
            _cargaHoraria = 210.0;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = 950.8;
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
            double cargaHoraria = _cargaHoraria;
            var publicoAlvo = _publicoAlvo;
            double valor = _valor;

            //Ação
            var curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);

            //Assert
            Assert.Equal(nome, curso.Nome);
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
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            //Ação
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            //Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
        {
           Assert.Throws<ArgumentException>(() =>
                new Curso(nomeInvalido,_cargaHoraria, _publicoAlvo, _valor))
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
                new Curso(_nome, cargaHoraria, _publicoAlvo, _valor))
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
                new Curso(_nome, _cargaHoraria, _publicoAlvo, valorInvalido))
                .ComMensagem("Valor Inválido");
        }
    }

    internal class Curso
    {
        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome é inválido");

            if(cargaHoraria < 1)
                throw new ArgumentException("Carga Horária inválida");

            if(valor < 1)
                throw new ArgumentException("Valor Inválido");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }

    internal enum PublicoAlvo
    {
        Estudante,
        Universtário,
        Empregado,
        Empreendedor
    }
}

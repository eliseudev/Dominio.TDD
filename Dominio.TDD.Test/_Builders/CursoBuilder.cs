﻿using Dominio.TDD.Cursos;
using Dominio.TDD.Enums;

namespace Dominio.TDD.Test._Builders
{
    public class CursoBuilder
    {
       private string _nome = "Mundo C#/.Net";
       private string _descricao = "Descrição do curso";
       private double _cargaHoraria = 210.0;
       private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
       private double _valor = 950.8;

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public Curso Build()
        {
            return new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
        }
    }
}

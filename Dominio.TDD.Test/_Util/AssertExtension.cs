using System;
using Xunit;

namespace Dominio.TDD.Test._Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ArgumentException ex, string messagem)
        {
            if(ex.Message == messagem)
                Assert.True(true);
            else
                Assert.False(true, $"Esperava a menssagem: {messagem}");
        }
    }
}

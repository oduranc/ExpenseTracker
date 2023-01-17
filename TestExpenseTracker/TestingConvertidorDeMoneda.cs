using Xunit;
using System.Collections.Generic;
using System;
using ExpenseTracker;

namespace TestExpenseTracker
{
    public class StubBuscadorTasas : IBuscadorTasas
    {
        public const float TASA_USD_DOP_POPULAR = 55.5f;
        public const float TASA_DOP_USD_POPULAR = 45.6f;

        public int cantidadDeLlamadasParaObtenerTasas = 0;
        List<Tasa> IBuscadorTasas.ObtenerTasas()
        {
            cantidadDeLlamadasParaObtenerTasas++;
            List<Tasa> tasas = new List<Tasa>();
            tasas.Add(new Tasa(TASA_USD_DOP_POPULAR, "USD", "DOP", "Banco Popular"));
            tasas.Add(new Tasa(TASA_DOP_USD_POPULAR, "DOP", "USD", "Banco Popular"));
            return tasas;
        }
    }

    public class TestingConvertidorDeMoneda
    {
        [Fact]
        public void Probando_una_compra_de_dolares_en_el_popular()
        {
            // ARRANGE
            float tasaCorrecta = StubBuscadorTasas.TASA_DOP_USD_POPULAR;
            IBuscadorTasas buscadorTasas = new StubBuscadorTasas();
            ConvertidorDeMoneda sut = new ConvertidorDeMoneda(buscadorTasas);

            // ACT
            float dolares = sut.ComprarDolaresEnElPopular(20);

            // ASSERT
            Assert.Equal(20 / tasaCorrecta, dolares, 2);
            Assert.Equal(1, ((StubBuscadorTasas)buscadorTasas).cantidadDeLlamadasParaObtenerTasas);
        }

        [Theory]
        [InlineData (300)]
        [InlineData (123)]
        [InlineData (456)]
        [InlineData (427)]
        [InlineData (564)]
        public void Probando_varias_compras_de_dolares_en_el_popular(float pesos)
        {
            // ARRANGE
            float tasaCorrecta = StubBuscadorTasas.TASA_DOP_USD_POPULAR;
            IBuscadorTasas buscadorTasas = new StubBuscadorTasas();
            ConvertidorDeMoneda sut = new ConvertidorDeMoneda(buscadorTasas);

            // ACT
            float dolares = sut.ConvertirMoneda(pesos,banco,moneda);

            // ASSERT
            Assert.Equal(pesos / tasaCorrecta, dolares, 2);
            Assert.Equal(1, ((StubBuscadorTasas)buscadorTasas).cantidadDeLlamadasParaObtenerTasas);
        }
    }
}
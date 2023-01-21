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
        async Task<List<Tasa>> IBuscadorTasas.ObtenerTasas()
        {
            await Task.Delay(1000); // simulate a delay of 1 second
            cantidadDeLlamadasParaObtenerTasas++;
            List<Tasa> tasas =   new List<Tasa>
            {
                new Tasa(TASA_USD_DOP_POPULAR, "USD", "DOP", "Banco Popular"),
                new Tasa(TASA_DOP_USD_POPULAR, "DOP", "USD", "Banco Popular")
            };
            return tasas;
        }
    }

    public class TestingConvertidorDeMoneda
    {
        [Fact]
        public async Task Conversion_dolar_a_pesos_en_el_popular()
        {
            // ARRANGE
            float tasaCorrecta = StubBuscadorTasas.TASA_USD_DOP_POPULAR;
            IBuscadorTasas buscadorTasas = new StubBuscadorTasas();
            ConvertidorDeMoneda sut = new ConvertidorDeMoneda(buscadorTasas);

            // ACT
            float pesos = await sut.ConvertirMoneda(100, "Banco Popular", "DOP");

            // ASSERT
            Assert.Equal(100 * tasaCorrecta, pesos, 2);
            Assert.Equal(1, ((StubBuscadorTasas)buscadorTasas).cantidadDeLlamadasParaObtenerTasas);
        }


        [Theory]
        [InlineData(10000, "Banco Popular", "DOP")]
        [InlineData(58000, "Banco Popular", "DOP")]
        [InlineData(500, "Banco Popular", "DOP")]
        [InlineData(60356, "Banco Popular", "DOP")]
        [InlineData(780, "Banco Popular", "DOP")]
        public async Task Conversion_varias_dolar_a_pesos_en_el_popular(float dolares, string banco, string moneda)
        {
            // ARRANGE
            float tasaCorrecta = StubBuscadorTasas.TASA_USD_DOP_POPULAR;
            IBuscadorTasas buscadorTasas = new StubBuscadorTasas();
            ConvertidorDeMoneda sut = new ConvertidorDeMoneda(buscadorTasas);

            // ACT
            float pesos = await sut.ConvertirMoneda(dolares, banco, moneda);

            // ASSERT
            Assert.Equal(dolares * tasaCorrecta, pesos, 2);
            Assert.Equal(1, ((StubBuscadorTasas)buscadorTasas).cantidadDeLlamadasParaObtenerTasas);
        }

    }
}
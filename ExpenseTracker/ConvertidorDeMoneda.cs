using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExpenseTracker
{
    public class ConvertidorDeMoneda
    {
 
        IBuscadorTasas buscadorTasas;
        public ConvertidorDeMoneda(IBuscadorTasas buscadorTasas)
        {
            this.buscadorTasas = buscadorTasas;
        }


        public async Task <float> ConvertirMoneda(float dinero,string banco,string moneda)
        {
            float NuevaTasa;
            if (moneda == "USD")
            {
                var tasas = await buscadorTasas.ObtenerTasas();

                var tasadeConversionDolarPopular = tasas.Where(x => x.Entidad == $"{banco}"
                                                         && x.MonedaOrigen == "DOP"
                                                         && x.MonedaDestino == "USD").First();
                 NuevaTasa = dinero / tasadeConversionDolarPopular.Valor;
                
            }
            else
            {
                var tasas = await buscadorTasas.ObtenerTasas();
                var tasadeConversionPesosPopular = tasas.Where(x => x.Entidad == $"{banco}"
                                                         && x.MonedaOrigen == "USD"
                                                         && x.MonedaDestino == "DOP").First();

                NuevaTasa = dinero * tasadeConversionPesosPopular.Valor;
            
            }

            return NuevaTasa;
        }


    }
}

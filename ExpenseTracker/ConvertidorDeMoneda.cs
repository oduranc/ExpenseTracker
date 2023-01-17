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


        public async Task <float> ConvertirMoneda(float dinero,string banco,int moneda)
        {
            float NuevaTasa;
            if (moneda == 1)
            {
                var tasas = await buscadorTasas.ObtenerTasas();

                var tasaVentaDolaresPopular = tasas.Where(x => x.Entidad == $"{banco}"
                                                         && x.MonedaOrigen == "DOP"
                                                         && x.MonedaDestino == "USD").First();
                 NuevaTasa = dinero / tasaVentaDolaresPopular.Valor;
                
            }
            else
            {
                var tasas = await buscadorTasas.ObtenerTasas();
                var tasaVentaDolaresPopular = tasas.Where(x => x.Entidad == $"{banco}"
                                                         && x.MonedaOrigen == "USD"
                                                         && x.MonedaDestino == "DOP").First();

                NuevaTasa = dinero * tasaVentaDolaresPopular.Valor;
            
            }

            return NuevaTasa;
        }


    }
}

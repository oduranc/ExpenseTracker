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


        public async Task <float> ConvertirMoneda(string banco, float dinero)
        {
            float NuevaTasa;
            
                var tasas = await buscadorTasas.ObtenerTasas();
                var tasadeConversionPesosPopular = tasas.Where(x => x.Entidad == $"{banco}"
                                                         && x.MonedaOrigen == "USD"
                                                         && x.MonedaDestino == "DOP").First();

                NuevaTasa = dinero * tasadeConversionPesosPopular.Valor;
            
           

            return NuevaTasa;
        }


    }
}

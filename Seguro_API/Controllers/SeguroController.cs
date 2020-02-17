using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using Seguro_API.DAL;
using Seguro_API.Models;

namespace Seguro_API.Controllers
{
    public class SeguroController : ApiController
    {
        public int PostSeguro(Seguro seguro)
        {
            seguro.Calcular_Premio_Comercial();
            return new SeguroDAO().Insert(seguro);
        }

        public Seguro GetSeguro(int id)
        {
            return new SeguroDAO().Select(id);
        }

        public double GetValorSeguro(Seguro seguro)
        {
            seguro.Calcular_Premio_Comercial();
            return seguro.VALOR_SEGURO;
        }

        public string GetRelatorioSeguro()
        {
            IEnumerable<Seguro> seguros = new SeguroDAO().SelectAll();

            var cont = 0;
            var soma_seguro = 0.0;

            foreach (Seguro seguro in seguros)
            {
                cont++;
                soma_seguro += seguro.VALOR_SEGURO;
            }

            Relatorio relatorio = new Relatorio() { media_valor_seguro = soma_seguro / cont };

            return Newtonsoft.Json.JsonConvert.SerializeObject(relatorio);

        }
    }
}

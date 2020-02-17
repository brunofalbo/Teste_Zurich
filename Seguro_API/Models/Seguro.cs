using Seguro_API.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Seguro_API.Models
{
    public class Seguro : ISeguro
    {
        public int ID_SEGURO { get; set; }
        public string MODELO_VEICULO { get; set; }
        public double VALOR_VEICULO { get; set; }
        public string NOME_SEGURADO { get; set; }
        public string CPF_SEGURADO { get; set; }
        public int IDADE_SEGURADO { get; set; }
        public double VALOR_SEGURO { get; set; }

        //Taxa de Risco = (Valor do Veículo * 5) / (2 x Valor do Veículo)
        //Prêmio de Risco = Taxa de Risco x Valor do Veículo
        //Prêmio Puro = Prêmio de Risco x (1 + MARGEM DE SEGURANÇA)
        //Prêmio Comercial = LUCRO x Prêmio Puro   (Esse é o valor do seguro)



        private double Calcular_Taxa_Risco()
        {
            return ((VALOR_VEICULO * 5) / (2 * VALOR_VEICULO)) / 100;
        }

        private double Calcular_Premio_Risco()
        {
            return Calcular_Taxa_Risco() * VALOR_VEICULO;
        }

        private double Calcular_Premio_Puro()
        {
            return Calcular_Premio_Risco() * (1 + 0.03);
        }

        public void Calcular_Premio_Comercial()
        {
            VALOR_SEGURO = Math.Round(Calcular_Premio_Puro() * 0.05, 2);
        }
    }
}
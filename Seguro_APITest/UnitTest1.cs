using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seguro_API.Models;
using Seguro_API.Controllers;
using Seguro_API.Interface;
using Moq;
using Seguro_API.DAL;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Seguro_APITest
{
    [TestClass]
    public class UnitTest1
    {
        #region PostSeguro
        [TestMethod]
        public void PostSeguro_DeveGravarSeguro()
        {
            Seguro seguro = new Seguro()
            {
                MODELO_VEICULO = "mod1",
                VALOR_VEICULO = 83000.76,
                NOME_SEGURADO = "nome1",
                CPF_SEGURADO = "2347654",
                IDADE_SEGURADO = 12
            };

            seguro.Calcular_Premio_Comercial();

            SeguroDAO seguroDAO = new SeguroDAO();

            var resultado = seguroDAO.Insert(seguro);

            Assert.AreEqual(1, resultado);
        }

        [TestMethod]
        public void PostSeguro_NaoDeveGravarSeguro()
        {
            Seguro seguro = new Seguro()
            {
                MODELO_VEICULO = "mod1",
                VALOR_VEICULO = 9000000000000000000000000000000000000000000000000000000.0,
                NOME_SEGURADO = "nome1",
                CPF_SEGURADO = "2347654",
                IDADE_SEGURADO = 12
            };
            SeguroDAO seguroDAO = new SeguroDAO();

            var resultado = seguroDAO.Insert(seguro);

            Assert.AreEqual(0, resultado);
        }
        #endregion

        [TestMethod]
        public void GetSeguro_DeveRetornarCorreto()
        {
            Seguro seguro = new Seguro()
            {
                ID_SEGURO = 6,
                MODELO_VEICULO = "MODELO"
            };

            //Mock<ISeguroDAO> mock = new Mock<ISeguroDAO>();
            //mock.Setup(m => m.Select(seguro.ID_SEGURO)).Returns(seguro);
            SeguroDAO seguroDAO = new SeguroDAO();

            //var resultado = mock.Object.Select(seguro.ID_SEGURO);
            var resultado = seguroDAO.Select(seguro.ID_SEGURO);


            Assert.AreEqual(seguro.MODELO_VEICULO , resultado.MODELO_VEICULO);
        }

        [TestMethod]
        public void GetSeguro_NaoDeveRetornarSeguro()
        {
            Seguro seguro = new Seguro()
            {
                ID_SEGURO = 1,
                MODELO_VEICULO = "MODELO_ERRADO"
            };

            SeguroDAO seguroDAO = new SeguroDAO();
           
            var resultado = seguroDAO.Select(seguro.ID_SEGURO);

            Assert.AreEqual(null, resultado);
        }

        [TestMethod]
        public void CalcularValorSeguro()
        {
            Seguro seguro = new Seguro()
            {
                MODELO_VEICULO = "mod1",
                VALOR_VEICULO = 83000.76,
                NOME_SEGURADO = "nome1",
                CPF_SEGURADO = "2347654",
                IDADE_SEGURADO = 12
            };

            seguro.Calcular_Premio_Comercial();
            Assert.AreEqual(CalcularValorSeguro(seguro), seguro.VALOR_SEGURO);
        }

        public double CalcularValorSeguro(Seguro seguro)
        {
            var taxa_risco = ((seguro.VALOR_VEICULO * 5) / (2 * seguro.VALOR_VEICULO)) / 100;
            var premio_risco = taxa_risco * seguro.VALOR_VEICULO;
            var premio_puro = premio_risco * (1 + 0.03);
            return Math.Round(premio_puro * 0.05, 2);
        }
    }
}

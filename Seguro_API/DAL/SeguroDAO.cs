using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using Seguro_API.Models;
using Seguro_API.Interface;
using Oracle.ManagedDataAccess.Client;

namespace Seguro_API.DAL
{
    public class SeguroDAO : ISeguroDAO
    {
        public Seguro Select(int id)
        {
            Seguro seguro = null;
            using (IDbConnection conexao = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(PORT=1521))(CONNECT_DATA=(SID=xe))); User Id=system; Password=b123;"))
            {
                var parametro = new DynamicParameters();
                parametro.Add("ID_SEGURO", value: id);
                seguro = conexao.QueryFirstOrDefault<Seguro>("SELECT * FROM TB_SEGURO WHERE ID_SEGURO = :ID_SEGURO", param: parametro);
            }

            return seguro;
        }

        public IEnumerable<Seguro> SelectAll()
        {
            IEnumerable<Seguro> seguros = null;
            using (IDbConnection conexao = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(PORT=1521))(CONNECT_DATA=(SID=xe))); User Id=system; Password=b123;"))
            {
                seguros = conexao.Query<Seguro>("SELECT VALOR_SEGURO FROM TB_SEGURO WHERE VALOR_SEGURO IS NOT NULL");
            }

            return seguros;
        }

        public int Insert(Seguro seguro)
        {
            try
            {
                using (IDbConnection conexao = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(PORT=1521))(CONNECT_DATA=(SID=xe))); User Id=system; Password=b123;"))
                {
                    var parametro = new DynamicParameters();
                    parametro.Add("MODELO_VEICULO", value: seguro.MODELO_VEICULO);
                    parametro.Add("VALOR_VEICULO", value: seguro.VALOR_VEICULO);
                    parametro.Add("NOME_SEGURADO", value: seguro.NOME_SEGURADO);
                    parametro.Add("CPF_SEGURADO", value: seguro.CPF_SEGURADO);
                    parametro.Add("IDADE_SEGURADO", value: seguro.IDADE_SEGURADO);
                    parametro.Add("VALOR_SEGURO", value: seguro.VALOR_SEGURO);

                    return conexao.Execute(@"INSERT INTO TB_SEGURO( MODELO_VEICULO, VALOR_VEICULO, NOME_SEGURADO, CPF_SEGURADO, IDADE_SEGURADO, VALOR_SEGURO)
                                                            VALUES( :MODELO_VEICULO, :VALOR_VEICULO, :NOME_SEGURADO, :CPF_SEGURADO, :IDADE_SEGURADO, :VALOR_SEGURO)",
                                           param: parametro);
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
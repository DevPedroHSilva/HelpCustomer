using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public class SQL
{
    public SqlConnection Conexao;
    public List<object> Parameters = new List<object>();

    public SQL()
    {
        Conexao = Open();
    }


    ///Funções Locais da class - INI


    /// <summary>
    /// Retorna Um objeto command ja com os parametros, caso exista, adicionado a consulta
    /// </summary>
    /// <param name="str_sql"></param>
    /// <returns></returns>
    SqlCommand Command(string str_sql)
    {
        SqlCommand command = new SqlCommand(str_sql, Conexao);

        if (Parameters != null)
        {
            foreach (dynamic parameter in Parameters)
            {
                string chr_campo = parameter.chr_campo;
                string chr_valor = parameter.chr_valor;

                command.Parameters.AddWithValue(chr_campo, chr_valor);
            }
        }

        return command;
    }

    SqlConnection Open()
    {
        string str_conn = "";
        Ambiente ambiente = new Ambiente();
        str_conn = ambiente.str_conexao_ambiente;
        SqlConnection conn = new SqlConnection(str_conn);
        return conn;
    }

    ///Funções Locais da class - FIM

    public void prm(string chr_campo, string chr_valor)
    {
        Parameters.Add(new
        {
            chr_campo = chr_campo,
            chr_valor = chr_valor
        });
    }


    public void Close()
    {
        Conexao.Close();
    }

    public SqlDataReader DataReader(string str_sql)
    {
        SqlCommand command = Command(str_sql);

        Conexao.Open();
        SqlDataReader rdr = command.ExecuteReader(CommandBehavior.CloseConnection);
        return rdr;
    }


    public DataTable DataTable(string str_sql)
    {
        SqlCommand command = Command(str_sql);

        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();

        Conexao.Open();
        adapter.Fill(dataTable);
        Conexao.Close();
        return dataTable;
    }

    public DataRow DataRow(string str_sql)
    {
        DataRow dr = null;
        DataTable dt = DataTable(str_sql);

        if (dt.Rows.Count > 0 && dt != null)
        {
            dr = dt.Rows[0];
        }

        return dr;
    }
}




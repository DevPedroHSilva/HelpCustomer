using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public class Ambiente
{
    public string str_url = "";
    public string str_conexao_ambiente = "";

    public Ambiente()
    {
        str_url = HttpContext.Current.Request.Url.ToString();
        str_url = str_url.ToLower();
        str_conexao_ambiente = retornarConexaoAmbiente();
    }

    public bool DEV()
    {
        return str_url.Contains("localhost") ? true : false;
    }

    public bool HOMOLOG()
    {
        return str_url.Contains(".H") || str_url.Contains(".Homolog") ? true : false;
    }  
    
    public bool PROD()
    {
        return !HOMOLOG() && !DEV() ? true : false;
    }

    public string retornarConexaoAmbiente()
    {
        string str_conn = "";

        if (DEV())
            str_conn = "Conexao_D";
        else if (HOMOLOG())
            str_conn = "Conexao_H";
        else if (PROD())
            str_conn = "Conexao_P";

        string conexao = ConfigurationManager.ConnectionStrings[str_conn].ConnectionString;

        return conexao;
    }
    

}
import cx_Oracle
import pandas as pd
import os
from dotenv import load_dotenv
load_dotenv()

def conexao_oracle(username, password, dsn):
    connection = None
    cursor = None
    try:
        # Adiciona o caminho do Instant Client manualmente, caso o PATH não funcione
        os.environ['PATH'] = os.getenv('PATH_instantclient_23_5') + ";" + os.environ['PATH']
        
        connection = cx_Oracle.connect(user=username, password=password, dsn=dsn)
        cursor = connection.cursor()
        cursor.execute("SELECT * FROM TBL_Previsao")

        for row in cursor:
            print(row)

    except cx_Oracle.DatabaseError as e:
        error, = e.args
        print(f"Erro ao conectar-se ao Oracle: {error.message}")

    finally:
        return connection, cursor
    
def finaliza_conexao(cursor, connection):
    if cursor:
        cursor.close()
    if connection:
        connection.close()
        
def consulta_para_dataframe(cursor, query):
    try:
        # Executa a consulta
        cursor.execute(query)
        
        # Pega os nomes das colunas
        colunas = [col[0] for col in cursor.description]
        
        # Obtém os dados da consulta
        dados = cursor.fetchall()
        
        # Cria o DataFrame
        df = pd.DataFrame(dados, columns=colunas)
        
        return df
    
    except cx_Oracle.DatabaseError as e:
        error, = e.args
        print(f"Erro ao executar a consulta: {error.message}")
        return None

def truncate_tabelas(connection):
    cursor = connection.cursor()
    
    try:
        tabelas = [
            'TBL_Previsao',
            'TBL_Previsao_Futura',
            'TBL_Distrito',
            'TBL_Zona',
            'TBL_Municipio'
        ]
        
        for tabela in tabelas:
            print(f"Truncando a tabela {tabela}...")
            cursor.execute(f"TRUNCATE TABLE {tabela} CASCADE")
        
        connection.commit()
        print("Todas as tabelas foram truncadas com sucesso!")
    
    except cx_Oracle.DatabaseError as e:
        error, = e.args
        print(f"Erro ao truncar as tabelas: {error.message}")
        connection.rollback()
    
    except Exception as e:
        print(f"Ocorreu um erro inesperado: {str(e)}")
        connection.rollback()
    
    finally:
        if cursor:
            cursor.close()
        print("Cursor fechado.")
# %% [markdown]
# ### Bibliotecas

# %%
import os
import pandas as pd
import datetime as dt

# %% [markdown]
# ### Diretórios

# %%
BASE_DIR = os.path.dirname(os.path.abspath(__file__))
DIR_ARQUIVOS = os.path.join(BASE_DIR, "DADOS_METEREOLOGICOS_ESTADO_SP")
DIR_TRATADOS = os.path.join(BASE_DIR, "DADOS_TRATADOS")

# %% [markdown]
# ### Filtro de dados

# %%
# Função para criar diretório se não existir
def ensure_directory(directory):
    """
    Verifica se o diretório existe e, caso contrário, cria-o.
    
    Args:
    directory (str): Caminho do diretório a ser verificado/criado.
    """
    if not os.path.exists(directory):
        os.makedirs(directory)
        print(f"Diretório criado: {directory}")
    else:
        print(f"Diretório já existe: {directory}")

# %%
def load_and_split(file_path):
    """
    Carrega a planilha e separa o cabeçalho extra dos dados principais.
    
    Args:
    file_path (str): Caminho para o arquivo CSV.
    
    Returns:
    tuple: DataFrames para o cabeçalho e os dados.
    """
    with open(file_path, 'r', encoding='latin1') as file:
        lines = file.readlines()

    # Separar o cabeçalho extra (primeiras 8 linhas)
    header_lines = lines[:8]
    data_lines = lines[8:]

    # Criar DataFrame para o cabeçalho
    df_header = pd.DataFrame([line.strip().split(';') for line in header_lines])

    # A primeira linha dos dados (após o cabeçalho) deve conter os nomes das colunas
    column_names = data_lines[0].strip().split(';')
    df_data = pd.DataFrame([line.strip().split(';') for line in data_lines[1:]], columns=column_names)
    
    # Remover colunas vazias
    df_data = df_data.dropna(axis=1, how='all')
    try:
        df_data = df_data.drop(columns='')
    except:
        pass

    return df_header, df_data

# %%
def format_numeric_columns(df):
    """
    Formata as colunas numéricas, substituindo vírgulas por pontos e convertendo para float.
    
    Args:
    df (DataFrame): DataFrame contendo os dados a serem formatados.
    
    Returns:
    DataFrame: DataFrame com colunas numéricas formatadas.
    """
    cols_to_convert = df.columns.difference(['Data', 'Hora UTC'])
    df[cols_to_convert] = df[cols_to_convert].replace(',', '.', regex=True)
    df[cols_to_convert] = df[cols_to_convert].apply(pd.to_numeric, errors='coerce')
    return df

# %%
def format_time_column(df):
    """
    Formata a coluna 'Hora UTC' para um objeto de hora válido.
    
    Args:
    df (DataFrame): DataFrame contendo os dados a serem formatados.
    
    Returns:
    DataFrame: DataFrame com a coluna 'Hora UTC' formatada.
    """
    df['Hora UTC'] = df['Hora UTC'].str.replace(' UTC', '')
    df['Hora UTC'] = pd.to_datetime(df['Hora UTC'], format='%H%M').dt.time
    return df

# %%
def save_to_excel(df_header, df_data, output_path):
    """
    Salva os DataFrames formatados em um arquivo Excel com diferentes abas.
    
    Args:
    df_header (DataFrame): DataFrame contendo o cabeçalho extra.
    df_data (DataFrame): DataFrame contendo os dados formatados.
    output_path (str): Caminho para salvar o arquivo Excel.
    """
    with pd.ExcelWriter(output_path, engine='xlsxwriter') as writer:
        df_header.to_excel(writer, sheet_name='Header', index=False, header=False)
        df_data.to_excel(writer, sheet_name='Data', index=False)

# %%
def process_file(file_path, output_path):
    """
    Processa o arquivo de entrada e o transforma em uma planilha formatada.
    
    Args:
    file_path (str): Caminho para o arquivo CSV de entrada.
    output_path (str): Caminho para salvar o arquivo Excel formatado.
    """
    df_header, df_data = load_and_split(file_path)
    df_data = format_numeric_columns(df_data)
    df_data = format_time_column(df_data)
    save_to_excel(df_header, df_data, output_path)
    print("Arquivo Excel criado com sucesso!")

# %%
# Verificar e criar diretório para arquivos tratados
ensure_directory(DIR_TRATADOS)

# %%
# Exemplo de uso:
file_name = 'INMET_SE_SP_A701_SAO PAULO - MIRANTE_01-01-2024_A_31-07-2024.CSV'
file_path = os.path.join(DIR_TESTES, file_name)
output_file_name = 'INMET_SE_SP_A701_SAO PAULO - MIRANTE_01-01-2024_A_31-07-2024.xlsx'
output_path = os.path.join(DIR_TRATADOS, output_file_name)

process_file(file_path, output_path)

# %%
# Função principal para processar todos os arquivos .csv
def process_all_files():
    # Verificar e criar diretório principal para arquivos tratados
    ensure_directory(DIR_TRATADOS)

    # Percorrer todos os diretórios de anos
    for year_dir in os.listdir(DIR_ARQUIVOS):
        year_path = os.path.join(DIR_ARQUIVOS, year_dir)
        
        if os.path.isdir(year_path):
            # Criar o diretório correspondente em DADOS_TRATADOS
            year_tratado_dir = os.path.join(DIR_TRATADOS, year_dir)
            ensure_directory(year_tratado_dir)

            # Processar todos os arquivos .csv dentro do diretório do ano
            for file_name in os.listdir(year_path):
                if file_name.endswith('.csv'):
                    file_path = os.path.join(year_path, file_name)
                    output_file_name = file_name.replace('.csv', '.xlsx')
                    output_path = os.path.join(year_tratado_dir, output_file_name)
                    
                    process_file(file_path, output_path)

# %%
# Executar a automação
process_all_files()



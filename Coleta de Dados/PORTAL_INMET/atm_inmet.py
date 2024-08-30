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
# Mapeamento das colunas antigas para as novas
COLUMN_MAPPING = {
    'DATA (YYYY-MM-DD)': 'Data',
    'HORA (UTC)': 'Hora UTC',
    'PRECIPITAÇÃO TOTAL, HORÁRIO (mm)': 'PRECIPITAÇÃO TOTAL, HORÁRIO (mm)',
    'PRESSAO ATMOSFERICA AO NIVEL DA ESTACAO, HORARIA (mB)': 'PRESSAO ATMOSFERICA AO NIVEL DA ESTACAO, HORARIA (mB)',
    'PRESSÃO ATMOSFERICA MAX.NA HORA ANT. (AUT) (mB)': 'PRESSÃO ATMOSFERICA MAX.NA HORA ANT. (AUT) (mB)',
    'PRESSÃO ATMOSFERICA MIN. NA HORA ANT. (AUT) (mB)': 'PRESSÃO ATMOSFERICA MIN. NA HORA ANT. (AUT) (mB)',
    'RADIACAO GLOBAL (KJ/m²)': 'RADIACAO GLOBAL (Kj/m²)',
    'TEMPERATURA DO AR - BULBO SECO, HORARIA (°C)': 'TEMPERATURA DO AR - BULBO SECO, HORARIA (°C)',
    'TEMPERATURA DO PONTO DE ORVALHO (°C)': 'TEMPERATURA DO PONTO DE ORVALHO (°C)',
    'TEMPERATURA MÁXIMA NA HORA ANT. (AUT) (°C)': 'TEMPERATURA MÁXIMA NA HORA ANT. (AUT) (°C)',
    'TEMPERATURA MÍNIMA NA HORA ANT. (AUT) (°C)': 'TEMPERATURA MÍNIMA NA HORA ANT. (AUT) (°C)',
    'TEMPERATURA ORVALHO MAX. NA HORA ANT. (AUT) (°C)': 'TEMPERATURA ORVALHO MAX. NA HORA ANT. (AUT) (°C)',
    'TEMPERATURA ORVALHO MIN. NA HORA ANT. (AUT) (°C)': 'TEMPERATURA ORVALHO MIN. NA HORA ANT. (AUT) (°C)',
    'UMIDADE REL. MAX. NA HORA ANT. (AUT) (%)': 'UMIDADE REL. MAX. NA HORA ANT. (AUT) (%)',
    'UMIDADE REL. MIN. NA HORA ANT. (AUT) (%)': 'UMIDADE REL. MIN. NA HORA ANT. (AUT) (%)',
    'UMIDADE RELATIVA DO AR, HORARIA (%)': 'UMIDADE RELATIVA DO AR, HORARIA (%)',
    'VENTO, DIREÇÃO HORARIA (gr) (° (gr))': 'VENTO, DIREÇÃO HORARIA (gr) (° (gr))',
    'VENTO, RAJADA MAXIMA (m/s)': 'VENTO, RAJADA MAXIMA (m/s)',
    'VENTO, VELOCIDADE HORARIA (m/s)': 'VENTO, VELOCIDADE HORARIA (m/s)'
}

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

    header_lines = lines[:8]
    data_lines = lines[8:]

    df_header = pd.DataFrame([line.strip().split(';') for line in header_lines])
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
def format_columns(df):
    """
    Renomeia as colunas de acordo com o mapeamento e faz as formatações necessárias.
    """
    df = df.rename(columns=COLUMN_MAPPING)
    
    # Verificar se a coluna 'Hora UTC' existe após o mapeamento
    if 'Hora UTC' in df.columns:
        # Remover qualquer sufixo de ":00" ou " UTC"
        df['Hora UTC'] = df['Hora UTC'].str.replace(' UTC', '').str.replace(':00', '')
        
        try:
            # Tentar converter usando o formato de horas e minutos
            df['Hora UTC'] = pd.to_datetime(df['Hora UTC'], format='%H%M').dt.time
        except ValueError:
            # Se falhar, tentar a conversão sem formato específico
            df['Hora UTC'] = pd.to_datetime(df['Hora UTC'], errors='coerce').dt.time
    
    return df


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
    df_data = format_columns(df_data)  # Renomear e formatar as colunas
    df_data = format_numeric_columns(df_data)
    save_to_excel(df_header, df_data, output_path)
    print(f"Arquivo Excel criado com sucesso em: {output_path}")

# %%
# Função principal para processar todos os arquivos .csv
def process_all_files():
    ensure_directory(DIR_TRATADOS)

    for year_dir in os.listdir(DIR_ARQUIVOS):
        year_path = os.path.join(DIR_ARQUIVOS, year_dir)
        
        if os.path.isdir(year_path):
            year_tratado_dir = os.path.join(DIR_TRATADOS, year_dir)
            ensure_directory(year_tratado_dir)

            for file_name in os.listdir(year_path):
                if file_name.endswith('.CSV'):
                    file_path = os.path.join(year_path, file_name)
                    output_file_name = file_name.replace('.CSV', '.xlsx')
                    output_path = os.path.join(year_tratado_dir, output_file_name)
                    
                    process_file(file_path, output_path)

# %%
# Executar a automação
process_all_files()
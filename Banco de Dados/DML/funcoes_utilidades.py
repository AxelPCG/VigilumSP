import pandas as pd
import numpy as np
import os
from shapely.geometry import Point
from math import radians, cos, sin, sqrt, atan2


def extract_and_map_excel_data(file_path):
    """
    Esta função extrai os dados necessários de um arquivo Excel e os mapeia para os campos da tabela TBL_Previsao.
    
    Args:
    file_path (str): O caminho para o arquivo Excel.

    Returns:
    tuple: Contendo um dicionário com os dados mapeados e as coordenadas de latitude e longitude da folha "Header".
    """
    try:
        # Carregar o arquivo Excel
        xls = pd.ExcelFile(file_path)
        
        # Carregar a folha "Header" para extrair coordenadas
        df_header = pd.read_excel(xls, sheet_name="Header")
        
        # Extração das coordenadas de latitude e longitude
        latitude = df_header.loc[df_header.iloc[:, 0] == 'LATITUDE:', df_header.columns[1]].values[0]
        longitude = df_header.loc[df_header.iloc[:, 0] == 'LONGITUDE:', df_header.columns[1]].values[0]
        
        # Carregar a folha "Data" para extrair os dados meteorológicos
        df_data = pd.read_excel(xls, sheet_name="Data")
        
        # Mapeamento dos dados da folha "Data" para o formato da tabela TBL_Previsao
        mapped_data = {
            "Data": df_data['Data'],
            "Hora": df_data['Hora UTC'],
            "Temperatura_Max": df_data['TEMPERATURA MÁXIMA NA HORA ANT. (AUT) (°C)'],
            "Temperatura_Min": df_data['TEMPERATURA MÍNIMA NA HORA ANT. (AUT) (°C)'],
            "Umidade_Max": df_data['UMIDADE REL. MAX. NA HORA ANT. (AUT) (%)'],
            "Umidade_Min": df_data['UMIDADE REL. MIN. NA HORA ANT. (AUT) (%)'],
            "Velocidade_Vento": df_data['VENTO, VELOCIDADE HORARIA (m/s)'],
            "Volume_Precipitacao": df_data['PRECIPITAÇÃO TOTAL, HORÁRIO (mm)'],
            "Pressao_Atm": df_data['PRESSAO ATMOSFERICA AO NIVEL DA ESTACAO, HORARIA (mB)']
        }
        
        return mapped_data, latitude, longitude
    
    except FileNotFoundError:
        print(f"Arquivo não encontrado: {file_path}")
        return None, None, None
    
    except KeyError as e:
        print(f"Coluna não encontrada: {e}")
        return None, None, None
    
    except Exception as e:
        print(f"Ocorreu um erro ao processar o arquivo {file_path}: {e}")
        return None, None, None
    

def haversine_distance(lat1, lon1, lat2, lon2):
    """
    Calcula a distância entre dois pontos na superfície da Terra usando a fórmula de Haversine.
    As coordenadas devem ser fornecidas em graus decimais.
    
    Args:
    lat1, lon1: Latitude e Longitude do primeiro ponto.
    lat2, lon2: Latitude e Longitude do segundo ponto.
    
    Returns:
    float: Distância em quilômetros entre os dois pontos.
    """
    # Converte de graus para radianos
    lat1, lon1, lat2, lon2 = map(radians, [lat1, lon1, lat2, lon2])

    # Diferenças entre as coordenadas
    dlat = lat2 - lat1
    dlon = lon2 - lon1

    # Fórmula de Haversine
    a = sin(dlat / 2) ** 2 + cos(lat1) * cos(lat2) * sin(dlon / 2) ** 2
    c = 2 * atan2(sqrt(a), sqrt(1 - a))
    r = 6371  # Raio da Terra em quilômetros
    return c * r


def find_nearest_zone(lat, lon, df_zona):
    """
    Encontra a zona mais próxima baseada nas coordenadas fornecidas.

    Args:
    lat (float): Latitude extraída do arquivo Excel.
    lon (float): Longitude extraída do arquivo Excel.
    df_zona (DataFrame): DataFrame contendo as zonas e suas coordenadas centrais.

    Returns:
    int: O ID da zona mais próxima.
    """
    min_distance = float('inf')
    nearest_zone_id = None
    
    # Iterar sobre cada zona para encontrar a mais próxima
    for index, row in df_zona.iterrows():
        central_lat, central_lon = row['CORD_CENTRAL'].y, row['CORD_CENTRAL'].x
        distance = haversine_distance(lat, lon, central_lat, central_lon)
        
        if distance < min_distance:
            min_distance = distance
            nearest_zone_id = row['ID']
    
    return nearest_zone_id


def extract_and_convert_coordinates(lat_str, lon_str):
    """
    Converte as coordenadas de string para float, se necessário.
    
    Args:
    lat_str (str/float): Latitude.
    lon_str (str/float): Longitude.
    
    Returns:
    tuple: Latitude e Longitude convertidas para float.
    """
    # Verifica se as coordenadas já são float
    if isinstance(lat_str, str):
        lat_str = float(lat_str.replace(",", ".").strip())
    if isinstance(lon_str, str):
        lon_str = float(lon_str.replace(",", ".").strip())
    
    return lat_str, lon_str


def convert_numpy_types(value):
    """
    Converte valores de tipos NumPy para tipos de dados nativos do Python.
    
    Args:
    value: O valor a ser convertido.
    
    Returns:
    O valor convertido para um tipo nativo do Python.
    """
    if isinstance(value, (np.int64, np.int32)):
        return int(value)
    elif isinstance(value, (np.float64, np.float32)):
        return float(value)
    elif pd.isna(value):  # Para valores nulos
        return None
    else:
        return value


def convert_time_to_number(time_str):
    """
    Converte um valor de hora no formato 'HH:MM:SS' para um número inteiro que representa a hora.
    
    Args:
    time_str (str): Hora no formato 'HH:MM:SS'.
    
    Returns:
    int: Hora convertida para um número inteiro (apenas a hora, ignorando minutos e segundos).
    """
    if isinstance(time_str, str):
        hour = int(time_str.split(':')[0])  # Extrai apenas a parte da hora e converte para inteiro
        return hour
    else:
        return time_str  # Se já estiver em formato numérico, retorne como está


def handle_special_values(value):
    """
    Substitui valores especiais como -9999.0 e NaN por None para representar valores nulos.
    
    Args:
    value (float): O valor a ser tratado.
    
    Returns:
    float/None: Retorna None se o valor for -9999.0 ou NaN, caso contrário, retorna o valor original.
    """
    if pd.isna(value) or value == -9999.0:
        return None
    return value


def truncate_value(value, max_digits, decimal_places):
    """
    Trunca um valor para garantir que ele não exceda a precisão permitida.
    
    Args:
    value (float): O valor a ser truncado.
    max_digits (int): Número máximo de dígitos permitidos.
    decimal_places (int): Número máximo de casas decimais permitidas.
    
    Returns:
    float: O valor truncado ou None se o valor for None.
    """
    if value is None:
        return None
    
    # Limita a quantidade de casas decimais
    format_str = "{:." + str(decimal_places) + "f}"
    truncated_value = float(format_str.format(value))
    
    # Verifica se o número de dígitos totais excede max_digits
    if len(str(int(truncated_value)).replace('-', '')) > max_digits - decimal_places:
        raise ValueError(f"Valor {truncated_value} excede o limite de dígitos permitidos.")
    
    return truncated_value


def insert_data_to_tbl_previsao(connection, mapped_data, zona_id):
    """
    Insere os dados mapeados na tabela TBL_Previsao do banco de dados Oracle.
    
    Args:
    connection (cx_Oracle.Connection): Conexão ativa com o banco de dados Oracle.
    mapped_data (dict): Dados mapeados da folha "Data" do Excel.
    zona_id (int): ID da zona correspondente encontrada.
    
    Returns:
    None
    """
    cursor = connection.cursor()

    # Preparar o comando SQL para inserção
    insert_sql = """
    INSERT INTO TBL_Previsao (Zona_Id, Data, Hora, Temperatura_Max, Temperatura_Min, 
                              Umidade_Max, Umidade_Min, Velocidade_Vento, Volume_Precipitacao, Pressao_Atm)
    VALUES (:1, TO_DATE(:2, 'YYYY-MM-DD'), :3, :4, :5, :6, :7, :8, :9, :10)
    """
    
    # Preparar os dados para inserção em batch
    data_to_insert = []
    for i in range(len(mapped_data['Data'])):
        record = (
            zona_id,
            mapped_data['Data'].iloc[i],
            convert_time_to_number(mapped_data['Hora'].iloc[i]),
            truncate_value(handle_special_values(convert_numpy_types(mapped_data['Temperatura_Max'].iloc[i])), 5, 2),
            truncate_value(handle_special_values(convert_numpy_types(mapped_data['Temperatura_Min'].iloc[i])), 5, 2),
            truncate_value(handle_special_values(convert_numpy_types(mapped_data['Umidade_Max'].iloc[i])), 5, 2),
            truncate_value(handle_special_values(convert_numpy_types(mapped_data['Umidade_Min'].iloc[i])), 5, 2),
            truncate_value(handle_special_values(convert_numpy_types(mapped_data['Velocidade_Vento'].iloc[i])), 5, 2),
            truncate_value(handle_special_values(convert_numpy_types(mapped_data['Volume_Precipitacao'].iloc[i])), 5, 2),
            truncate_value(handle_special_values(convert_numpy_types(mapped_data['Pressao_Atm'].iloc[i])), 5, 2)
        )
        data_to_insert.append(record)
    
    # Executar a inserção em batch
    cursor.executemany(insert_sql, data_to_insert)
    
    # Confirmar as alterações no banco de dados
    connection.commit()
    
    # Fechar o cursor
    cursor.close()
    
    print(f"{len(data_to_insert)} registros inseridos com sucesso na tabela TBL_Previsao.")
    
    
def process_all_excel_files(directory, df_zona, connection):
    """
    Processa todos os arquivos Excel em um diretório, extraindo e mapeando os dados para a tabela TBL_Previsao.
    
    Args:
    directory (str): O caminho do diretório contendo os arquivos Excel.
    df_zona (DataFrame): DataFrame contendo as zonas e suas coordenadas centrais.
    connection (cx_Oracle.Connection): Conexão ativa com o banco de dados Oracle.
    
    Returns:
    None
    """
    for filename in os.listdir(directory):
        if filename.endswith(".xlsx"):
            file_path = os.path.join(directory, filename)
            print(f"Processando arquivo: {file_path}")
            
            # Extrair e mapear os dados do Excel
            mapped_data, latitude, longitude = extract_and_map_excel_data(file_path)
            
            if mapped_data is None:
                continue  # Pula arquivos que não puderam ser processados
            
            # Converter as coordenadas extraídas do Excel
            latitude_float, longitude_float = extract_and_convert_coordinates(latitude, longitude)
            
            # Encontrar a zona mais próxima
            nearest_zone_id = find_nearest_zone(latitude_float, longitude_float, df_zona)
            
            # Inserir os dados no banco de dados
            insert_data_to_tbl_previsao(connection, mapped_data, nearest_zone_id)

    print("Processamento concluído para todos os arquivos.")
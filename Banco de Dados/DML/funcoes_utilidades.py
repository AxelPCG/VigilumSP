import pandas as pd
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
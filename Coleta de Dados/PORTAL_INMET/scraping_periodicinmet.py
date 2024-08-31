# %% [markdown]
# ### Bibliotecas

# %%
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.common.exceptions import NoSuchElementException, TimeoutException, WebDriverException
from time import sleep
import os
import zipfile
from datetime import date

# %% [markdown]
# ### Diretórios

# %%
# Estrutura de diretórios
BASE_DIR = os.path.dirname(os.path.abspath(__file__))
DIR_ARQUIVOS = os.path.join(BASE_DIR, "DADOS_METEREOLOGICOS_ESTADO_SP")

# %% [markdown]
# ### Scraping

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
# Função para abrir o navegador no site da inmet
def web_chrome():
    try:
        navegadoropt = webdriver.ChromeOptions()
        navegadoropt.add_experimental_option("prefs", {
            'download.default_directory': BASE_DIR,  # Definir o diretório de download
            "download.prompt_for_download": False,
            "download.directory_upgrade": True
        })
        navegadoropt.add_argument("--disable-popup-blocking")
        navegador = webdriver.Chrome(options=navegadoropt)
        navegador.get("https://portal.inmet.gov.br/dadoshistoricos")
        sleep(20)  # Esperar 20 segundos para garantir que a página carregue completamente
        return navegador
    except WebDriverException as e:
        print(f"Erro ao iniciar o navegador: {e}")
        return None

# %%
# Função para encontrar e baixar o último arquivo zip
def baixar_ultimo_arquivo_zip(navegador):
    if navegador is None:
        print("Navegador não iniciado corretamente.")
        return
    
    try:
        # Encontre todos os elementos <a> que possuem links para arquivos zip
        links = navegador.find_elements(By.CSS_SELECTOR, "article.post-preview a")
        
        # Verifique se encontrou algum link
        if not links:
            print("Nenhum link encontrado na página.")
            return
        
        # O último link na lista deve ser o link mais recente
        ultimo_link = links[-1]
        
        # Clique no último link para iniciar o download
        ultimo_link.click()
        print(f"Baixando arquivo: {ultimo_link.get_attribute('href')}")
        sleep(10)  # Aguarde um tempo para garantir que o download seja iniciado
        print('Arquivo baixado com sucesso!')
        
    except NoSuchElementException as e:
        print(f"Erro ao localizar elementos na página: {e}")
    except TimeoutException as e:
        print(f"Tempo de espera excedido: {e}")
    except WebDriverException as e:
        print(f"Erro no WebDriver: {e}")
    finally:
        # Fechar o navegador após o download, mesmo que ocorra um erro
        navegador.quit()

# %%
# Função para extrair o arquivo ZIP para uma pasta com o mesmo nome do arquivo ZIP
def extrair_arquivo_zip(caminho_arquivo_zip, destino):
    try:
        # Nome da pasta será o nome do arquivo .zip sem a extensão
        nome_pasta = os.path.splitext(os.path.basename(caminho_arquivo_zip))[0]
        caminho_pasta_destino = os.path.join(destino, nome_pasta)
        
        # Criar a pasta de destino se não existir
        if not os.path.exists(caminho_pasta_destino):
            os.makedirs(caminho_pasta_destino)
        
        # Extrair os arquivos para a pasta criada
        with zipfile.ZipFile(caminho_arquivo_zip, 'r') as zip_ref:
            zip_ref.extractall(caminho_pasta_destino)
        print(f"Arquivo {caminho_arquivo_zip} extraído com sucesso para {caminho_pasta_destino}.")
    except zipfile.BadZipFile as e:
        print(f"Erro ao extrair {caminho_arquivo_zip}: {e}")
    except Exception as e:
        print(f"Ocorreu um erro ao extrair {caminho_arquivo_zip}: {e}")

# %%
def main():
    ensure_directory(DIR_ARQUIVOS)
    
    # iniciando processo:
    navegador = web_chrome()
    baixar_ultimo_arquivo_zip(navegador)
    
    ano_atual = date.today().year
    ano_atual = str(date.today().year)
    ano_atual_arquivo = ano_atual+'.zip'
    caminho_arquivo = os.path.join(BASE_DIR, ano_atual_arquivo)
    
    #Extrai arquivo na pasta
    extrair_arquivo_zip(caminho_arquivo, DIR_ARQUIVOS)

# %%
if __name__ == "__main__":
    main()



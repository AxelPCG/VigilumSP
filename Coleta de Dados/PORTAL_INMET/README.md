## ARQUIVOS DE DADOS METEOROLÓGICOS DO ESTADO DE SP (PORTAL INMET)

- Os arquivos são .csvs de cidades de SP que fazem a coleta de dados metereologicos. O arquivo possui mais de 100mb, então está disponível para download no link abaixo:

LINK: https://fiapcom-my.sharepoint.com/:u:/g/personal/rm557311_fiap_com_br/Ea1jcqFliBJIu-o7A-OsQWQBz019_RXDYTBWiiepv1eKYw?e=PIaVI0

- O script 'atm_inmet.ipynb' ou 'atm_inmet.py' já filtra e prepara os dados para inserção no banco de dados a partir da pasta extraída no arquivo .zip acima (deixe a pasta extraida do zip no mesmo diretório do script).

- O arquivo .xlsx é um exemplo de como dados ficam após o tratamento.

- Os Scripts 'scraping_periodicinmet.ipynb' e 'scraping_periodicinmet.py' fazem o scraping do site do INMET disponibilizado no link: https://portal.inmet.gov.br/dadoshistoricos
  O scraping sempre faz o download do ultimo arquivo disponibilziado, extrai e organiza os dados de forma automática. Pode ser adicionado em uma máquina virtual para recorrência do upload dos dados históricos mensais.

### **RNO 1: Gestão de Municípios, Zonas e Distritos**

- **Descrição**: Esta regra de negócio define a estrutura hierárquica entre municípios, zonas e distritos, garantindo que cada zona esteja corretamente vinculada a um município e cada distrito a uma zona. Isso permitirá análises geoespaciais detalhadas e localizadas.

- **Tabelas Envolvidas**: `TBL_Municipio`, `TBL_Zona`, `TBL_Distrito`

- **Detalhamento**:
  - **TBL_Municipio**:
    - Colunas: `CD_MUN` (NUMBER, PK), `NM_MUN` (VARCHAR2(100)), `SG_Estado` (CHAR(2)), `AREA_KM2` (FLOAT(10)), `geometry` (SDO_GEOMETRY), `CORD_CENTRAL` (SDO_GEOMETRY)
    - Chave Primária: `TBL_Municipio_PK` (CD_MUN)
  - **TBL_Zona**:
    - Colunas: `Id` (NUMBER, PK), `Nome` (VARCHAR2(50)), `geometry` (SDO_GEOMETRY), `CORD_CENTRAL` (SDO_GEOMETRY), `Municipio_Id` (NUMBER, FK)
    - Chave Primária: `TBL_Zona_PK` (Id)
    - Chave Estrangeira: `TBL_Zona_TBL_Municipio_FK` (`Municipio_Id`) referenciando `TBL_Municipio` (`CD_MUN`)
  - **TBL_Distrito**:
    - Colunas: `CD_DIST` (NUMBER, PK), `NM_DIST` (VARCHAR2(100)), `geometry` (SDO_GEOMETRY), `CORD_CENTRAL` (SDO_GEOMETRY), `Macro_Id` (NUMBER, FK)
    - Chave Primária: `TBL_Distrito_PK` (CD_DIST)
    - Chave Estrangeira: `TBL_Distrito_TBL_Zona_FK` (`Macro_Id`) referenciando `TBL_Zona` (`Id`)

- **Relacionamento**:
  - Cada município (`TBL_Municipio`) pode ter várias zonas associadas (`TBL_Zona`), e cada zona pode ter vários distritos (`TBL_Distrito`) associados. 
  - **Cardinalidade**: 
    - 1:N (Um para Muitos) - Um município pode ter muitas zonas.
    - 1:N (Um para Muitos) - Uma zona pode ter muitos distritos.

---

### **RNO 2: Coleta e Armazenamento de Dados Meteorológicos**

- **Descrição**: Esta regra estabelece como os dados meteorológicos, como temperatura, umidade, ventos, precipitação, nuvens e pressão atmosférica, devem ser coletados e armazenados para análise futura.

- **Tabelas Envolvidas**: `TBL_Temperatura`, `TBL_Umidade`, `TBL_Ventania`, `TBL_Precipitacao`, `TBL_Nuvem`, `TBL_Pressao`

- **Detalhamento**:
  - **TBL_Temperatura**:
    - Colunas: `Id` (NUMBER, PK), `Data` (DATE), `Hora` (NUMBER(2)), `Temperatura_Max` (FLOAT(3)), `Temperatura_Min` (FLOAT(3))
    - Chave Primária: `TBL_Temperatura_PK` (Id)
  - **TBL_Umidade**:
    - Colunas: `Id` (NUMBER, PK), `Data` (DATE), `Hora` (NUMBER(2)), `Umidade_Max` (FLOAT(3)), `Umidade_Min` (FLOAT(3))
    - Chave Primária: `TBL_Umidade_PK` (Id)
  - **TBL_Ventania**:
    - Colunas: `Id` (NUMBER, PK), `Data` (DATE), `Hora` (NUMBER(2)), `Velocidade` (FLOAT(3))
    - Chave Primária: `TBL_Ventania_PK` (Id)
  - **TBL_Precipitacao**:
    - Colunas: `Id` (NUMBER, PK), `Data` (DATE), `Hora` (NUMBER(2)), `Volume` (FLOAT(3))
    - Chave Primária: `TBL_Precipitacao_PK` (Id)
  - **TBL_Nuvem**:
    - Colunas: `Id` (NUMBER, PK), `Data` (DATE), `Hora` (NUMBER(2)), `Tipo` (VARCHAR2(50))
    - Chave Primária: `TBL_Nuvem_PK` (Id)
  - **TBL_Pressao**:
    - Colunas: `Id` (NUMBER, PK), `Data` (DATE), `Hora` (NUMBER(2)), `Pressao_atm` (FLOAT(3))
    - Chave Primária: `TBL_Pressao_PK` (Id)

- **Relacionamento**:
  - As tabelas de dados meteorológicos armazenam registros diários, onde cada registro é independente e não diretamente relacionado a uma região específica nesta estrutura. No entanto, podem ser utilizados em conjunto para análises agregadas ou inseridos em tabelas como `TBL_Previsao`.
  - **Cardinalidade**: Não há relacionamentos diretos entre essas tabelas na estrutura atual, mas elas podem ser relacionadas a outras tabelas, como `TBL_Previsao`, conforme necessário.

---

### **RNO 3: Criação e Armazenamento de Previsões Meteorológicas**

- **Descrição**: Esta regra define como as previsões meteorológicas devem ser geradas com base nos dados históricos e atuais, armazenadas e associadas a uma zona específica.

- **Tabelas Envolvidas**: `TBL_Previsao`, `TBL_Previsao_Futura`, `TBL_Zona`, `TBL_Temperatura`, `TBL_Umidade`, `TBL_Ventania`, `TBL_Precipitacao`, `TBL_Nuvem`, `TBL_Pressao`

- **Detalhamento**:
  - **TBL_Previsao**:
    - Colunas: `Id` (NUMBER, PK), `Zona_Id` (NUMBER, FK), `Data` (DATE), `Hora` (NUMBER(2)), `Temperatura_Id` (NUMBER, FK), `Umidade_Id` (NUMBER, FK), `Ventania_Id` (NUMBER, FK), `Precipitacao_Id` (NUMBER, FK), `Nuvem_Id` (NUMBER, FK), `Pressao_Id` (NUMBER, FK)
    - Chave Primária: `TBL_Previsao_PK` (Id)
    - Chaves Estrangeiras:
      - `TBL_Previsao_TBL_Zona_FK` (`Zona_Id`) referenciando `TBL_Zona` (`Id`)
      - `TBL_Previsao_TBL_Temperatura_FK` (`Temperatura_Id`) referenciando `TBL_Temperatura` (`Id`)
      - `TBL_Previsao_TBL_Umidade_FK` (`Umidade_Id`) referenciando `TBL_Umidade` (`Id`)
      - `TBL_Previsao_TBL_Ventania_FK` (`Ventania_Id`) referenciando `TBL_Ventania` (`Id`)
      - `TBL_Previsao_TBL_Precipitacao_FK` (`Precipitacao_Id`) referenciando `TBL_Precipitacao` (`Id`)
      - `TBL_Previsao_TBL_Nuvem_FK` (`Nuvem_Id`) referenciando `TBL_Nuvem` (`Id`)
      - `TBL_Previsao_TBL_Pressao_FK` (`Pressao_Id`) referenciando `TBL_Pressao` (`Id`)
  - **TBL_Previsao_Futura**:
    - Colunas: `Id` (NUMBER, PK), `Zona_Id` (NUMBER, FK), `Data_Referencia` (DATE), `Data_Futura` (DATE), `Hora` (NUMBER(2)), `Temperatura_Max` (FLOAT(3)), `Temperatura_Min` (FLOAT(3)), `Umidade_Max` (FLOAT(3)), `Umidade_Min` (FLOAT(3)), `Ventania` (FLOAT(3)), `Precipitacao` (FLOAT(3)), `Nuvem` (VARCHAR2(50)), `Pressao` (FLOAT(3))
    - Chave Primária: `TBL_Previsao_Futura_PK` (Id)
    - Chave Estrangeira:
      - `TBL_Previsao_Futura_TBL_Zona_FK` (`Zona_Id`) referenciando `TBL_Zona` (`Id`)

- **Relacionamento**:
  - A tabela `TBL_Previsao` consolida dados de previsão para zonas específicas, vinculando-os a registros meteorológicos existentes.
  - A tabela `TBL_Previsao_Futura` armazena previsões futuras simplificadas, relacionadas a uma zona específica.
  - **Cardinalidade**: 
    - 1:N (Um para Muitos) - Uma zona pode ter muitas previsões associadas.
    - 1:N (Um para Muitos) - Muitos dados meteorológicos podem ser utilizados para gerar várias previsões.

---

### **RNO 4: Geração e Gestão de Alertas Meteorológicos**

- **Descrição**: Esta regra estabelece como os alertas meteorológicos devem ser gerados com base nas previsões e condições atuais, e como devem ser armazenados e disseminados.

- **Tabelas Envolvidas**: `TBL_Aviso`, `TBL_Zona`

- **Detalhamento**:
  - **TBL_Aviso**:
    - Colunas: `Id` (NUMBER, PK), `Data` (DATE), `Zona_Id` (NUMBER, FK), `Tipo` (VARCHAR2(50)), `Severidade` (INTEGER), `Mensagem` (VARCHAR2(255))
    - Chave Primária: `TBL_Aviso_PK` (Id)
    - Chave Estrangeira: 
      - `TBL_Aviso_TBL_Zona_FK` (`Zona_Id`) referenciando `TBL_Zona` (`Id`)

- **Relacionamento**:
  - Cada aviso meteorológico deve estar vinculado a uma zona específica através da chave estrangeira `Zona_Id`.
  - **Cardinalidade**: 1:N (Um para Muitos) - Uma zona pode ter muitos avisos meteorológicos.

---

### **RNO 5: Integração com Sistemas Externos**

- **Descrição**: Esta regra estabelece como o sistema deve integrar-se com outras plataformas ou serviços, como APIs de previsão do tempo ou sistemas de alerta, garantindo que as informações estejam sempre atualizadas e precisas.

- **Tabelas Envolvidas**: `TBL_Integracao`, `TBL_Zona`

- **Detalhamento**:
  - **TBL_Integracao**:
    - Colunas: `Id` (NUMBER, PK), `Zona_Id` (NUMBER, FK), `Fonte` (VARCHAR2(100)), `Data_Sincronizacao` (DATE), `Status` (VARCHAR2(50))
    - Chave Primária: `TBL_Integracao_PK` (Id)
    - Chave Estrangeira: 
      - `TBL_Integracao_TBL_Zona_FK` (`Zona_Id`) referenciando `TBL_Zona` (`Id`)

- **Relacionamento**:
  - Cada integração deve estar associada a uma zona, permitindo o rastreamento da origem dos dados meteorológicos.
  - **Cardinalidade**: 1:N (Um para Muitos) - Uma zona pode estar associada a muitas integrações de fontes externas.

---

### **RNO 6: Armazenamento e Gerenciamento de Dados Históricos**

- **Descrição**: Esta regra estabelece como os dados meteorológicos históricos devem ser armazenados, acessados e utilizados para melhorar as previsões futuras e análises de risco.

- **Tabelas Envolvidas**: `TBL_Temperatura`, `TBL_Umidade`, `TBL_Ventania`, `TBL_Precipitacao`, `TBL_Nuvem`, `TBL_Pressao`, `TBL_Previsao`

- **Detalhamento**:
  - **Armazenamento**:
    - Todos os dados meteorológicos diários devem ser mantidos em armazenamento de longo prazo para permitir análises históricas e identificação de padrões.
    - Deve haver uma estratégia de arquivamento para dados antigos que ainda possam ser relevantes para análises de tendências de longo prazo.
  - **Acesso e Uso**:
    - Os dados históricos devem estar disponíveis para cálculos de médias, desvios e outras estatísticas que possam ajudar a gerar previsões mais acuradas.
    - A tabela `TBL_Previsao` deve incluir uma referência aos dados históricos usados para gerar cada previsão específica.

- **Relacionamento**:
  - Os dados meteorológicos históricos devem estar relacionados à `TBL_Previsao` de forma que seja possível rastrear quais dados históricos influenciaram uma previsão específica.
  - **Cardinalidade**: 1:N (Um para Muitos) - Muitos registros de previsão podem utilizar os mesmos dados históricos.

---

### **RNO 7: Segurança e Controle de Acesso**

- **Descrição**: Define as regras para segurança dos dados, incluindo autenticação, autorização e controle de acesso às diferentes partes do sistema.

- **Tabelas Envolvidas**: `TBL_Usuario`, `TBL_Permissao`

- **Detalhamento**:
  - **TBL_Usuario**:
    - Colunas: `Id` (NUMBER, PK), `Nome` (VARCHAR2(50)), `Email` (VARCHAR2(100)), `Senha_Hash` (VARCHAR2(255)), `Data_Criacao` (DATE)
    - Chave Primária: `TBL_Usuario_PK` (Id)
  - **TBL_Permissao**:
    - Colunas: `Id` (NUMBER, PK), `Usuario_Id` (NUMBER, FK), `Recurso` (VARCHAR2(50)), `Permissao_Tipo` (VARCHAR2(50))
    - Chave Primária: `TBL_Permissao_PK` (Id)
    - Chave Estrangeira:
      - `TBL_Permissao_TBL_Usuario_FK` (`Usuario_Id`) referenciando `TBL_Usuario` (`Id`)

- **Relacionamento**:
  - Cada usuário (`TBL_Usuario`) pode ter várias permissões (`TBL_Permissao`), controlando o acesso a diferentes recursos do sistema (e.g., leitura, escrita, administração).
  - **Cardinalidade**: 1:N (Um para Muitos) - Um usuário pode ter muitas permissões.

---

### **RNO 8: Auditoria e Rastreamento de Atividades**

- **Descrição**: Estabelece como as atividades dos usuários e as operações críticas no sistema devem ser monitoradas e registradas para garantir a integridade e segurança dos dados.

- **Tabelas Envolvidas**: `TBL_Auditoria`

- **Detalhamento**:
  - **TBL_Auditoria**:
    - Colunas: `Id` (NUMBER, PK), `Usuario_Id` (NUMBER, FK), `Acao` (VARCHAR2(255)), `Data_Hora` (TIMESTAMP), `Detalhes` (CLOB)
    - Chave Primária: `TBL_Auditoria_PK` (Id)
    - Chave Estrangeira:
      - `TBL_Auditoria_TBL_Usuario_FK` (`Usuario_Id`) referenciando `TBL_Usuario` (`Id`)

- **Relacionamento**:
  - Cada ação de usuário que afete dados críticos deve ser registrada na tabela `TBL_Auditoria` para futura referência e controle.
  - **Cardinalidade**: 1:N (Um para Muitos) - Um usuário pode realizar muitas ações que serão auditadas.

---

### **RNO 9: Escalabilidade e Otimização de Performance**

- **Descrição**: Define as práticas para garantir que o sistema seja escalável e opere com alta performance mesmo com grandes volumes de dados meteorológicos e usuários simultâneos.

- **Tabelas Envolvidas**: Todas as tabelas podem ser impactadas por práticas de escalabilidade e otimização.

- **Detalhamento**:
  - **Indexação**:
    - As tabelas de dados meteorológicos e previsões devem ser indexadas por `Zona_Id` e `Data` para garantir consultas rápidas.
  - **Particionamento**:
    - O particionamento das tabelas de dados históricos deve ser implementado para facilitar a gestão de grandes volumes de dados, possibilitando a separação por ano, mês ou zonas.
  - **Cache**:
    - Implementação de caching para previsões mais acessadas, reduzindo a carga de consulta diretamente ao banco de dados.

- **Relacionamento**:
  - As estratégias de escalabilidade devem ser aplicadas de maneira uniforme em todo o sistema, assegurando que cada componente possa suportar crescimento sem perda de desempenho.

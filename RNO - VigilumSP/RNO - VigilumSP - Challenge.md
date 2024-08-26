
### **RNO 1: Gestão de Macro-Regiões e Regiões**

- **Descrição**: Esta regra de negócio define a estrutura hierárquica entre as regiões e as macro-regiões, garantindo que cada região esteja corretamente vinculada a uma macro-região, o que permitirá análises agregadas e localizadas.

- **Tabelas Envolvidas**: TBL_Macro_Regiao, TBL_Regiao

- **Detalhamento**:
  - **TBL_Macro_Regiao**:
    - Colunas: `Id` (ROWID, PK), `Nome` (VARCHAR2(50))
    - Chave Primária: `TBL_Macro_Regiao_PK` (Id)
  - **TBL_Regiao**:
    - Colunas: `Id` (ROWID, PK), `Nome` (VARCHAR2(50)), `Macro_id` (ROWID, FK)
    - Chave Primária: `TBL_Regioes_PK` (Id)
    - Chave Estrangeira: `TBL_Regiao_TBL_Macro_Regiao_FK` (Macro_id) referenciando `TBL_Macro_Regiao` (Id)

- **Relacionamento**:
  - Cada macro-região (`TBL_Macro_Regiao`) pode ter várias regiões associadas (`TBL_Regiao`), mas cada região pertence a uma única macro-região.
  - **Cardinalidade**: 1:N (Um para Muitos) - Uma macro-região pode ter muitas regiões.
 
### **RNO 2: Coleta e Armazenamento de Dados Meteorológicos**

- **Descrição**: Esta regra estabelece como os dados meteorológicos, como temperatura, umidade, ventos, precipitação e nuvens, devem ser coletados e armazenados, vinculados às respectivas regiões para análise futura.

- **Tabelas Envolvidas**: `TBL_Temperatura`, `TBL_Humidade`, `TBL_Ventania`, `TBL_Precipitacao`, `TBL_Nuvem`, `TBL_Regiao`

- **Detalhamento**:
  - **TBL_Temperatura**:
    - Colunas: `Id` (ROWID, PK), `Data` (DATE), `Regiao_id` (ROWID, FK), `Dia_1` a `Dia_7` (FLOAT)
    - Chave Primária: `TBL_Temperatura_PK` (Id)
    - Chave Estrangeira: `TBL_Temperatura_TBL_Regiao_FK` (Regiao_id) referenciando `TBL_Regiao` (Id)
  - **TBL_Humidade**:
    - Colunas: `Id` (ROWID, PK), `Data` (DATE), `Regiao_id` (ROWID, FK), `Dia_1` a `Dia_7` (FLOAT)
    - Chave Primária: `TBL_Humidade_PK` (Id)
    - Chave Estrangeira: `TBL_Humidade_TBL_Regiao_FK` (Regiao_id) referenciando `TBL_Regiao` (Id)
  - **TBL_Ventania**:
    - Colunas: `Id` (ROWID, PK), `Data` (DATE), `Regiao_id` (ROWID, FK), `Dia_1` a `Dia_7` (FLOAT)
    - Chave Primária: `TBL_Ventania_PK` (Id)
    - Chave Estrangeira: `TBL_Ventania_TBL_Regiao_FK` (Regiao_id) referenciando `TBL_Regiao` (Id)
  - **TBL_Precipitacao**:
    - Colunas: `Id` (ROWID, PK), `Data` (DATE), `Regiao_id` (ROWID, FK), `Dia_1` a `Dia_7` (FLOAT)
    - Chave Primária: `TBL_Precipitacao_PK` (Id)
    - Chave Estrangeira: `TBL_Precipitacao_TBL_Regiao_FK` (Regiao_id) referenciando `TBL_Regiao` (Id)
  - **TBL_Nuvem**:
    - Colunas: `Id` (ROWID, PK), `Data` (DATE), `Regiao_id` (ROWID, FK), `Dia_1` a `Dia_7` (VARCHAR2(50))
    - Chave Primária: `TBL_Nuvem_PK` (Id)
    - Chave Estrangeira: `TBL_Nuvem_TBL_Regiao_FK` (Regiao_id) referenciando `TBL_Regiao` (Id)

- **Relacionamento**:
  - Cada tabela de dados meteorológicos (`TBL_Temperatura`, `TBL_Humidade`, etc.) está relacionada a uma região específica através do campo `Regiao_id`.
  - **Cardinalidade**: N:1 (Muitos para Um) - Muitos registros de dados meteorológicos podem estar associados a uma única região.
 
### **RNO 3: Criação e Armazenamento de Previsões Meteorológicas**

- **Descrição**: Esta regra define como as previsões meteorológicas devem ser geradas com base nos dados históricos e atuais, armazenadas e associadas a uma região específica.

- **Tabelas Envolvidas**: `TBL_Previsao`, `TBL_Regiao`

- **Detalhamento**:
  - **TBL_Previsao**:
    - Colunas: `Id` (ROWID, PK), `Data` (DATE), `Regiao_id` (ROWID, FK), `Temperatura_id` (ROWID, FK), `Ventania_id` (ROWID, FK), `Humidade_id` (ROWID, FK), `Precipitacao_id` (ROWID, FK), `Nuvem_id` (ROWID, FK)
    - Chave Primária: `TBL_Previsao_PK` (Id)
    - Chaves Estrangeiras:
      - `TBL_Previsao_TBL_Regiao_FK` (`Regiao_id`) referenciando `TBL_Regiao` (Id)
      - `TBL_Previsao_TBL_Temperatura_FK` (`Temperatura_id`) referenciando `TBL_Temperatura` (Id)
      - `TBL_Previsao_TBL_Ventania_FK` (`Ventania_id`) referenciando `TBL_Ventania` (Id)
      - `TBL_Previsao_TBL_Humidade_FK` (`Humidade_id`) referenciando `TBL_Humidade` (Id)
      - `TBL_Previsao_TBL_Precipitacao_FK` (`Precipitacao_id`) referenciando `TBL_Precipitacao` (Id)
      - `TBL_Previsao_TBL_Nuvem_FK` (`Nuvem_id`) referenciando `TBL_Nuvem` (Id)

- **Relacionamento**:
  - A tabela `TBL_Previsao` deve consolidar os dados de previsão meteorológica, vinculando-os aos dados diários nas tabelas de meteorologia (`TBL_Temperatura`, `TBL_Ventania`, etc.).
  - **Cardinalidade**: N:1 (Muitos para Um) - Muitas previsões podem ser geradas para uma única região com base nos dados meteorológicos coletados.

### **RNO 4: Geração e Gestão de Alertas Meteorológicos**

- **Descrição**: Esta regra estabelece como os alertas meteorológicos devem ser gerados com base nas previsões e condições atuais, e como devem ser armazenados e disseminados.

- **Tabelas Envolvidas**: `TBL_Aviso`, `TBL_Regiao`

- **Detalhamento**:
  - **TBL_Aviso**:
    - Colunas: `Id` (ROWID, PK), `Data` (DATE), `Regiao_id` (ROWID, FK), `Tipo` (VARCHAR2(50)), `Severidade` (INTEGER), `Mensagem` (VARCHAR2(255))
    - Chave Primária: `TBL_Aviso_PK` (Id)
    - Chave Estrangeira: `TBL_Aviso_TBL_Regiao_FK` (`Regiao_id`) referenciando `TBL_Regiao` (Id)

- **Relacionamento**:
  - Cada aviso deve estar vinculado a uma região específica através da chave estrangeira `Regiao_id`.
  - **Cardinalidade**: N:1 (Muitos para Um) - Muitos avisos podem ser emitidos para uma única região.

### **RNO 5: Integração com Sistemas Externos**

- **Descrição**: Esta regra estabelece como o sistema deve integrar-se com outras plataformas ou serviços, como APIs de previsão do tempo ou sistemas de alerta, garantindo que as informações estejam sempre atualizadas e precisas.

- **Tabelas Envolvidas**: `TBL_Integracao`, `TBL_Regiao`

- **Detalhamento**:
  - **TBL_Integracao**:
    - Colunas: `Id` (ROWID, PK), `Regiao_id` (ROWID, FK), `Fonte` (VARCHAR2(100)), `Data_Sincronizacao` (DATE), `Status` (VARCHAR2(50))
    - Chave Primária: `TBL_Integracao_PK` (Id)
    - Chave Estrangeira: `TBL_Integracao_TBL_Regiao_FK` (`Regiao_id`) referenciando `TBL_Regiao` (Id)

- **Relacionamento**:
  - Cada integração deve estar associada a uma região, permitindo o rastreamento da origem dos dados meteorológicos.
  - **Cardinalidade**: N:1 (Muitos para Um) - Muitas integrações podem ocorrer para uma única região, dependendo da quantidade de fontes externas.

### **RNO 6: Armazenamento e Gerenciamento de Dados Históricos**

- **Descrição**: Esta regra estabelece como os dados meteorológicos históricos devem ser armazenados, acessados e utilizados para melhorar as previsões futuras e análises de risco.

- **Tabelas Envolvidas**: `TBL_Temperatura`, `TBL_Humidade`, `TBL_Ventania`, `TBL_Precipitacao`, `TBL_Nuvem`, `TBL_Previsao`

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

### **RNO 7: Integração com Sistemas de Terceiros**

- **Descrição**: Esta regra define como o sistema deve se integrar com APIs externas e outras fontes de dados para enriquecer as previsões e avisos meteorológicos.

- **Tabelas Envolvidas**: Dependente das APIs utilizadas, pode-se criar tabelas como `TBL_API_Log`, `TBL_External_Data`.

- **Detalhamento**:
  - **TBL_API_Log**:
    - Colunas: `Id` (ROWID, PK), `API_Nome` (VARCHAR2(50)), `Chamada_Data` (DATE), `Status` (VARCHAR2(50)), `Resposta` (CLOB)
    - Chave Primária: `TBL_API_Log_PK` (Id)
  - **TBL_External_Data**:
    - Colunas: `Id` (ROWID, PK), `Fonte_Nome` (VARCHAR2(50)), `Data_Coleta` (DATE), `Dado_Tipo` (VARCHAR2(50)), `Valor` (FLOAT)
    - Chave Primária: `TBL_External_Data_PK` (Id)

- **Relacionamento**:
  - A tabela `TBL_API_Log` deve registrar todas as interações com APIs externas para auditoria e monitoramento, enquanto `TBL_External_Data` deve armazenar os dados específicos obtidos dessas fontes.
  - **Cardinalidade**: N:M (Muitos para Muitos) - Muitas chamadas de API podem retornar muitos conjuntos de dados diferentes.

---

### **RNO 8: Segurança e Controle de Acesso**

- **Descrição**: Define as regras para segurança dos dados, incluindo autenticação, autorização e controle de acesso às diferentes partes do sistema.

- **Tabelas Envolvidas**: `TBL_Usuario`, `TBL_Permissao`

- **Detalhamento**:
  - **TBL_Usuario**:
    - Colunas: `Id` (ROWID, PK), `Nome` (VARCHAR2(50)), `Email` (VARCHAR2(100)), `Senha_Hash` (VARCHAR2(255)), `Data_Criacao` (DATE)
    - Chave Primária: `TBL_Usuario_PK` (Id)
  - **TBL_Permissao**:
    - Colunas: `Id` (ROWID, PK), `Usuario_id` (ROWID, FK), `Recurso` (VARCHAR2(50)), `Permissao_Tipo` (VARCHAR2(50))
    - Chave Primária: `TBL_Permissao_PK` (Id)
    - Chave Estrangeira: `TBL_Permissao_TBL_Usuario_FK` (`Usuario_id`) referenciando `TBL_Usuario` (Id)

- **Relacionamento**:
  - Cada usuário (`TBL_Usuario`) pode ter várias permissões (`TBL_Permissao`), controlando o acesso a diferentes recursos do sistema (e.g., leitura, escrita, administração).
  - **Cardinalidade**: 1:N (Um para Muitos) - Um usuário pode ter muitas permissões.

---

### **RNO 9: Auditoria e Rastreamento de Atividades**

- **Descrição**: Estabelece como as atividades dos usuários e as operações críticas no sistema devem ser monitoradas e registradas para garantir a integridade e segurança dos dados.

- **Tabelas Envolvidas**: `TBL_Auditoria`

- **Detalhamento**:
  - **TBL_Auditoria**:
    - Colunas: `Id` (ROWID, PK), `Usuario_id` (ROWID, FK), `Acao` (VARCHAR2(255)), `Data_Hora` (TIMESTAMP), `Detalhes` (CLOB)
    - Chave Primária: `TBL_Auditoria_PK` (Id)
    - Chave Estrangeira: `TBL_Auditoria_TBL_Usuario_FK` (`Usuario_id`) referenciando `TBL_Usuario` (Id)

- **Relacionamento**:
  - Cada ação de usuário que afete dados críticos deve ser registrada na tabela `TBL_Auditoria` para futura referência e controle.
  - **Cardinalidade**: 1:N (Um para Muitos) - Um usuário pode realizar muitas ações que serão auditadas.

---

### **RNO 10: Escalabilidade e Otimização de Performance**

- **Descrição**: Define as práticas para garantir que o sistema seja escalável e opere com alta performance mesmo com grandes volumes de dados meteorológicos e usuários simultâneos.

- **Tabelas Envolvidas**: Todas as tabelas podem ser impactadas por práticas de escalabilidade e otimização.

- **Detalhamento**:
  - **Indexação**:
    - As tabelas de dados meteorológicos e previsões devem ser indexadas por `Regiao_id` e `Data` para garantir consultas rápidas.
  - **Particionamento**:
    - O particionamento das tabelas de dados históricos deve ser implementado para facilitar a gestão de grandes volumes de dados, possibilitando a separação por ano, mês, ou regiões.
  - **Cachê**:
    - Implementação de caching para previsões mais acessadas, reduzindo a carga de consulta diretamente ao banco de dados.

- **Relacionamento**:
  - As estratégias de escalabilidade devem ser aplicadas de maneira uniforme em todo o sistema, assegurando que cada componente possa suportar crescimento sem perda de desempenho.

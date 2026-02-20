# Projeto Final - Gestão de Equipamentos

API REST desenvolvida em **.NET 10** para gerenciamento de equipamentos
industriais.

## Tecnologias Utilizadas

-   .NET 10
-   ASP.NET Core Web API
-   Entity Framework Core
-   PostgreSQL
-   Docker Compose
-   xUnit (Testes)

------------------------------------------------------------------------

# Estrutura do Projeto

    projeto-final
    │
    ├── Controllers
    │   └── EquipamentosController.cs
    │
    ├── Domain
    │   ├── Models
    │   │   └── Equipamento.cs
    │   └── Enums
    │       ├── TipoEquipamento.cs
    │       └── StatusOperacional.cs
    │
    ├── DTOs
    │   ├── EquipamentoCreateDto.cs
    │   ├── EquipamentoUpdateDto.cs
    │   └── EquipamentoResponseDto.cs
    │
    ├── Services
    │   ├── IEquipamentoService.cs
    │   └── EquipamentoService.cs
    │
    ├── Data
    │   └── AppDbContext.cs
    │
    ├── docker-compose.yml
    ├── Program.cs
    └── appsettings.json

    ProjetoFinal.Tests
    │
    ├── Controllers
    ├── Domain
    ├── DTOs
    ├── Services
    ├── Data
    └── TestResults

------------------------------------------------------------------------

# Como Rodar via Docker Compose

## Subir o PostgreSQL

``` bash
docker compose up -d
```

Verifique se o container está rodando:

``` bash
docker ps
```

Banco:

-   Host: localhost
-   Porta: 5433
-   Database: projetofinal_db
-   User: postgres
-   Password: postgres

------------------------------------------------------------------------

# SQL para DBeaver

``` sql
-- Schema padrão
CREATE SCHEMA IF NOT EXISTS public;

-- Remove tabela antiga se existir
DROP TABLE IF EXISTS public.equipamentos CASCADE;

CREATE TABLE public.equipamentos (
    id                   SERIAL PRIMARY KEY,
    codigo               VARCHAR(50) NOT NULL,
    tipo                 VARCHAR(50) NOT NULL,
    modelo               VARCHAR(100) NOT NULL,
    horimetro            NUMERIC(18,2) NOT NULL DEFAULT 0,
    status_operacional   VARCHAR(50) NOT NULL,
    data_aquisicao       DATE,
    localizacao_atual    VARCHAR(200)
);

-- Índice único para Codigo
CREATE UNIQUE INDEX ux_equipamentos_codigo
    ON public.equipamentos (codigo);

-- Constraints adicionais
ALTER TABLE public.equipamentos
    ADD CONSTRAINT chk_horimetro_nao_negativo CHECK (horimetro >= 0),
    ADD CONSTRAINT chk_tipo_valido CHECK (
        tipo IN ('Caminhao', 'Escavadeira', 'Perfuratriz', 'Carregadeira', 'Trator')
    ),
    ADD CONSTRAINT chk_status_valido CHECK (
        status_operacional IN ('Operacional', 'EmManutencao', 'Parado')
    );

-- Dados de exemplo
INSERT INTO public.equipamentos
(codigo, tipo, modelo, horimetro, status_operacional, data_aquisicao, localizacao_atual)
VALUES
('CAT-793F-000123', 'Caminhao', 'Caterpillar 793F', 18234.50, 'Operacional', '2019-03-15', 'Mina Carajás N4E'),
('EXC-320D-000045', 'Escavadeira', 'Caterpillar 320D', 12500.75, 'EmManutencao', '2020-06-20', 'Oficina Central'),
('PER-ROC-000078', 'Perfuratriz', 'Sandvik DR410', 9800.20, 'Parado', '2018-11-10', 'Mina S11D');

-- Consulta rápida
SELECT * FROM public.equipamentos ORDER BY id;

```

------------------------------------------------------------------------
# Como realizar os testes no Postman/Insomnia

## buscar equipamentos

``` bash
GET http://localhost:5004/api/projFinal
```

## buscar equipamentos por ID

``` bash
GET http://localhost:5004/api/projFinal/{id}
```

## buscar equipamentos

``` bash
POST http://localhost:5004/api/projFinal
```
### Exemplo de JSON

``` bash
{
    "codigo": "CAT-793F-000128",
    "tipo": "Caminhao",
    "modelo": "Caterpillar 793F",
    "horimetro": 18234.5,
    "statusOperacional": "Operacional",
    "dataAquisicao": "2019-03-15",
    "localizacaoAtual": "Mina Carajás N4E"
}
```

## editar equipamento

``` bash
PUT http://localhost:5004/api/projFinal/{id}
```
### Exemplo de JSON

``` bash
{
  "horimetro": 20394.6,
  "statusOperacional": "Operacional",
  "localizacaoAtual": "Mina Carajás N4E"
}
```

## apagar equipamento

``` bash
DELETE http://localhost:5004/api/projFinal/{id}
```
------------------------------------------------------------------------

# SQL para DBeaver

``` sql
-- Schema padrão
CREATE SCHEMA IF NOT EXISTS public;

-- Remove tabela antiga se existir
DROP TABLE IF EXISTS public.equipamentos CASCADE;

CREATE TABLE public.equipamentos (
    id                   SERIAL PRIMARY KEY,
    codigo               VARCHAR(50) NOT NULL,
    tipo                 VARCHAR(50) NOT NULL,
    modelo               VARCHAR(100) NOT NULL,
    horimetro            NUMERIC(18,2) NOT NULL DEFAULT 0,
    status_operacional   VARCHAR(50) NOT NULL,
    data_aquisicao       DATE,
    localizacao_atual    VARCHAR(200)
);

-- Índice único para Codigo
CREATE UNIQUE INDEX ux_equipamentos_codigo
    ON public.equipamentos (codigo);

-- Constraints adicionais
ALTER TABLE public.equipamentos
    ADD CONSTRAINT chk_horimetro_nao_negativo CHECK (horimetro >= 0),
    ADD CONSTRAINT chk_tipo_valido CHECK (
        tipo IN ('Caminhao', 'Escavadeira', 'Perfuratriz', 'Carregadeira', 'Trator')
    ),
    ADD CONSTRAINT chk_status_valido CHECK (
        status_operacional IN ('Operacional', 'EmManutencao', 'Parado')
    );

-- Dados de exemplo
INSERT INTO public.equipamentos
(codigo, tipo, modelo, horimetro, status_operacional, data_aquisicao, localizacao_atual)
VALUES
('CAT-793F-000123', 'Caminhao', 'Caterpillar 793F', 18234.50, 'Operacional', '2019-03-15', 'Mina Carajás N4E'),
('EXC-320D-000045', 'Escavadeira', 'Caterpillar 320D', 12500.75, 'EmManutencao', '2020-06-20', 'Oficina Central'),
('PER-ROC-000078', 'Perfuratriz', 'Sandvik DR410', 9800.20, 'Parado', '2018-11-10', 'Mina S11D');

-- Consulta rápida
SELECT * FROM public.equipamentos ORDER BY id;

```

------------------------------------------------------------------------


# Rodando a API

``` bash
dotnet clean
dotnet run
```

API:

http://localhost:5004

Swagger:

http://localhost:5004/swagger

------------------------------------------------------------------------

# Executar Testes

``` bash
dotnet test
```

------------------------------------------------------------------------

# Observações

-   O banco deve estar ativo antes de iniciar a API.
-   Os enums são persistidos como string no PostgreSQL.
-   Projeto desenvolvido para fins acadêmicos.

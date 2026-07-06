# APIRest

Projeto de estudo prático de conceitos REST, construído com .NET 8, Entity Framework Core e SQLite.

## Objetivo

Praticar o design de uma API REST do zero: modelagem de recursos, verbos HTTP, status codes, paginação, cache e tratamento de erros — evoluindo a implementação conforme os conceitos vão sendo estudados.

## Stack

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core 8 + SQLite
- Swagger / OpenAPI

## Como rodar

```bash
docker-compose up -d

dotnet restore
dotnet run --project APIRest
```

As migrations (incluindo seed de dados) são aplicadas automaticamente na inicialização.

## Conceitos REST aplicados

- **Recursos no plural**: `/products` (coleção) e `/products/{id}` (item)
- **Verbos HTTP com semântica correta**: `GET`, `POST`, `PUT` (substituição completa), `PATCH` (atualização parcial), `DELETE`, `HEAD` e `OPTIONS`
- **Status codes** como parte do contrato (200, 201, 204, 400, 404, 409), sem depender de campos tipo `"error": true` no corpo
- **Paginação e filtros** via query string na própria URI da coleção
- **Idempotência**: `GET`/`PUT`/`DELETE` produzem o mesmo resultado em chamadas repetidas
- **Soft delete** consistente entre todos os endpoints de leitura

## Nível de maturidade (Richardson Maturity Model)

Atualmente no **Nível 2** (recursos + verbos HTTP + status codes semânticos). HATEOAS (Nível 3) ainda não implementado — próximo passo de estudo.

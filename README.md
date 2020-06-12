# Serviço responsável por efetuar lançamentos entre duas contas corrente


## Autenticação [Funcionalidade para autenticar cliente]
```
POST: api/Autenticacao
     {
         "documento": "40671663895",
         "senha": "12345678"
     }
```


## Cliente [Funcionalidade para cadastrar um novo cliente]
```
POST: api/Cliente
     {
        "nome": "carlos eduardo",
        "documento": "40671663895",
        "senha": "12345678"
     }
```


## Conta Corrente [Funcionalidades para cadastrar e obter o saldo de um cliente]
```
POST: api/ContaCorrente
     {
        "agencia": "1020",
        "numeroConta": "50006",
        "digitoVerificadorConta": 10,
        "numeroDocumento": "32959444825",
        "nomeLegal": "Carlos Eduardo",
        "tipoConta": 0,
        "valor":50.00
    }
```
* Para cadastrar uma nova conta corrente é preciso cadastrar um usuário e se autenticar.


```
GET: api/ContaCorrente
```
* Só é possível consultar o saldo de um cliente autenticado.


## Lançamento [Funcionalidades para efetuar lançamentos entre duas contas]
* No momento só é permitido transferir entre contas correntes.
```
POST: api/ContaCorrente
    {
        "agenciaFavorecido": "1020",
        "numeroContaFavorecido": "50006",
        "digitoVerificadorFavorecido": 10,
        "numeroDocumentoFavorecido": "32959444825",
        "nomeLegalFavorecido": "Monica Michelle",
        "tipoContaFavorecido": 0,
        "ValorLancamento": 100.00
    }
```
* Para efetuar um lançamento, é preciso estar autenticado com os dados do cliente que efetuará a transferência.

## HealthCheck
 `/internal/healtcheck`

## Métricas

`/internal/metrics`

## OpenApi
`/internal/swagger`



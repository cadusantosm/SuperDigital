# Serviço responsável por efetuar lançamentos entre duas contas correntes


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


## O que é Domain driven design
 
É uma abordagem de desenvolvimento de software que se baseia em fazer refletir seu software mais próximo do contexto no mundo real.
 
Ele foi criado por Eric Evans (Inclusive tenho o livro dele, mas não consegui mastigar todo ainda) onde o mesmo juntou diversos approach de desenvolvimento de software para criar e evoluir o DDD.
 
A importância na estratégia de desenvolvimento com essa abordagem é a proximidade do desenvolvimento focado no negócio, ou seja, no domínio.
 
 
## O que é e como funciona uma arquitetura baseada em micro serviços?
 
Uma arquitetura baseada em micro-serviços consiste em generalização o sistema em pequenas partes dando a cada parte uma responsabilidade (fazer aquela uma coisa muito bem).
 
A ideia da arquitetura é ter independência entre os demais serviços que compõem esse software, com isso temos diversos benefícios como: autonomia para escalar, autonomia para os times que irão desenvolvê-los, entrega continua com foco no negócio, monitoramento independente…
 
Desenvolvimento baseado em micro-serviços possuí muitos desafios, dentre eles estão:
 
* Consistência das informações
Desenvolver com micro-serviços requer a autonomia no armazenamento dos dados e o grande desafio aqui é garantir a consistência entre os dados de todos os serviços.
 
* Comunicação entre serviços
A comunicação entre serviços é um tema complexo, pois há diversas abordagens para se tratar do assunto. Alguns padrões podem garantir ou remover benefícios do microsserviços.
- Comunicação HTTP: é uma abordagem simples, mas cria pontos de falha e a perda de resiliência do serviço
- Comunicação via Mensagem: garante mais resiliência e baixo acoplamento entre os serviços que irão se comunicar.
 
* Resiliência
Caso um micro-serviço venha falhar, é importante o processo não parar. O sistema deve continuar operando e esse serviço que falhou voltar o mais rápido possível.
Há diversas abordagens para aumentar a resiliência de um serviço como:
* Monitoramento
* Retry pattern
* Circuit breaker
 
## Qual diferença entre comunicação síncrona e assíncrona e qual melhor cenário para usar uma e outra.
 
A diferença entre as duas comunicação é que na comunicação síncrona se faz necessário as duas partes presentes na comunicação já na comunicação assíncrona não.
 
Na comunicação síncrona a ordem do fluxo de dados é importante para que haja consistência na comunicação, sendo assim é transmitido um pacote por vez.
 
Na comunicação assíncrona é usado um "carimbo" em cada pacote a ser enviado para o remetente saber a ordem dos pacotes.

O melhor cenário para comunicação síncrona é com tarefas que devem ser executadas em sequência.
 
Deve-se evitar trabalhar de forma síncrona quando a execução a execução for longa, de maneira geral é I/O (Acesso a banco, arquivo, execuções longas) deve se usar comunicação assíncrona.
  
>> Em linguagens como C# são utilizados threads para comunicação assíncrona, já em linguagens como elixir se utiliza processos.

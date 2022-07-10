# Registro-Series-DotNet
#### Projeto em C# baseado no repositório de Eliézer Zarpelão: https://github.com/elizarp/dio-dotnet-poo-lab-2

## O que é o projeto
#### É uma aplicação em console que permite que o usuário registre séries (televisão, streaming etc) em uma lista.
#### O usuário pode adicionar, excluir e visualizar séries, com informações de ano, gênero, título e descrição.

## Mudanças
#### O projeto é uma implementação baseada na demonstração de Eliézer Zarpelão, com funcionalidades novas:
1. O console pede a confirmação do usuário para certas operações, e.g. indicar uma série como excluída e sobreescrever uma lista existente.
2. A função que recebe input do usuário para os parâmetros de uma série, ```GerarSerieComParametros()```. Ela evita a repetição de código previamente vista nas funções ```AtualizarSerie()``` e ```InserirSerie()```, seguindo o conceito *Don't Repeat Yourself* (DRY).
3. A classe ```Csv```, que permite a importação e exportação de arquivo .csv para que o usuário possa salvar e carregar a sua lista, com o nome desejado. Todo arquivo é criado na pasta do projeto e carregado dela também.
4. Comandos do usuário no menu principal usam input com ```Console.ReadKey()``` ao invés de ```Console.ReadLine()``` para agilizar a experiência do usuário.

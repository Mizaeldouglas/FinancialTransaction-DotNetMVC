# Aplicação de Transações Financeiras (EM CONSTRUÇÃO)

Esta aplicação é um sistema de gerenciamento de transações financeiras desenvolvido usando C# e .NET MVC. Ele permite que os usuários importem transações, gerenciem usuários e visualizem históricos de transações.

## Recursos

1. **Importar Transações**: Os usuários podem importar transações de um arquivo CSV. O sistema valida os dados e salva as transações válidas no banco de dados. Transações duplicadas e transações com dados faltantes ou inválidos são ignoradas.

2. **Gerenciamento de Usuários**: Os administradores podem criar, editar e excluir usuários. Quando um novo usuário é criado, uma senha aleatória é gerada para ele.

3. **Visualizar Históricos de Transações**: Os usuários podem visualizar uma lista de todas as transações importadas e seus detalhes.

## Tecnologias Utilizadas

- **C#**: O backend da aplicação é escrito em C#.
- **JavaScript**: Usado para scripting de frontend.
- **ASP.NET MVC**: A aplicação usa o padrão arquitetural MVC.
- **Entity Framework Core**: Usado para acesso a dados.

## Estrutura do Projeto

O projeto é estruturado em Controladores, Visualizações e Modelos.

- **Controladores**: Lidam com solicitações de navegador recebidas, recuperam dados do modelo e, em seguida, especificam modelos de visualização que retornam uma resposta ao navegador.
- **Visualizações**: Contêm a marcação HTML e o conteúdo enviado ao cliente. Eles usam a sintaxe Razor para incorporar código .NET.
- **Modelos**: Representam os dados em sua aplicação. O ASP.NET Core MVC também pode validar a entrada do formulário.

## Configuração

Para executar este projeto, você precisa ter o .NET Core SDK instalado em sua máquina. Após clonar o projeto, navegue até o diretório do projeto e execute o seguinte comando para iniciar a aplicação:

```bash
dotnet run
```

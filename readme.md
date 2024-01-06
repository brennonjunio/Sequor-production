<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Comandos do Projeto</title>
</head>
<body>

    <h2>Restaurar/Instalar Dependências:</h2>
    <pre>
        <code>dotnet restore</code>
    </pre>

    <h2>Instalar Pacotes:</h2>
    <h3>Usando o Terminal:</h3>
    <pre>
        <code>dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.0</code>
        <code>dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.0</code>
        <code>dotnet add package Microsoft.VisualStudio.Azure.Containers.Tools.Targets --version 1.19.5</code>
        <code>dotnet add package Swashbuckle.AspNetCore --version 6.5.0</code>
    </pre>

    <h3>Usando o Console do Gerenciador de Pacotes no Visual Studio:</h3>
    <pre>
        <code>Install-Package Microsoft.EntityFrameworkCore.Design -Version 6.0.0</code>
        <code>Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 6.0.0</code>
        <code>Install-Package Microsoft.VisualStudio.Azure.Containers.Tools.Targets -Version 1.19.5</code>
        <code>Install-Package Swashbuckle.AspNetCore -Version 6.5.0</code>
    </pre>

    <h2>Criar o Schema no Banco de Dados:</h2>
    <h3>Usando o Terminal:</h3>
    <pre>
        <code>dotnet ef database update</code>
    </pre>

    <h3>Usando o Console do Gerenciador de Pacotes no Visual Studio:</h3>
    <pre>
        <code>Update-Database</code>
    </pre>

    <h2>Rodar o Projeto:</h2>
    <pre>
        <code>dotnet run</code>
    </pre>

</body>
</html>

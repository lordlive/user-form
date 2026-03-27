pipeline {
    agent { 
        label 'windows' 
    }

    environment {
        // Шлях до MSBuild (перевірте версію на вашому сервері)
        MSBUILD_PATH = "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe"
        // Шлях до NuGet
        NUGET_PATH = "C:\\nuget\\nuget.exe"
        // Це критично для SSH на Windows-агентах без налаштованого known_hosts
        GIT_SSH_COMMAND = "ssh -o StrictHostKeyChecking=no"
    }

    stages {
        stage('Checkout') {
            steps {
                // Видаляємо старі файли перед клонуванням для чистоти
                deleteDir() 
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                // Відновлення пакетів через NuGet
                bat "${env.NUGET_PATH} restore UsersForms.sln"
            }
        }

        stage('Build') {
            steps {
                // Збірка проекту
                bat "\"${env.MSBUILD_PATH}\" UsersForms.sln /p:Configuration=Release /p:Platform=\"Any CPU\""
            }
        }

        stage('Test') {
            steps {
                // Якщо є тести (MSTest, NUnit, xUnit), зазвичай використовується vstest.console.exe
                echo 'Running tests...'
                // bat "vstest.console.exe YourTests.dll"
            }
        }
    }
}

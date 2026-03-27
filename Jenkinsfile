pipeline {
    agent { label 'winserver' }

    options {
        // Налаштування зберігання:
        // daysToKeepStr: скільки днів зберігати білд (наприклад, 7 днів)
        // numToKeepStr: яку кількість останніх білдів зберігати (наприклад, останні 10)
        // artifactDaysToKeepStr: окремо для артефактів (якщо треба видаляти їх раніше за логи)
        buildDiscarder(logRotator(
            daysToKeepStr: '7', 
            numToKeepStr: '10',
            artifactDaysToKeepStr: '3',
            artifactNumToKeepStr: '5'
        ))
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                // Використовуємо сучасний dotnet restore замість nuget.exe
                bat "dotnet restore UsersForms.sln"
            }
        }

        stage('Build') {
            steps {
                // Збірка через dotnet CLI (вона автоматично знайде потрібний MSBuild)
                bat "dotnet build UsersForms.sln --configuration Release --no-restore"
            }
        }

        stage('Test') {
            steps {
                bat "dotnet test UsersForms.sln --configuration Release --no-restore"
            }
        }

        stage('Archive') {
            steps {
                // Архівуємо артефакти, як у GitHub Actions
                archiveArtifacts artifacts: '**/bin/Release/**', allowEmptyArchive: true
            }
        }
        post {
            always {
                echo 'Очищення робочого простору...'
                cleanWs() // Видаляє вміст папки workspace на агенті
            }
        }
    }
}
